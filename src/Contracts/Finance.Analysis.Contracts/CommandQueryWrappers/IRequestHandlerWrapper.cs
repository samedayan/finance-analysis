using MediatR;

namespace Finance.Analysis.Contracts.CommandQueryWrappers;

public interface IRequestHandlerWrapper<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IRequestWrapper<TOut>
{
}