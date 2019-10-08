using Espades.Common.Containers;
using Espades.Common.Enumerators;
using System;

namespace Espades.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public RequestResult ExceptionResult { get; private set; }

        public BusinessException(string message) : base(message)
        {
            CreateResult(message);
        }

        public BusinessException(RequestResult requestResult)
        {
            ExceptionResult = requestResult;
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
            CreateResult(message);
        }

        public BusinessException()
        {
            CreateResult();
        }

        private void CreateResult(string message = "")
        {
            ExceptionResult = new RequestResult(StatusResult.Warning, new Message(message));
        }
    }
}
