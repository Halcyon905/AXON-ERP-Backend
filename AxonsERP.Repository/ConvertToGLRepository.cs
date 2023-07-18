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
    internal class ConvertToGLRepository : RepositoryBase, IConvertToGLRepository
    {
        internal ConvertToGLRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<ConvertToGL> GetListConvertToGL()
        {
            var generalDescList = Connection.Query<ConvertToGL>(@"SELECT
                                                                    SUBSTR(GL.OPERATION_CODE, 6, 10) AS operationCode,
                                                                    GDO.DESC1 AS operationDesc1,
                                                                    GDO.DESC2 AS operationDesc2,
                                                                    SUBSTR(GL.SUB_OPERATION, 6, 10) AS subOperationCode,
                                                                    GDS.DESC1 AS subOperationDesc1,
                                                                    GDS.DESC2 AS subOperationDesc2,
                                                                    SUBSTR(GL.BUSINESS_TYPE, 6, 10) AS businessType,
                                                                    GDB.DESC1 AS businessTypeDesc1,
                                                                    GDB.DESC2 AS businessTypeDesc2,
                                                                    SUBSTR(GL.GROUP_ACCOUNT, 6, 10) AS groupAccount,
                                                                    GDG.DESC1 AS groupAccountDesc1,
                                                                    GDG.DESC2 AS groupAccountDesc2,
                                                                    SUBSTR(GL.DOC_TYPE, 6, 10) AS docType,
                                                                    GDD.DESC1 AS docTypeDesc1,
                                                                    GDD.DESC2 AS docTypeDesc2,
                                                                    GL.TRN_CODE AS trnCode,    
                                                                    TRN.NAME_LOCAL AS trnNameLocal,
                                                                    TRN.NAME_ENG AS trnNameEng,
                                                                    GL.POST_FLAG AS postFlag,
                                                                    GL.ACCOUNT_CODE1 AS accountCode1,
                                                                    C1.AC_NAME_LOCAL AS accountCodeNameLocal1,
                                                                    C1.AC_NAME_ENG AS accountCodeNameEng1,
                                                                    GL.DEBIT_CREDIT_#1 AS type1,
                                                                    GL.ACCOUNT_CODE2 AS accountCode2,    
                                                                    C2.AC_NAME_LOCAL AS accountCodeNameLocal2,
                                                                    C2.AC_NAME_ENG AS accountCodeNameEng2,
                                                                    GL.DEBIT_CREDIT_#2 AS type2,
                                                                    GL.EFFECTIVE_DATE as effectiveDate,
                                                                    GL.COMPANY as company
                                                                FROM CONVERT_TO_GL GL
                                                                    INNER JOIN GENERAL_DESC GDO
                                                                    ON GDO.GDCODE = GL.OPERATION_CODE
                                                                    INNER JOIN GENERAL_DESC GDS
                                                                    ON GDS.GDCODE = GL.SUB_OPERATION
                                                                    INNER JOIN GENERAL_DESC GDB
                                                                    ON GDB.GDCODE = GL.BUSINESS_TYPE
                                                                    INNER JOIN GENERAL_DESC GDG
                                                                    ON GDG.GDCODE = GL.GROUP_ACCOUNT
                                                                    INNER JOIN GENERAL_DESC GDD
                                                                    ON GDD.GDCODE = GL.DOC_TYPE
                                                                    INNER JOIN TRN_DESC TRN
                                                                    ON TRN.TRN_CODE = GL.TRN_CODE AND TRN.DOC_TYPE = GL.DOC_TYPE
                                                                    LEFT OUTER JOIN COMPANY_ACC_CHART C1
                                                                    ON C1.AC_CODE = GL.ACCOUNT_CODE1
                                                                    LEFT OUTER JOIN COMPANY_ACC_CHART C2
                                                                    ON C2.AC_CODE = GL.ACCOUNT_CODE2
                                                                ORDER BY
                                                                    GL.COMPANY,
                                                                    GL.OPERATION_CODE,
                                                                    GL.SUB_OPERATION,
                                                                    GL.BUSINESS_TYPE,
                                                                    GL.GROUP_ACCOUNT,
                                                                    GL.DOC_TYPE,
                                                                    GL.TRN_CODE");
            return generalDescList;
        }

        public ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle)
        {
            return null;
        }
    }
}