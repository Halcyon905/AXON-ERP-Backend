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
    internal class CreditControlRepository : RepositoryBase, ICreditControlRepository
    {
        internal CreditControlRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public CreditControl GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle)
        {
            string query = @"SELECT DISTINCT CC.CUSTOMER_CODE as customerCode,
                                             C.NAME_LOCAL as nameLocal,
                                             C.NAME_ENG as nameEng,
                                             CC.DEPARTMENT as departmentCode,
                                             G1.DESC1 as departmentDesc1,
                                             G1.DESC2 as departmentDesc2,
                                             CC.BILL_COL_CALCULATE as billColCalculate,
                                             G2.DESC1 as billColCalculateDesc1,
                                             G2.DESC2 as billColCalculateDesc2
                                             FROM CREDIT_CONTROL CC, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2
                                             WHERE CC.CUSTOMER_CODE = C.CV_CODE AND
                                                   CC.DEPARTMENT = G1.GDCODE AND
                                                   CC.BILL_COL_CALCULATE = G2.GDCODE AND
                                                   CC.CUSTOMER_CODE = :customerCode AND
                                                   CC.DEPARTMENT = :departmentCode AND
                                                   CC.BILL_COL_CALCULATE = :billColCalculate";
            var result = Connection.QueryFirstOrDefault<CreditControl>(query, creditControlForGetSingle);
            return result;
        }
        public IEnumerable<CreditControl> GetAllCreditControl()
        {
            string query = @"SELECT DISTINCT CC.CUSTOMER_CODE as customerCode,
                                             C.NAME_LOCAL as nameLocal,
                                             C.NAME_ENG as nameEng,
                                             CC.DEPARTMENT as departmentCode,
                                             G1.DESC1 as departmentDesc1,
                                             G1.DESC2 as departmentDesc2,
                                             CC.BILL_COL_CALCULATE as billColCalculate,
                                             G2.DESC1 as billColCalculateDesc1,
                                             G2.DESC2 as billColCalculateDesc2
                                             FROM CREDIT_CONTROL CC, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2
                                             WHERE CC.CUSTOMER_CODE = C.CV_CODE AND
                                                   CC.DEPARTMENT = G1.GDCODE AND
                                                   CC.BILL_COL_CALCULATE = G2.GDCODE AND
                                                   CC.BILL_COL_CALCULATE between 'BICAL5' AND 'BICAL6'";
            var result = Connection.Query<CreditControl>(query);
            return result;
        }
        public IEnumerable<CreditControl> SearchCreditControl(CreditControlParameters parameters)
        {
            /// SEARCH
            var condition = @"WHERE C.CUSTOMER_CODE = D.CV_CODE AND
                                    C.DEPARTMENT = G1.GDCODE AND
                                    C.BILL_COL_CALCULATE = G2.GDCODE AND
                                    C.BILL_COL_CALCULATE between 'BICAL5' AND 'BICAL6' ";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<CreditControlForColumnSearchFilter, CreditControlForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                if(whereCause != "") {
                    condition += "AND " + whereCause;
                }
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<CreditControl>(orderByQueryString: parameters.OrderBy, 'C');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(CUSTOMER_CODE) FROM CV_DESC D, CREDIT_CONTROL C, GENERAL_DESC G1, GENERAL_DESC G2 {condition};
                                 OPEN :rslt2 FOR SELECT C.CUSTOMER_CODE as customerCode,
                                                        D.NAME_LOCAL as nameLocal,
                                                        D.NAME_ENG as nameEng,
                                                        C.DEPARTMENT as departmentCode,
                                                        G1.DESC1 as departmentDesc1,
                                                        G1.DESC2 as departmentDesc2,
                                                        C.BILL_COL_CALCULATE as billColCalculate,
                                                        G2.DESC1 as billColCalculateDesc1,
                                                        G2.DESC2 as billColCalculateDesc2
                                                 FROM CV_DESC D, CREDIT_CONTROL C, GENERAL_DESC G1, GENERAL_DESC G2 {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetAllCreditControl().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }


            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var generalDescs = multi.Read<CreditControl>().ToList();
            var results = new PagedList<CreditControl>(generalDescs, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
    }
}