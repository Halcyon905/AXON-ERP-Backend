using System.Runtime.Serialization;

namespace AxonsERP.Entities.Exceptions;

[Serializable]
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
        : base(message)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
