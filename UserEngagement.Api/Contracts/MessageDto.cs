using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace UserEngagement.Api.Contracts;

public sealed record MessageDto : MessageBaseDto
{
    /// <summary>
    /// Unique Id of the message (technical id)
    /// </summary>
    /// <example>1</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonPropertyOrder(10)]
    public long Id { get; init; }
}