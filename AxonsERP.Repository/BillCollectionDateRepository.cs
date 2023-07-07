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