using Finance.Analysis.Contracts.Queries.Partners.Search;
using FluentValidation;

namespace Finance.Analysis.Contracts.Queries.WorkItems.ValidationRules;

public class FindWorkItemQueryValidation :  AbstractValidator<FindPartnerQuery>
{
    public FindWorkItemQueryValidation()
    {
        When(x => x.Page.HasValue, () => { RuleFor(x => x.Page).GreaterThanOrEqualTo(0); });
        RuleFor(t => t.PageSize).NotNull().NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(10);
    }
}