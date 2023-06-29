using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface IGeneralDescRepository
    {
        IEnumerable<GeneralDesc> GetListGeneralDesc();
    }
}