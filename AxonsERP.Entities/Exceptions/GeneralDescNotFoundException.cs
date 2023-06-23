using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.Exceptions
{
    [Serializable]
    public sealed class GeneralDescNotFoundException : NotFoundException
    {
        public GeneralDescNotFoundException(string gdcode)
        : base($"General Desc with id: {gdcode} doesn't exist in the database.")
        {

        }

        private GeneralDescNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
