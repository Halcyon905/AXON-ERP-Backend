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
            var result = Connection.Query<TaxRateControl>(@"SELECT T.TAX_CODE as taxCode,
                                                            T.EFFECTIVE_DATE as effectiveDate,
                                                            T.RATE as rate,
                                                            T.OWNER as owner,
                                                            T.CREATE_DATE as createDate,
                                                            T.LAST_UPDATE_DATE as lastUpdateDate,
                                                            T.FUNCTION as function,
                                                            T.RATE_ORIGINAL as rateOriginal
                                                            FROM TAX_RATE_CONTROL T");
            return result;
        }
        public TaxRateControl GetSingleTaxRateControl(string taxCode, DateTime effectiveDate) 
        {
            var result = Connection.QueryFirstOrDefault<TaxRateControl>(@"SELECT T.TAX_CODE as taxCode,
                                                                        T.EFFECTIVE_DATE as effectiveDate,
                                                                        T.RATE as rate,
                                                                        T.OWNER as owner,
                                                                        T.CREATE_DATE as createDate,
                                                                        T.LAST_UPDATE_DATE as lastUpdateDate,
                                                                        T.FUNCTION as function,
                                                                        T.RATE_ORIGINAL as rateOriginal 
                                                                        FROM TAX_RATE_CONTROL T
                                                                        WHERE TAX_CODE = :taxCode AND EFFECTIVE_DATE = :effectiveDate", 
                                                                        new { taxCode, effectiveDate });
            return result;
        }
    }
}