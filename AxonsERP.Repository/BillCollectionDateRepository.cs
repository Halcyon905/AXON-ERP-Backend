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
    internal class BillCollectionDateRepository : RepositoryBase, IBillCollectionDateRepository
    {
        internal BillCollectionDateRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<BillCollectionDateToReturn> GetAllBillCollectionDate()
        {
            var billCollectionDateList = Connection.Query<BillCollectionDateToReturn>(@"SELECT DISTINCT B.CUSTOMER_CODE as customerCode,
                                                                                        C.NAME_LOCAL as nameLocal,
                                                                                        C.NAME_ENG as nameEng,
                                                                                        B.DEPARTMENT as departmentCode,
                                                                                        G1.DESC1 as departmentDesc1,
                                                                                        G1.DESC2 as departmentDesc2,
                                                                                        B.BILL_COL_CALCULATE as billColCalculate,
                                                                                        G2.DESC1 as billColCalculateDesc1,
                                                                                        G2.DESC2 as billColCalculateDesc2
                                                                                        FROM BILL_COL_INFO B, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2
                                                                                        WHERE G1.GDCODE = B.DEPARTMENT AND C.CV_CODE = B.CUSTOMER_CODE AND G2.GDCODE = B.BILL_COL_CALCULATE");
            return billCollectionDateList;
        }
        public IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters)
        {
            /// SEARCH
            var condition = "WHERE G1.GDCODE = B.DEPARTMENT AND C.CV_CODE = B.CUSTOMER_CODE AND G2.GDCODE = B.BILL_COL_CALCULATE ";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<BillCollectionDateForColumnSearchFilter, BillCollectionDateForColumnSearchTerm>
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
                orderBy = QueryBuilder.CreateOrderQuery<BillCollectionDateToReturn>(orderByQueryString: parameters.OrderBy, 'T');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }


            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(B.CUSTOMER_CODE) FROM BILL_COL_INFO B, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2 {condition};
                                 OPEN :rslt2 FOR SELECT DISTINCT B.CUSTOMER_CODE as customerCode,
                                                        C.NAME_LOCAL as nameLocal,
                                                        C.NAME_ENG as nameEng,
                                                        B.DEPARTMENT as departmentCode,
                                                        G1.DESC1 as departmentDesc1,
                                                        G1.DESC2 as departmentDesc2,
                                                        B.BILL_COL_CALCULATE as billColCalculate,
                                                        G2.DESC1 as billColCalculateDesc1,
                                                        G2.DESC2 as billColCalculateDesc2
                                                 FROM BILL_COL_INFO B, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2 {condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);
            
            if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetAllBillCollectionDate().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }

            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var billCollectionDates = multi.Read<BillCollectionDateToReturn>().ToList();
            var results = new PagedList<BillCollectionDateToReturn>(billCollectionDates, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
        public IEnumerable<BillCollectionDate> GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle)
        {
            string query = @"SELECT B.CUSTOMER_CODE as customerCode,
                            C.NAME_LOCAL as nameLocal,
                            C.NAME_ENG as nameEng,
                            B.DEPARTMENT as departmentCode,
                            G1.DESC1 as departmentDesc1,
                            G1.DESC2 as departmentDesc2,
                            B.BILL_COL_CALCULATE as billColCalculate,
                            G2.DESC1 as billColCalculateDesc1,
                            G2.DESC2 as billColCalculateDesc2,
                            B.START_DATE as billCollectionDateMonthStart,
                            B.END_DATE as billCollectionDateMonthEnd,
                            B.WEEK_NO as billCollectionDateWeekStart,
                            B.DAY_OF_WEEK as billCollectionDateWeekEnd
                            FROM BILL_COL_INFO B, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2
                            WHERE G1.GDCODE = B.DEPARTMENT 
                            AND C.CV_CODE = B.CUSTOMER_CODE
                            AND C.CV_CODE = :customerCode
                            AND G2.GDCODE = B.BILL_COL_CALCULATE
                            AND B.DEPARTMENT = :departmentCode
                            AND B.BILL_COL_CALCULATE = :billColCalculate";
            var billCollectionDateList = Connection.Query<BillCollectionDate>(query, billCollectionDateForSingle);
            return billCollectionDateList;
        }
    }
}