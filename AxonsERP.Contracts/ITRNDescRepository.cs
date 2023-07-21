using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface ITRNDescRepository
    {
        IEnumerable<TRNDesc> GetListTRNDesc();
        IEnumerable<TRNDesc> SearchTRNDesc(TRNDescParameters parameters);
    }
}