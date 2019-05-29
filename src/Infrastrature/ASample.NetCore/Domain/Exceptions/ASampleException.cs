using System;

namespace ASample.NetCore.Domain.Exceptions
{
    public class ASampleException:Exception
    {
        public string Code { get; }

        public ASampleException()
        {
        }

        public ASampleException(string code)
        {
            Code = code;
        }

        public ASampleException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public ASampleException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public ASampleException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public ASampleException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
