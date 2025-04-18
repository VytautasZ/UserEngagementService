using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace UserEngagement.Api.Contracts;

public sealed record MessageDto
{
    /// <summary>
    /// Unique Id of the message (technical id)
    /// </summary>
    /// <example>1</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonProperty(Order = 10)]
    public long Id { get; init; }

    /// <summary>
    /// Unique currency code
    /// </summary>
    /// <example>0000-0000-0000-0000</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonProperty(Order = 20)]
    public Guid UserId { get; init; }

    /// <summary>
    /// Message text
    /// </summary>
    /// <example>"You have a new notificaiton!"</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonProperty(Order = 30)]
    public required string Text { get; init; }
}