using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class InsertErrorException : BadRequestException
    {
        public InsertErrorException(string message)
        : base(message)
        {

        }

        private InsertErrorException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}