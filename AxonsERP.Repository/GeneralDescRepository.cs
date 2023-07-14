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
    internal class GeneralDescRepository : RepositoryBase, IGeneralDescRepository
    {
        internal GeneralDescRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<GeneralDesc> GetListGeneralDesc()
        {
            var generalDescList = Connection.Query<GeneralDesc>(@"SELECT GDCODE as gdCode,
                                                                DESC1 as desc1,
                                                                DESC2 as desc2
                                                                FROM GENERAL_DESC
                                                                WHERE GDTYPE = 'TXCOD'");
            return generalDescList;
        }

        public PagedList<GeneralDesc> SearchGeneralDesc(GeneralDescParameters parameters) 
        {
            /// SEARCH
            var condition = "WHERE GDTYPE = '" + parameters.codeType + "'";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<GeneralDescForColumnSearchFilter, GeneralDescForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);

                if(!string.IsNullOrEmpty(whereCause)) 
                {
                    condition += " AND " + whereCause;
                }
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<GeneralDesc>(orderByQueryString: parameters.OrderBy, 'G');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }


            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(GDCODE) FROM GENERAL_DESC G {condition};
                                 OPEN :rslt2 FOR SELECT G.GDCODE as gdCode,
                                                        G.DESC1 as desc1,
                                                        G.DESC2 as desc2
                                                 FROM GENERAL_DESC G {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListGeneralDesc().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }

            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var generalDescs = multi.Read<GeneralDesc>().ToList();
            var results = new PagedList<GeneralDesc>(generalDescs, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}