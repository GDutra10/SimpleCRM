using System.Text.Json.Serialization;

namespace SimpleCRM.Domain.Common.DTOs;

public class ValidationRS : BaseRS
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<KeyValuePair<string, string>>? ValidationMessages { get; set; }

    public void AddValidationMessage(string key, string message)
    {
        ValidationMessages ??= new List<KeyValuePair<string, string>>();
        ValidationMessages.Add(new KeyValuePair<string, string>(key, message));
    }
}