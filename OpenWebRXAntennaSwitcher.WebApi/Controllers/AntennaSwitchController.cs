﻿using Microsoft.AspNetCore.Mvc;
using OpenWebRXAntennaSwitcher.WebApi.Dtos;
using OpenWebRXAntennaSwitcher.WebApi.Dtos.AntennaSwitching;
using OpenWebRXAntennaSwitcher.WebApi.Services.AntennaSwitching;

namespace OpenWebRXAntennaSwitcher.WebApi.Controllers;

[ApiController]
[Route("antennaswitch")]
public class AntennaSwitchController : ControllerBase
{
    private readonly ISerialAntennaSwitch _antennaSwitch;

    public AntennaSwitchController(ISerialAntennaSwitch antennaSwitch)
    {
        _antennaSwitch = antennaSwitch;
    }

    [HttpPost]
    public IActionResult SendCommand([FromBody] AntennaSwitchRequest request)
    {
        var response = _antennaSwitch.SendCommand(request.Command);
        return Ok(new Response<AntennaSwitchResponse>()
        {
            Payload = response
        });
    }
}
