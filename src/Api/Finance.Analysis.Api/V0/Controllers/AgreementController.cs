using System.Net.Mime;
using Finance.Analysis.Api.Filters.ValidationModels;
using Finance.Analysis.Contracts.Commands.Agreements.Add;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Contracts.Commands.Agreements.Update;
using Finance.Analysis.Contracts.Queries.Agreement.Responses;
using Finance.Analysis.Contracts.Queries.Agreement.Search;
using Finance.Analysis.Infrastructure.Exceptions;
using Finance.Analysis.Infrastructure.Extensions;
using Finance.Analysis.Infrastructure.Loggers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Finance.Analysis.Api.V0.Controllers;

[ApiController]
[Route("api/v0/agreements")]
[Produces(MediaTypeNames.Application.Json)]
public class AgreementController(IConsoleLogger consoleLogger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindAgreementResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] FindAgreementQuery query)
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
    
    [HttpGet("{agreementId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindAgreementResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string agreementId)
    {
        try
        {
            var query = new FindAgreementQuery { AgreementId = agreementId };
        
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
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(AgreementResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] AgreementAddCommand query)
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
    
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(AgreementResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(AgreementUpdateCommand request)
    {
        try
        {
            if (Guid.Empty == request.AgreementId)
            {
                throw new BusinessException($"Agreement Id Dont Empty!.", new Exception($"{request.AgreementId} is not a valid agreement id."));
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
    
    [HttpDelete("{agreementId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(AgreementResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string agreementId)
    {
        try
        {
            if (string.IsNullOrEmpty(agreementId) || new Guid(agreementId) == Guid.Empty)
            {
                throw new BusinessException($"Agreement Id Dont Empty!.", new Exception($"{agreementId} is not a valid agreement id."));
            }
            
            await consoleLogger.LogInformation(agreementId.AsJson());
            
            var response = await mediator.Send(new Guid(agreementId));
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
}