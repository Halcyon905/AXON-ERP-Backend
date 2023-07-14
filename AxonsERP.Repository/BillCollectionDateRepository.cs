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
                                                                                        substr(B.DEPARTMENT, 6, 5) as departmentCode,
                                                                                        G1.DESC1 as departmentDesc1,
                                                                                        G1.DESC2 as departmentDesc2,
                                                                                        substr(B.BILL_COL_CALCULATE, 6, 5) as billColCalculate,
                                                                                        G2.DESC1 as billColCalculateDesc1,
                                                                                        G2.DESC2 as billColCalculateDesc2
                                                                                        FROM BILL_COL_INFO B, CV_DESC C, GENERAL_DESC G1, GENERAL_DESC G2
                                                                                        WHERE G1.GDCODE = B.DEPARTMENT AND C.CV_CODE = B.CUSTOMER_CODE AND G2.GDCODE = B.BILL_COL_CALCULATE");
            return billCollectionDateList;
        }
        public BillCollectionDateSingleToReturn GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDate)
        {
            string query = @"SELECT B.CUSTOMER_CODE as customerCode,
                            substr(B.DEPARTMENT, 6, 5) as departmentCode,
                            substr(B.BILL_COL_CALCULATE, 6, 5) as billColCalculate,
                            B.START_DATE as startDate,
                            B.END_DATE as endDate,
                            B.WEEK_NO as weekNo,
                            B.DAY_OF_WEEK as dayOfWeek
                            FROM BILL_COL_INFO B
                            WHERE B.CUSTOMER_CODE=:customerCode AND B.DEPARTMENT=:departmentCode AND B.BILL_COL_CALCULATE=:billColCalculate";
            if(billCollectionDate.billColCalculate == "BICAL5") {
                query += @" AND B.WEEK_NO = :dateOne AND B.DAY_OF_WEEK = :dateTwo";
            }
            else {
                query += @" AND B.START_DATE = :dateOne AND B.END_DATE = :dateTwo";
            }
            var result = Connection.QueryFirstOrDefault<BillCollectionDateSingleToReturn>(query, billCollectionDate);
            return result;
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
                                                        substr(B.DEPARTMENT, 6, 5) as departmentCode,
                                                        G1.DESC1 as departmentDesc1,
                                                        G1.DESC2 as departmentDesc2,
                                                        substr(B.BILL_COL_CALCULATE, 6, 5) as billColCalculate,
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
        public IEnumerable<BillCollectionDate> GetCompanyBillCollectionDate(BillCollectionDateForSingleCustomer billCollectionDateForSingle)
        {
            string query = @"SELECT B.CUSTOMER_CODE as customerCode,
                            C.NAME_LOCAL as nameLocal,
                            C.NAME_ENG as nameEng,
                            substr(B.DEPARTMENT, 6, 5) as departmentCode,
                            G1.DESC1 as departmentDesc1,
                            G1.DESC2 as departmentDesc2,
                            substr(B.BILL_COL_CALCULATE, 6, 5) as billColCalculate,
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
        public void UpdateBillCollectionDate(BillCollectionDateForUpdateDto billCollectionDateForUpdate)
        {
            string query = @"UPDATE BILL_COL_INFO B SET 
                             B.LAST_UPDATE_DATE=:lastUpdateDate,
                             B.FUNCTION=:function,";
            if(billCollectionDateForUpdate.billColCalculate == "BICAL5") {
                query += @"B.WEEK_NO=:newStartDate, 
                           B.DAY_OF_WEEK=:newEndDate
                           WHERE B.WEEK_NO = :startDate AND B.DAY_OF_WEEK = :endDate";
            }
            else {
                query += @"B.START_DATE=:newStartDate, 
                           B.END_DATE=:newEndDate
                           WHERE B.START_DATE = :startDate AND B.END_DATE = :endDate";
            }
            Connection.Execute(query, billCollectionDateForUpdate, transaction: Transaction);
        }
        public void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany)
        {
            string query = @"DELETE FROM BILL_COL_INFO
                            WHERE CUSTOMER_CODE=:customerCode AND BILL_COL_CALCULATE=:billColCalculate AND DEPARTMENT=:departmentCode";
            foreach(var billColInfo in billCollectionDateForDeleteMany) {
                Connection.Execute(query, billColInfo, transaction: Transaction);
            }
        }
        public void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete)
        {
            string query = @"DELETE FROM BILL_COL_INFO
                            WHERE CUSTOMER_CODE=:customerCode AND BILL_COL_CALCULATE=:billColCalculate AND DEPARTMENT=:departmentCode";

            if(billCollectionDateForDelete.billColCalculate == "BICAL5") {
                query += @" AND WEEK_NO = :startDate AND DAY_OF_WEEK = :endDate";
            }
            else {
                query += @" AND START_DATE = :startDate AND END_DATE = :endDate";
            }
            
            Connection.Execute(query, billCollectionDateForDelete, transaction: Transaction);
        }
        public void CreateBillCollectionDate(BillCollectionDateForCreateDto billCollectionDateForCreateDto)
        {
            string query = @"INSERT INTO BILL_COL_INFO(CUSTOMER_CODE, DEPARTMENT, BILL_COL_CALCULATE, START_DATE, END_DATE, WEEK_NO, DAY_OF_WEEK, OWNER, CREATE_DATE, LAST_UPDATE_DATE, FUNCTION)
                             VALUES(:customerCode, :departmentCode, :billColCalculate, :startDate, :endDate, :weekNo, :dayOfWeek, :owner, :createDate, :lastUpdateDate, :function)";
            Connection.Execute(query, billCollectionDateForCreateDto, transaction: Transaction);
        }
    }
}