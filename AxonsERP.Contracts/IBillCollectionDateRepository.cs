using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
using System;

namespace AxonsERP.Contracts 
{
    public interface IBillCollectionDateRepository 
    {
        IEnumerable<BillCollectionDateToReturn> GetAllBillCollectionDate();
        BillCollectionDateSingleToReturn GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDate);
        IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters);
        IEnumerable<BillCollectionDate> GetCompanyBillCollectionDate(BillCollectionDateForSingleCustomer billCollectionDateForSingle);
        void UpdateBillCollectionDate(BillCollectionDateForUpdateDto billCollectionDateForUpdate);
        void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany);
        void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete);
        void CreateBillCollectionDate(BillCollectionDateForCreateDto billCollectionDateForCreateDto);
    }
}