using Finance.Analysis.Contracts.Queries.RiskAnalysis.Search;
using FluentValidation;

namespace Finance.Analysis.Contracts.Queries.RiskAnalysis.ValidationRules;

public class FindRiskAnalysisQueryValidation :  AbstractValidator<FindRiskAnalysisQuery>
{
    public FindRiskAnalysisQueryValidation()
    {
        When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
        RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
    }
}