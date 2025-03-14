using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Backoffice.WebApp.System.Exception;

namespace SimpleCRM.Backoffice.WebApp.API;

public class SimpleCRMApi : ISimpleCRMApi
{
    private const string CantReachApiMessage = "Can't reach API. Please, try again later or contact the Admin!";
    private readonly ILogger<SimpleCRMApi> _logger;
    private readonly Uri _urlBase;
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new () { PropertyNameCaseInsensitive = true, Converters  = { new JsonStringEnumConverter() }};

    public SimpleCRMApi(
        ILogger<SimpleCRMApi> logger,
        IConfiguration configuration, 
        IHttpClientFactory httpClientFactory)
    {
        var section = configuration.GetSection("SimpleCRMApi");
        var url = section.GetValue<string>("URL") ?? throw new ClientException("SimpleCRMApi.UrlBase has not been defined on configuration!");

        _logger = logger;
        _urlBase = new Uri(url);
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<(LoginRS?, ValidationRS?, ErrorRS?)> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Post, "Authentication/Login", loginRQ);

        return await GetResultOrThrowAsync<LoginRS>(httpRequestMessage, cancellationToken);
    }

    public async Task<bool> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Post, "Authentication/ValidateToken", accessToken: accessToken);

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

        return httpResponseMessage.StatusCode == HttpStatusCode.OK;
    }

    public async Task<(OrderRS?, ValidationRS?, ErrorRS?)> GetOrderAsync(string accessToken, string orderId, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Get, $"Backoffice/Orders/{orderId}", accessToken: accessToken);

        return await GetResultOrThrowAsync<OrderRS>(httpRequestMessage, cancellationToken);
    }

    public async Task<(BaseRS?, ValidationRS?, ErrorRS?)> UpdateOrderAsync(string accessToken, string orderId, OrderBackofficeUpdateRQ orderUpdateRQ, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Put, $"Backoffice/Orders/{orderId}", orderUpdateRQ, accessToken: accessToken);
        
        return await GetResultOrThrowAsync<BaseRS>(httpRequestMessage, cancellationToken);
    }

    public async Task<(OrderSearchRS?, ValidationRS?, ErrorRS?)> GetOrdersAsync(string accessToken, OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Post, $"Backoffice/Orders", orderSearchRQ, accessToken: accessToken);
        
        return await GetResultOrThrowAsync<OrderSearchRS>(httpRequestMessage, cancellationToken);
    }

    public async Task<(InteractionStartRS?, ValidationRS?, ErrorRS?)> StartInteractionAsync(string accessToken, string orderId, CancellationToken cancellationToken)
    {
        var httpRequestMessage = GetHttpRequestMessage(HttpMethod.Post, $"Backoffice/Interactions/Start?orderId={orderId}", accessToken: accessToken);
        
        return await GetResultOrThrowAsync<InteractionStartRS>(httpRequestMessage, cancellationToken);
    }

    private HttpRequestMessage GetHttpRequestMessage(HttpMethod httpMethod, string relativeUri, object? content = null, string? accessToken = null)
    {
        var httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(_urlBase, relativeUri));

        if (content is not null)
            httpRequestMessage.Content = JsonContent.Create(content);
        
        if (accessToken is not null)
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");

        return httpRequestMessage;
    }
    
    private async Task<(T?, ValidationRS?, ErrorRS?)> GetResultOrThrowAsync<T>(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken) where T : BaseRS, new()
    {
        T? result = default(T);
        ValidationRS? validationRS = null;
        ErrorRS? errorRS = null;
        
        _logger.LogInformation("Consuming SimpleCRMApi {RequestUri}", httpRequestMessage.RequestUri);
        _logger.LogDebug("HttpRequestMessage: {HttpRequestMessage}", httpRequestMessage);
        
        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        var response = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("response: {Response}", response);
        
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                validationRS = JsonSerializer.Deserialize<ValidationRS>(response, _jsonOptions);
                return (result, validationRS, errorRS);
            case HttpStatusCode.InternalServerError:
                errorRS = JsonSerializer.Deserialize<ErrorRS>(response, _jsonOptions);
                return (result, validationRS, errorRS);
            case HttpStatusCode.OK:
                result = JsonSerializer.Deserialize<T>(response, _jsonOptions);
                return (result, validationRS, errorRS);
            case HttpStatusCode.NoContent:
                return (new T(), validationRS, errorRS);
            default:
                throw new ClientException(CantReachApiMessage);
        }
    }
}