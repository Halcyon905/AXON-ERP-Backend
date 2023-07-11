using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IBillCollectionDateService
    {
        IEnumerable<BillCollectionDateToReturn> GetListBillCollectionDate();
        BillCollectionDateSingleToReturn GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDate);
        IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters);
        BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingleCustomer billCollectionDateForSingle);
        void UpdateBillCollectionDate(BillCollectionDateForUpdate billCollectionDateForUpdate);
        void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany);
        void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete);
        BillCollectionDateForGetSingle CreateBillCollectionDate(BillCollectionDateForCreate billCollectionDateForCreate);
    }
}