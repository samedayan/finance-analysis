using System.Net.Mime;
using Finance.Analysis.Api.Filters.ValidationModels;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Add;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Update;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Search;
using Finance.Analysis.Infrastructure.Exceptions;
using Finance.Analysis.Infrastructure.Extensions;
using Finance.Analysis.Infrastructure.Loggers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Finance.Analysis.Api.V0.Controllers;

[ApiController]
[Route("api/v0/risk-analysis")]
[Produces(MediaTypeNames.Application.Json)]
public class RiskAnalysisController(IConsoleLogger consoleLogger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindRiskAnalysisResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] FindRiskAnalysisQuery query)
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
    
    [HttpGet("{riskAnalysisId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully response list", typeof(FindRiskAnalysisResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string riskAnalysisId)
    {
        try
        {
            var query = new FindRiskAnalysisQuery { RiskAnalysisId = riskAnalysisId };
        
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
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(RiskAnalysisResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromBody] RiskAnalysisAddCommand query)
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
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(RiskAnalysisResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(RiskAnalysisUpdateCommand request)
    {
        try
        {
            if (Guid.Empty == request.RiskAnalysisId)
            {
                throw new BusinessException($"RiskAnalysis Id Dont Empty!.", new Exception($"{request.RiskAnalysisId} is not a valid riskAnalysis id."));
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
    
    [HttpDelete("{riskAnalysisId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully", typeof(RiskAnalysisResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string riskAnalysisId)
    {
        try
        {
            if (string.IsNullOrEmpty(riskAnalysisId) || new Guid(riskAnalysisId) == Guid.Empty)
            {
                throw new BusinessException($"RiskAnalysis Id Dont Empty!.", new Exception($"{riskAnalysisId} is not a valid riskAnalysis id."));
            }
            
            await consoleLogger.LogInformation(riskAnalysisId.AsJson());
            
            var response = await mediator.Send(new Guid(riskAnalysisId));
            
            return Ok(response);
        }
        catch (BusinessException exception)
        {
            throw new BusinessException(exception.Message, exception);
        }
    }
}