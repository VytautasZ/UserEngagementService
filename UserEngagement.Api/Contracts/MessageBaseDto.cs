using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace UserEngagement.Api.Contracts;

public record MessageBaseDto
{
    /// <summary>
    /// Unique currency code
    /// </summary>
    /// <example>0000-0000-0000-0000</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonPropertyOrder(20)]
    public Guid UserId { get; init; }

    /// <summary>
    /// Message text
    /// </summary>
    /// <example>"You have a new notificaiton!"</example>
    [Required]
    [SwaggerSchema(ReadOnly = true)]
    [JsonPropertyOrder(30)]
    public required string Text { get; init; }
}