using System.Text.Json;

namespace BusinessManagement.Entities.ErrorModels;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString() => new JsonSerializer.Serialize(this);
}
