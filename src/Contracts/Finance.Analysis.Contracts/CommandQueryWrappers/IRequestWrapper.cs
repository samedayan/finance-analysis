using MediatR;

namespace Finance.Analysis.Contracts.CommandQueryWrappers;

public interface IRequestWrapper<out T> : IRequest<T>
{
}