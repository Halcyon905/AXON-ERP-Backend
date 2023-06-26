using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class TaxRateControlNotFoundException : NotFoundException
    {
        public TaxRateControlNotFoundException(string taxCode, string effectiveDate)
        : base($"Tax Rate Control: {taxCode} on date {effectiveDate} doesn't exist in the database.")
        {

        }

        private TaxRateControlNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}