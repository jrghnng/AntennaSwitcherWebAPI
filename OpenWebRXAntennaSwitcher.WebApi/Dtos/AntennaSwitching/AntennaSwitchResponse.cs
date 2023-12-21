namespace OpenWebRXAntennaSwitcher.WebApi.Dtos.AntennaSwitching;

/// <summary>
/// A response of the antenna switch.
/// </summary>
public class AntennaSwitchResponse
{
    /// <summary>
    /// The response message
    /// </summary>
    public string? Response { get; set; }

    /// <summary>
    /// The time in milliseconds between the command being sent and a response being received.
    /// </summary>
    public long ResponseMilliseconds { get; set; }
}
