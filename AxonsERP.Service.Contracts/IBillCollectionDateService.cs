using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IBillCollectionDateService
    {
        IEnumerable<BillCollectionDateToReturn> GetListBillCollectionDate();
        BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle);
    }
}