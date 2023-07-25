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
    internal class LinkOperationRepository : RepositoryBase, ILinkOperationRepository
    {
        internal LinkOperationRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<LinkOperation> GetListLinkOperation(string mainColumn)
        {
            var LinkOperationList = Connection.Query<LinkOperation>(@$"SELECT distinct substr(GDCODE, 6, 5) as opCode,
                                                                        DESC1 as desc1,
                                                                        DESC2 as desc2
                                                                        FROM GENERAL_DESC G, LINK_OPERATION L
                                                                        WHERE G.GDCODE = L.{mainColumn}");
            return LinkOperationList;
        }

        public IEnumerable<LinkOperation> SearchLinkOperation(LinkOperationParameters parameters) 
        {
            /// SEARCH
            var condition = $"WHERE G.GDCODE = L.{parameters.mainColumn}";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null && parameters.Search.Count() != 0) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var generalWhereCause = QueryBuilder.
                    CreateWhereQuery<GeneralDescForColumnSearchFilter, GeneralDescForColumnSearchTerm>
                    (parameters.Search, "G", parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);

                if(!string.IsNullOrEmpty(generalWhereCause)) 
                {
                    condition += " AND " + generalWhereCause;
                }
                
                var operationWhereCause = QueryBuilder.
                    CreateWhereQuery<LinkOperationForColumnSearchFilter, LinkOperationForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);

                if(!string.IsNullOrEmpty(operationWhereCause)) 
                {
                    condition += " AND " + operationWhereCause;
                }
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<LinkOperation>(orderByQueryString: parameters.OrderBy, 'L');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }


            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(GDCODE) FROM GENERAL_DESC G, LINK_OPERATION L {condition};
                                 OPEN :rslt2 FOR SELECT distinct substr(GDCODE, 6, 5) as opCode,
                                                        G.DESC1 as desc1,
                                                        G.DESC2 as desc2
                                                 FROM GENERAL_DESC G, LINK_OPERATION L {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListLinkOperation(parameters.mainColumn).Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var linkOperations = multi.Read<LinkOperation>().ToList();
            var results = new PagedList<LinkOperation>(linkOperations, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}