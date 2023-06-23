using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.Exceptions
{
    [Serializable]
    public sealed class GeneralDescDuplicateBadRequestException : BadRequestException
    {
        public GeneralDescDuplicateBadRequestException(string gdcode)
        : base($"Duplicate General Desc with id: {gdcode}.")
        {

        }

        private GeneralDescDuplicateBadRequestException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
