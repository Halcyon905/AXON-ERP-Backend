using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Repository.Extensions;
using AxonsERP.Repository.Extensions.Utility;
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
        public void DeleteTaxRateControl(List<TaxRateControlForDelete> TaxRateControlList) 
        {
            string query = @"DELETE FROM TAX_RATE_CONTROL
                            WHERE TAX_CODE = :taxCode AND EFFECTIVE_DATE=:effectiveDate";
            foreach(var taxRateControl in TaxRateControlList) {
                Connection.Execute(query, taxRateControl, transaction: Transaction);
            }
        }
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
            return result;
        }

        public PagedList<TaxRateControl> SearchTaxRateControl(TaxRateControlParameters parameters) 
        {
            /// SEARCH
            var condition = "WHERE T.TAX_CODE = G.GDCODE ";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<TaxRateControlForColumnSearchFilter, TaxRateControlForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);

                if(!string.IsNullOrEmpty(whereCause)) 
                {
                    condition += "AND " + whereCause;
                }
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<TaxRateControl>(orderByQueryString: parameters.OrderBy, 'T');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }


            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(TAX_CODE) FROM TAX_RATE_CONTROL T, GENERAL_DESC G {condition};
                                 OPEN :rslt2 FOR SELECT T.TAX_CODE as taxCode,
                                                        T.EFFECTIVE_DATE as effectiveDate,
                                                        T.RATE as rate,
                                                        T.OWNER as owner,
                                                        T.CREATE_DATE as createDate,
                                                        T.LAST_UPDATE_DATE as lastUpdateDate,
                                                        T.FUNCTION as function,
                                                        T.RATE_ORIGINAL as rateOriginal,
                                                        G.DESC1 as desc1,
                                                        G.DESC2 as desc2
                                                 FROM TAX_RATE_CONTROL T, GENERAL_DESC G {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);
            
            if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListTaxRateControl().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }

            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var taxRateControls = multi.Read<TaxRateControl>().ToList();
            var results = new PagedList<TaxRateControl>(taxRateControls, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}