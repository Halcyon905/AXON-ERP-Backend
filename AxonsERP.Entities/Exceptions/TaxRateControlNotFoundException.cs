using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions 
{
    public class TaxRateControlNotFoundException : NotFoundException
    {
        public TaxRateControlNotFoundException(string taxCode, DateTime effectiveDate)
        : base($"Tax Rate Control: code {taxCode} on date {effectiveDate} doesn't exist in the database.")
        {

        }

        private TaxRateControlNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}