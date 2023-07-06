using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Repository.Extensions.Utility;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using AxonsERP.Repository.Extensions;

namespace AxonsERP.Repository
{
    internal class BillCollectionDateRepository : RepositoryBase, IBillCollectionDateRepository
    {
        internal BillCollectionDateRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<BillCollectionDateToReturn> GetAllBillCollectionDate()
        {
            return null;
        }
        public BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle)
        {
            return null;
        }
    }
}