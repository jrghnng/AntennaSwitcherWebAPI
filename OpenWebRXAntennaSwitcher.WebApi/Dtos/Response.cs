namespace OpenWebRXAntennaSwitcher.WebApi.Dtos;

/// <summary>
/// An API response.
/// </summary>
/// <typeparam name="T">The payload type.</typeparam>
public class Response<T>
{
    /// <summary>
    /// The payload.
    /// </summary>
    public T? Payload { get; set; }
}
