using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace AxonsERP.Repository 
{
    internal class TaxRateControlRepository : RepositoryBase, ITaxRateControlRepository
    {
        internal TaxRateControlRepository(IDbTransaction transaction)
               : base(transaction)
        {

        }

        public void CreateTaxRateControl(TaxRateControl _TaxRateControl) {}
        public void UpdateTaxRateControl(TaxRateControl _TaxRateControl) {}
        public void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList) {}
        public IEnumerable<TaxRateControl> GetListTaxRateControl() 
        {
            var result = Connection.Query<TaxRateControl>(@"SELECT * FROM TAX_RATE_CONTROL");
            return result;
        }
        public TaxRateControl GetSingleTaxRateControl(string taxCode, DateTime effectiveDate) 
        {
            var result = Connection.QueryFirstOrDefault<TaxRateControl>(@"SELECT * FROM TAX_RATE_CONTROL 
                                                                        WHERE TAX_CODE = :taxCode AND EFFECTIVE_DATE = :effectiveDate", 
                                                                        new { taxCode, effectiveDate });
            return result;
        }
    }
}