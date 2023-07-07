using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class BillCollectionDateNotFoundException : NotFoundException
    {
        public BillCollectionDateNotFoundException(string customerCode, string billColCalculate)
        : base($"Bill Collection Date: code {customerCode} with {billColCalculate} doesn't exist in the database.")
        {

        }

        private BillCollectionDateNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}