using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class TaxRateControlDuplicateException : BadRequestException
    {
        public TaxRateControlDuplicateException(string taxCode, DateTime effectiveDate)
        : base("Tax code " + taxCode + " on date " + effectiveDate + " already exists in database.")
        {

        }

        private TaxRateControlDuplicateException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}