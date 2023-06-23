using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions
{
    [Serializable]
    public sealed class BankDescNotFoundException : NotFoundException
    {
          public BankDescNotFoundException(string ctrlRunningWithMetaData)
        : base($"Bank description with id: {ctrlRunningWithMetaData} doesn't exist in the database.")
        {

        }

        private BankDescNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}