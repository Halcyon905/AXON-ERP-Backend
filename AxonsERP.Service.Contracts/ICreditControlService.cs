using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ICreditControlService
    {
        CreditControl GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle);
        IEnumerable<CreditControl> SearchCreditControl(CreditControlParameters parameters);
    }
}