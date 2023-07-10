using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
using System;

namespace AxonsERP.Contracts 
{
    public interface IBillCollectionDateRepository 
    {
        IEnumerable<BillCollectionDateToReturn> GetAllBillCollectionDate();
        IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters);
        IEnumerable<BillCollectionDate> GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle);
        void UpdateBillCollectionDate(BillCollectionDateForUpdate billCollectionDateForUpdate);
        void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany);
        void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete);
    }
}