using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.Exceptions
{
    [Serializable]
    public sealed class BankDescDuplicateBadRequestException : BadRequestException
    {
        public BankDescDuplicateBadRequestException(string bankCode)
        :base($"Bank description with id: {bankCode} already exist in the database.")
        {

        }

        private BankDescDuplicateBadRequestException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
