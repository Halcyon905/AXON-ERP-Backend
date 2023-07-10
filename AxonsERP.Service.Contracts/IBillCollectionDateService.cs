using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IBillCollectionDateService
    {
        IEnumerable<BillCollectionDateToReturn> GetListBillCollectionDate();
        IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters);
        BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle);
        void UpdateBillCollectionDate(BillCollectionDateForUpdate billCollectionDateForUpdate);
        void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany);
        void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete);
    }
}