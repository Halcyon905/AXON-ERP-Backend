using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.Exceptions
{
    [Serializable]

    public sealed class ControlAccountLengthNotFoundException : NotFoundException
    {
        public ControlAccountLengthNotFoundException()
      : base($"Control Account Length is not set.")
        {

        }

        private ControlAccountLengthNotFoundException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
