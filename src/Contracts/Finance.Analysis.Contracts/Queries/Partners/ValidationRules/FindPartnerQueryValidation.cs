using Finance.Analysis.Contracts.Queries.Agreement.Search;
using Finance.Analysis.Contracts.Queries.Partners.Search;
using FluentValidation;

namespace Finance.Analysis.Contracts.Queries.Partners.ValidationRules;

public class FindPartnerQueryValidation :  AbstractValidator<FindPartnerQuery>
{
    public FindPartnerQueryValidation()
    {
        When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
        RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
    }
}