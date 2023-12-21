using OpenWebRXAntennaSwitcher.WebApi.Dtos.AntennaSwitching;

namespace OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;

/// <summary>
/// Interface to the serial antenna switch.
/// </summary>
public interface ISerialAntennaSwitch
{
    /// <summary>
    /// Sends a command to the serial antenna switch.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>The response.</returns>
    AntennaSwitchResponse SendCommand(string command);
}
