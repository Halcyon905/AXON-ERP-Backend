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
    internal class CVDescRepository : RepositoryBase, ICVDescRepository
    {
        internal CVDescRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<CustomerInfo> GetAllCustomerInfo()
        {
            string query = @"SELECT DISTINCT C.CV_CODE as gdCode,
                                             C.NAME_LOCAL as desc1,
                                             C.NAME_ENG as desc2
                                             FROM CV_DESC C";
            var result = Connection.Query<CustomerInfo>(query);
            return result;
        }
        
        public IEnumerable<CustomerInfo> SearchCustomerInfo(CVDescParameters parameters)
        {
            /// SEARCH
            var condition = "";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<CVDescForColumnSearchFilter, CVDescForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                condition += "WHERE " + whereCause;
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<CustomerInfo>(orderByQueryString: parameters.OrderBy, 'C');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(CV_CODE) FROM CV_DESC C {condition};
                                 OPEN :rslt2 FOR SELECT C.CV_CODE as gdCode,
                                                        C.NAME_LOCAL as desc1,
                                                        C.NAME_ENG as desc2
                                                 FROM CV_DESC C {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetAllCustomerInfo().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var generalDescs = multi.Read<CustomerInfo>().ToList();
            var results = new PagedList<CustomerInfo>(generalDescs, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}