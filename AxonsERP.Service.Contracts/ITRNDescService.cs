using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ITRNDescService
    {
        IEnumerable<TRNDesc> GetListTRNDesc();
        IEnumerable<TRNDesc> SearchTRNDesc(TRNDescParameters parameters);
    }
}