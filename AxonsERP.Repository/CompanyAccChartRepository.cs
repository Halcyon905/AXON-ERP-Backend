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
    internal class CompanyAccChartRepository : RepositoryBase, ICompanyAccChartRepository
    {
        internal CompanyAccChartRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<CompanyAccChart> GetListCompanyAccChart()
        {
            string query = @"SELECT AC_CODE as acCode,
                                    AC_NAME_LOCAL as nameLocal,
                                    AC_NAME_ENG as nameEng
                            FROM COMPANY_ACC_CHART";

            var companyAccList = Connection.Query<CompanyAccChart>(query);
            return companyAccList;
        }
        public IEnumerable<CompanyAccChart> SearchCompanyAccChart(CompanyAccChartParameters parameters)
        {
            /// SEARCH
            var condition = "";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<CompanyAccChartForColumnSearchFilter, CompanyAccChartForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                condition += "WHERE " + whereCause;
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<CompanyAccChart>(orderByQueryString: parameters.OrderBy, 'C');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(AC_CODE) FROM COMPANY_ACC_CHART C {condition};
                                 OPEN :rslt2 FOR SELECT C.AC_CODE as acCode,
                                                        C.AC_NAME_LOCAL as nameLocal,
                                                        C.AC_NAME_ENG as nameEng
                                                 FROM COMPANY_ACC_CHART C {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListCompanyAccChart().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var companyAccCharts = multi.Read<CompanyAccChart>().ToList();
            var results = new PagedList<CompanyAccChart>(companyAccCharts, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}