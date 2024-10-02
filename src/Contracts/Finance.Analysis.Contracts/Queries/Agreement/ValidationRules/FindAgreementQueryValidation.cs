using Finance.Analysis.Contracts.Queries.Agreement.Search;
using FluentValidation;

namespace Finance.Analysis.Contracts.Queries.Agreement.ValidationRules;

public class FindAgreementQueryValidation :  AbstractValidator<FindAgreementQuery>
{
    public FindAgreementQueryValidation()
    {
        When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
        RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
    }
}