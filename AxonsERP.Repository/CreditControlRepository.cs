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
    }
}