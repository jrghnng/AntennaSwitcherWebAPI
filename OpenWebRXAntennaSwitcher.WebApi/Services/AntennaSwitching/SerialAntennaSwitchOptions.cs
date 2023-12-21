using System.IO.Ports;

namespace OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;

/// <summary>
/// Options for the serial antenna switch.
/// </summary>
public class SerialAntennaSwitchOptions
{
    /// <summary>
    /// The <see cref="SerialPort.PortName"/>.
    /// </summary>
    public string PortName { get; set; } = "COM3";

    /// <summary>
    /// The <see cref="SerialPort.BaudRate"/>.
    /// </summary>
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// The <see cref="SerialPort.Parity"/>.
    /// </summary>
    public Parity Parity { get; set; } = Parity.None;

    /// <summary>
    /// The <see cref="SerialPort.DataBits"/>.
    /// </summary>
    public int DataBits { get; set; } = 8;

    /// <summary>
    /// The <see cref="SerialPort.StopBits"/>.
    /// </summary>
    public StopBits StopBits { get; set; } = StopBits.One;

    /// <summary>
    /// The <see cref="SerialPort.Handshake"/>.
    /// </summary>
    public Handshake Handshake { get; set; } = Handshake.XOnXOff;

    /// <summary>
    /// The <see cref="SerialPort.NewLine"/>.
    /// </summary>
    public string SerialNewLine { get; set; } = "\r\n";

    /// <summary>
    /// The <see cref="SerialPort.ReadTimeout"/>.
    /// </summary>
    public int ReadTimeoutMilliseconds { get; set; } = 10000;

    /// <summary>
    /// The <see cref="SerialPort.WriteTimeout"/>.
    /// </summary>
    public int WriteTimeoutMilliseconds { get; set; } = 3000;
}
