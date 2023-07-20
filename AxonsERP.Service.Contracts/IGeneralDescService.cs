using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IGeneralDescService
    {
        IEnumerable<GeneralDesc> GetListGeneralDesc(string codeType);
        PagedList<GeneralDesc> SearchGeneralDesc(GeneralDescParameters parameters);
    }
}