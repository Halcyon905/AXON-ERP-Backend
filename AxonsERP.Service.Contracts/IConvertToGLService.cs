using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IConvertToGLService
    {
        IEnumerable<ConvertToGL> GetListConvertToGL();
    }
}