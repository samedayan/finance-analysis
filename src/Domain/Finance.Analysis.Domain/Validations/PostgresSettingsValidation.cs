using System.Text.Json;
using Finance.Analysis.Domain.Settings.PostgresSql;
using Finance.Analysis.Infrastructure.Loggers;
using Microsoft.Extensions.Options;

namespace Finance.Analysis.Domain.Validations;

public class PostgresSettingsValidation : IValidateOptions<PostgresSettings>
{
    private readonly IConsoleLogger _logger;

    public PostgresSettingsValidation(IConsoleLogger logger)
    {
        _logger = logger;
    }

    public ValidateOptionsResult Validate(string name, PostgresSettings options)
    {
        _logger.LogTrace($"{nameof(PostgresSettings)}:{JsonSerializer.Serialize(options)}");

        if (!string.IsNullOrEmpty(options.ConnectionString?.Trim()))
            return ValidateOptionsResult.Success;

        _logger.LogError($"{options.GetType().Name}:{nameof(options.ConnectionString)} is null");
        return ValidateOptionsResult.Fail(
            $"{options.GetType().Name}:{nameof(options.ConnectionString)} is null");
    }
}