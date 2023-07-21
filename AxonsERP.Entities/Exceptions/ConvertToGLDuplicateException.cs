using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class ConvertToGLDuplicateException : BadRequestException
    {
        public ConvertToGLDuplicateException()
        : base("This set of convert to GL values already exists in the database.")
        {

        }

        private ConvertToGLDuplicateException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}