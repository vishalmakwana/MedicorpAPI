using System.Runtime.Serialization;

namespace Medicorp.Core.CustomException
{
    public class OperationExecutionException : Exception
    {
        public OperationExecutionException()
        {
        }

        public OperationExecutionException(string message) : base(message)
        {
        }

        public OperationExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
