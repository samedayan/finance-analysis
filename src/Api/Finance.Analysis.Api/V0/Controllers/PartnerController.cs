using System.Net.Mime;
using Finance.Analysis.Api.Filters.ValidationModels;
using Finance.Analysis.Contracts.Commands.Partners.Add;
using Finance.Analysis.Contracts.Commands.Partners.Delete;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.Partners.Update;
using Finance.Analysis.Contracts.Queries.Partners.Responses;
using Finance.Analysis.Contracts.Queries.Partners.Search;
using Finance.Analysis.Infrastructure.Exceptions;
using Finance.Analysis.Infrastructure.Extensions;
using Finance.Analysis.Infrastructure.Loggers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Finance.Analysis.Api.V0.Controllers;

[ApiController]
[Route("api/v0/partners")]
[Produces(MediaTypeNames.Application.Json)]
public class PartnerController(IConsoleLogger consoleLogger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindPartnerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] FindPartnerQuery query)
    {
        try
        {
            await consoleLogger.LogInformation(query.AsJson());

            var response = await mediator.Send(query);

            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
    
    [HttpGet("{partnerId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindPartnerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string partnerId)
    {
        try
        {
            var query = new FindPartnerQuery { PartnerId = partnerId };
        
            await consoleLogger.LogInformation(query.AsJson());
        
            var response = await mediator.Send(query);
        
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
    
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(PartnerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] PartnerAddCommand command)
    {
        try
        {
            await consoleLogger.LogInformation(command.AsJson());
            
            var response = await mediator.Send(command);
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
    
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(PartnerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(PartnerUpdateCommand request)
    {
        try
        {
            if (Guid.Empty == request.PartnerId)
            {
                throw new BusinessException($"Partner Id Dont Empty!.", new Exception($"{request.PartnerId} is not a valid partner id."));
            }
            
            await consoleLogger.LogInformation(request.AsJson());
            
            var response = await mediator.Send(request);
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
    
    [HttpDelete("{partnerId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(PartnerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] PartnerDeleteCommand request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.PartnerId) || new Guid(request.PartnerId) == Guid.Empty)
            {
                throw new BusinessException($"Partner Id Dont Empty!.", new Exception($"{request.PartnerId} is not a valid partner id."));
            }
            
            await consoleLogger.LogInformation(request.PartnerId.AsJson());
            
            var guid = new Guid(request.PartnerId);
            var response = await mediator.Send(guid);
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
}