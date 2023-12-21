using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenWebRXAntennaSwitcher.WebApi.Dtos.AntennaSwitching;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;

/// <summary>
/// Implementation of <see cref="ISerialAntennaSwitch"/>.
/// </summary>
public class SerialAntennaSwitch : IHostedService, ISerialAntennaSwitch, IDisposable
{
    private readonly ILogger _logger;
    private readonly SerialAntennaSwitchOptions _options;

    private readonly object _serialPortLock = new();
    private SerialPort? _serialPort;

    public SerialAntennaSwitch(ILogger<SerialAntennaSwitch> logger, IOptions<SerialAntennaSwitchOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    ~SerialAntennaSwitch()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Disconnect();
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        LogOptions();
        Connect();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Disconnect();
        return Task.CompletedTask;
    }

    private SerialPort Connect(bool reconnect = false)
    {
        lock (_serialPortLock)
        {
            if (reconnect) Disconnect();

            var serialPort = _serialPort;
            if (serialPort != null) return serialPort; // Already connected

            LogPortNames();

            _logger.LogInformation("Connecting ...");

            serialPort = new SerialPort
            {
                PortName = _options.PortName,
                BaudRate = _options.BaudRate,
                Parity = _options.Parity,
                DataBits = _options.DataBits,
                StopBits = _options.StopBits,
                Handshake = _options.Handshake,
                NewLine = _options.SerialNewLine,
                ReadTimeout = _options.ReadTimeoutMilliseconds,
                WriteTimeout = _options.WriteTimeoutMilliseconds
            };

            serialPort.Open();
            _serialPort = serialPort;

            _logger.LogInformation("Connected to {Port}", serialPort.PortName);

            return serialPort;
        }
    }

    private void Disconnect()
    {
        lock (_serialPortLock)
        {
            if (_serialPort == null) return;

            _logger.LogInformation("Disconnecting ...");

            _serialPort.Close();
            _serialPort.Dispose();
            _serialPort = null;

            _logger.LogInformation("Disconnected");
        }
    }

    private void LogOptions()
    {
        _logger.LogInformation("SerialAntennaSwitchOptions: {Options}", JsonSerializer.Serialize(_options));
    }

    private void LogPortNames()
    {
        _logger.LogInformation("Available Ports:");
        foreach (string portName in SerialPort.GetPortNames())
        {
            _logger.LogInformation("  {PortName}", portName);
        }
    }

    public AntennaSwitchResponse SendCommand(string command)
    {
        lock (_serialPortLock)
        {
            var serialPort = Connect();
            serialPort.ReadExisting(); // Clear buffer

            var stopwatch = Stopwatch.StartNew();
            serialPort.Write(command);
            var response = serialPort.ReadLine();
            stopwatch.Stop();

            return new AntennaSwitchResponse
            {
                Response = response,
                ResponseMilliseconds = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
