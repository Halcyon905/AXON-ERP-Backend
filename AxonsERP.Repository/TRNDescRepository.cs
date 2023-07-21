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
    internal class TRNDescRepository : RepositoryBase, ITRNDescRepository
    {
        internal TRNDescRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<TRNDesc> GetListTRNDesc()
        {
            string query = @"SELECT DOC_TYPE as docType,
                                    TRN_CODE as trnCode,
                                    NAME_LOCAL as nameLocal,
                                    NAME_ENG as nameEng
                            FROM TRN_DESC";

            var trnDescList = Connection.Query<TRNDesc>(query);
            return trnDescList;
        }
        public IEnumerable<TRNDesc> SearchTRNDesc(TRNDescParameters parameters)
        {
            /// SEARCH
            var condition = "";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<TRNDescForColumnSearchFilter, TRNDescForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                condition += "WHERE " + whereCause;
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<TRNDesc>(orderByQueryString: parameters.OrderBy, 'T');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(DOC_TYPE) FROM TRN_DESC T {condition};
                                 OPEN :rslt2 FOR SELECT SUBSTR(T.DOC_TYPE, 6, 10) as docType,
                                                        T.TRN_CODE as trnCode,
                                                        T.NAME_LOCAL as nameLocal,
                                                        T.NAME_ENG as nameEng
                                                FROM TRN_DESC T {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListTRNDesc().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var tRNDescs = multi.Read<TRNDesc>().ToList();
            var results = new PagedList<TRNDesc>(tRNDescs, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}