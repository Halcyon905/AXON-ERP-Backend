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
    internal class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        internal CompanyRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<Company> GetListCompany()
        {
            string query = @"SELECT COMPANY as company,
                                    NAME_LOCAL as nameLocal,
                                    NAME_ENG as nameEng
                            FROM COMPANY";

            var trnDescList = Connection.Query<Company>(query);
            return trnDescList;
        }
        public IEnumerable<Company> SearchCompany(CompanyParameters parameters)
        {
            /// SEARCH
            var condition = "";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<CompanyForColumnSearchFilter, CompanyForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                condition += "WHERE " + whereCause;
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<Company>(orderByQueryString: parameters.OrderBy, 'C');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(C.COMPANY) FROM COMPANY C {condition};
                                 OPEN :rslt2 FOR SELECT C.COMPANY as company,
                                                        C.NAME_LOCAL as nameLocal,
                                                        C.NAME_ENG as nameEng
                                                FROM COMPANY C {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListCompany().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var companies = multi.Read<Company>().ToList();
            var results = new PagedList<Company>(companies, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}