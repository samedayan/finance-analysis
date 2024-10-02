using System.Runtime.Serialization;

namespace Finance.Analysis.Infrastructure.Exceptions;

[Serializable]    
public class InvalidRequestException : Exception
{
    public string ExceptionCode { get; set; }
        
    public InvalidRequestException()
    {
    }

    public InvalidRequestException(string message) : base(message)
    {
    }
        
    public InvalidRequestException(string message,string exceptionCode) : base(message)
    {
        ExceptionCode = exceptionCode;
    }

    public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}