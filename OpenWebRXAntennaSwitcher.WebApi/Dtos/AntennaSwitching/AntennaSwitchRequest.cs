using System.ComponentModel.DataAnnotations;

namespace OpenWebRXAntennaSwitcher.WebApi.Dtos.AntennaSwitching;

/// <summary>
/// A request to the antenna switch to execute the given <see cref="Command"/>.
/// </summary>
public class AntennaSwitchRequest
{
    /// <summary>
    /// The command to execute.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [StringLength(8)]
    public required string Command { get; set; }
}
