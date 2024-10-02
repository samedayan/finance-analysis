﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Finance.Analysis.Api.HealthChecks;

public class ReadinessHealthCheck  : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}