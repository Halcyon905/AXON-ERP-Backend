using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
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

        public TaxRateControl CreateTaxRateControl(TaxRateControlDto _taxRateControl) 
        {
            string query = @"INSERT INTO TAX_RATE_CONTROL(TAX_CODE, EFFECTIVE_DATE, RATE, OWNER, CREATE_DATE, LAST_UPDATE_DATE, FUNCTION, RATE_ORIGINAL)
                            VALUES (:taxCode, :effectiveDate, :rate, :owner, :createDate, :lastUpdateDate, :function, :rateOriginal)";

            Connection.Execute(query, _taxRateControl, transaction: Transaction);

            var result = GetSingleTaxRateControl(_taxRateControl.taxCode, _taxRateControl.effectiveDate);
            return result;
        }
        public void UpdateTaxRateControl(TaxRateControlDto _taxRateControl) 
        {
            string query = @"UPDATE TAX_RATE_CONTROL SET RATE=:rate, RATE_ORIGINAL=:rateOriginal, LAST_UPDATE_DATE=:lastUpdateDate, FUNCTION=:function 
                            WHERE TAX_CODE = :taxCode AND EFFECTIVE_DATE=:effectiveDate";

            Connection.Execute(query, _taxRateControl, transaction: Transaction);
        }
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
                                                            T.RATE_ORIGINAL as rateOriginal,
                                                            G.DESC1 as desc1, 
                                                            G.DESC2 as desc2
                                                            FROM TAX_RATE_CONTROL T, GENERAL_DESC G
                                                            WHERE T.TAX_CODE = G.GDCODE");
            foreach(var element in result) {
                element.taxCode = element.taxCode.Substring(5, element.taxCode.Length - 5);
            }                                                
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
                                                                        T.RATE_ORIGINAL as rateOriginal,
                                                                        G.DESC1 as desc1, 
                                                                        G.DESC2 as desc2
                                                                        FROM TAX_RATE_CONTROL T, GENERAL_DESC G
                                                                        WHERE TAX_CODE = :taxCode AND EFFECTIVE_DATE = :effectiveDate AND T.TAX_CODE = G.GDCODE", 
                                                                        new { taxCode, effectiveDate });
            
            if(result != null) {
                result.taxCode = result.taxCode.Substring(5, result.taxCode.Length - 5);
            }
            return result;
        }
    }
}