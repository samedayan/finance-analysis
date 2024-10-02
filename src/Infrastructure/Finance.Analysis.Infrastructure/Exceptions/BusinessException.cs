using System.Runtime.Serialization;

namespace Finance.Analysis.Infrastructure.Exceptions;

[Serializable]
public class BusinessException : Exception
{
    public const string BusinessExceptionCodeKey = "BusinessExceptionCode";
    public string ExceptionCode { get; set; }
    
    public BusinessException()
    {
    }

    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BusinessException(string message,string exceptionCode) : base(message)
    {
        ExceptionCode = exceptionCode;
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}