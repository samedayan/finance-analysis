using System.Net.Mime;
using Finance.Analysis.Api.Filters.ValidationModels;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Contracts.Commands.WorkItems.Update;
using Finance.Analysis.Contracts.Commands.WorkItems.Add;
using Finance.Analysis.Contracts.Queries.WorkItems.Responses;
using Finance.Analysis.Contracts.Queries.WorkItems.Search;
using Finance.Analysis.Infrastructure.Exceptions;
using Finance.Analysis.Infrastructure.Extensions;
using Finance.Analysis.Infrastructure.Loggers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Finance.Analysis.Api.V0.Controllers;

[ApiController]
[Route("api/v0/work-items")]
[Produces(MediaTypeNames.Application.Json)]
public class WorkItemsController(IConsoleLogger consoleLogger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindWorkItemResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] FindWorkItemQuery query)
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
    
    [HttpGet("{workItemId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindWorkItemResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string workItemId)
    {
        try
        {
            var query = new FindWorkItemQuery { WorkItemId = workItemId };
        
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
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(WorkItemResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromQuery] WorkItemAddCommand query)
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
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(WorkItemResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(WorkItemUpdateCommand request)
    {
        try
        {
            if (Guid.Empty == request.WorkItemId)
            {
                throw new BusinessException($"WorkItem Id Dont Empty!.", new Exception($"{request.WorkItemId} is not a valid workItem id."));
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
    
    [HttpDelete("{workItemId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(WorkItemResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string workItemId)
    {
        try
        {
            if (string.IsNullOrEmpty(workItemId) || new Guid(workItemId) == Guid.Empty)
            {
                throw new BusinessException($"WorkItem Id Dont Empty!.", new Exception($"{workItemId} is not a valid workItem id."));
            }
            
            await consoleLogger.LogInformation(workItemId.AsJson());
            
            var response = await mediator.Send(new Guid(workItemId));
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
}