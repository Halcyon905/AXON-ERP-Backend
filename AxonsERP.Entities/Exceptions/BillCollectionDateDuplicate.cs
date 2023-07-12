using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class BillCollectionDateDuplicateException : BadRequestException
    {
        public BillCollectionDateDuplicateException()
        : base("Bill Collection Date: Dates for this customer already exists in database.")
        {

        }

        private BillCollectionDateDuplicateException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}