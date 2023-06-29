using AxonsERP.Entities.Models;

namespace AxonsERP.Service.Contracts 
{
    public interface IGeneralDescService
    {
        IEnumerable<GeneralDesc> GetListGeneralDesc();
    }
}