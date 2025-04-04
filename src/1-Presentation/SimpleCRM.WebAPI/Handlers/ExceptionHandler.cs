﻿using System.Net;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Common.System.Exceptions;

namespace SimpleCRM.WebAPI.Handlers;

public class ExceptionHandler
{
    protected readonly ILogger<ExceptionHandler> Logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        Logger = logger;
    }
    
    public async Task Handler(HttpContext context, Exception error)
    {
        var validationRS = new ValidationRS();
        var response = context.Response;
        response.ContentType = "application/json";

        switch(error)
        {
            case BusinessException validationException:
                // custom application error
                validationRS.AddValidation(validationException.Key, validationException.Message);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException notFoundException:
                // not found error
                response.StatusCode = (int)HttpStatusCode.NotFound;
                var message = string.IsNullOrEmpty(notFoundException.Message) ? "Register not found!" : notFoundException.Message;
                validationRS.AddValidation(notFoundException.Key, message);
                break;
            default:
                // unhandled error
                var errorRS = new ErrorRS(error);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsJsonAsync(errorRS);
                return;
        }
        
        await response.WriteAsJsonAsync(validationRS);
    }
}