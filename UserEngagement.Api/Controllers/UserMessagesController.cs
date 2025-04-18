using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserEngagement.Api.Contracts;
using UserEngagement.Api.Mapping;
using UserEngagement.Application.Commands;
using UserEngagement.Application.Queries;
using UserEngagement.Core.Domain;
using UserEngagement.Core.Interfaces;

namespace UserEngagement.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/user-messages")]
[ApiVersion("1.0")]
[Consumes("application/json")]
[Produces("application/json")]
public sealed class UserMessagesController : ControllerBase
{
    private const string SWAGGER_GROUP = "UserMesages";

    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public UserMessagesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    /// <summary>
    /// Send a message to a user
    /// </summary>
    /// <param name="messageDto">The message details</param>
    /// <param name="cancellation">Cancellation token</param>
    /// <remarks>
    /// **Sample request:**
    ///
    ///     POST /api/v1/user-messages
    ///     {
    ///         "userId": "6a51d4c5-a36c-441a-a719-77ea5b069c50",
    ///         "text": "You have a new message"
    ///     }
    /// </remarks>
    /// <response code="200">The actualized resource</response>
    /// <response code="400">If the structure of the payload is incorrect</response>
    /// <response code="401">If no authentication or an invalid authentication is present</response>
    /// <response code="403">If the authenticated user does not have the right to actualize the resource</response>
    /// <response code="404">If the resource could not be found</response>
    /// <response code="406">If the expected media type for the response is not supported</response>
    /// <response code="415">If the delivered media type for the request is not supported</response>
    /// <response code="422">If the payload contains invalid values for the resource</response>
    /// <response code="500">If an unexpected error occurs</response>
    [HttpPost(Name = ApiRoutes.UserMessages.SEND_MESSAGE_TO_USER)]
    [SwaggerOperation(Tags = new[] { SWAGGER_GROUP })]
    [ProducesResponseType(typeof(MessageBaseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageBaseDto>> SentMessageToUserAsync([FromBody] MessageBaseDto messageBaseDto, CancellationToken cancellation = default)
    {
        var command = new SendMessageCommand()
        {
            Message = messageBaseDto.ToMessage()
        };

        var messageId = await _commandDispatcher.Dispatch<SendMessageCommand, long>(command, cancellation);

        return CreatedAtAction(ApiRoutes.UserMessages.GET_MESSAGE_BY_ID, new { messageId = messageId }, messageBaseDto);
    }
    /// <summary>
    /// Get a message sent to the user by message ID.
    /// </summary>
    /// <param name="messageId">The unique identifier of the user message.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <remarks>
    /// **Sample request:**
    ///     GET /api/v1/user-messages/123
    /// </remarks>
    /// <response code="200">Returns the message details</response>
    /// <response code="401">If authentication is missing or invalid</response>
    /// <response code="403">If the user is not authorized to access the resource</response>
    /// <response code="404">If the message with the given ID does not exist</response>
    /// <response code="406">If the requested media type is not supported</response>
    /// <response code="415">If the request's media type is not supported</response>
    /// <response code="500">If an unexpected server error occurs</response>
    [HttpGet("{messageId}", Name = ApiRoutes.UserMessages.GET_MESSAGE_BY_ID)]
    [SwaggerOperation(Tags = [SWAGGER_GROUP])]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(NoContentDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDto>> GetMessageById([FromRoute] long messageId, CancellationToken cancellationToken = default)
    {
        var query = new GetMessageByIdQuery()
        {
            MessageId = messageId
        };

        var message = await _queryDispatcher.Dispatch<GetMessageByIdQuery, Message>(query, cancellationToken);
        
        if(message == null)
        {
            return NotFound();
        }

        return Ok(message!.ToMessageDto());
    }
}