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

        private string selectQuery = @"SUBSTR(L.OPERATION_CODE, 6, 10) AS operationCode,
                                        GDO.DESC1 AS operationDesc1,
                                        GDO.DESC2 AS operationDesc2,
                                        SUBSTR(L.SUB_OPERATION, 6, 10) AS subOperationCode,
                                        GDS.DESC1 AS subOperationDesc1,
                                        GDS.DESC2 AS subOperationDesc2,
                                        SUBSTR(L.BUSINESS_TYPE, 6, 10) AS businessType,
                                        GDB.DESC1 AS businessTypeDesc1,
                                        GDB.DESC2 AS businessTypeDesc2,
                                        SUBSTR(L.GROUP_ACCOUNT, 6, 10) AS groupAccount,
                                        GDG.DESC1 AS groupAccountDesc1,
                                        GDG.DESC2 AS groupAccountDesc2,
                                        SUBSTR(L.DOC_TYPE, 6, 10) AS docType,
                                        GDD.DESC1 AS docTypeDesc1,
                                        GDD.DESC2 AS docTypeDesc2,
                                        L.TRN_CODE AS trnCode,    
                                        TRN.NAME_LOCAL AS trnNameLocal,
                                        TRN.NAME_ENG AS trnNameEng,
                                        L.POST_FLAG AS postFlag,
                                        L.ACCOUNT_CODE1 AS accountCode1,
                                        C1.AC_NAME_LOCAL AS accountCodeNameLocal1,
                                        C1.AC_NAME_ENG AS accountCodeNameEng1,
                                        L.DEBIT_CREDIT_#1 AS type1,
                                        L.ACCOUNT_CODE2 AS accountCode2,    
                                        C2.AC_NAME_LOCAL AS accountCodeNameLocal2,
                                        C2.AC_NAME_ENG AS accountCodeNameEng2,
                                        L.DEBIT_CREDIT_#2 AS type2,
                                        L.EFFECTIVE_DATE as effectiveDate,
                                        L.COMPANY as company
                                    FROM CONVERT_TO_GL L
                                        INNER JOIN GENERAL_DESC GDO
                                        ON GDO.GDCODE = L.OPERATION_CODE
                                        INNER JOIN GENERAL_DESC GDS
                                        ON GDS.GDCODE = L.SUB_OPERATION
                                        INNER JOIN GENERAL_DESC GDB
                                        ON GDB.GDCODE = L.BUSINESS_TYPE
                                        INNER JOIN GENERAL_DESC GDG
                                        ON GDG.GDCODE = L.GROUP_ACCOUNT
                                        INNER JOIN GENERAL_DESC GDD
                                        ON GDD.GDCODE = L.DOC_TYPE
                                        INNER JOIN TRN_DESC TRN
                                        ON TRN.TRN_CODE = L.TRN_CODE AND TRN.DOC_TYPE = L.DOC_TYPE
                                        LEFT OUTER JOIN COMPANY_ACC_CHART C1
                                        ON C1.AC_CODE = L.ACCOUNT_CODE1
                                        LEFT OUTER JOIN COMPANY_ACC_CHART C2
                                        ON C2.AC_CODE = L.ACCOUNT_CODE2 ";
        
        public IEnumerable<ConvertToGL> GetListConvertToGL()
        {
            var ConvertToGLList = Connection.Query<ConvertToGL>(@"SELECT " + selectQuery + @"ORDER BY
                                                                                            L.COMPANY,
                                                                                            L.OPERATION_CODE,
                                                                                            L.SUB_OPERATION,
                                                                                            L.BUSINESS_TYPE,
                                                                                            L.GROUP_ACCOUNT,
                                                                                            L.DOC_TYPE,
                                                                                            L.TRN_CODE");
        return ConvertToGLList;
        }

        public ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle)
        {
            string query = @"SELECT CD.NAME_LOCAL as companyNameLocal,
                                    CD.NAME_ENG as companyNameEng, " +
                                selectQuery + @"INNER JOIN COMPANY CD 
                                                ON CD.COMPANY = L.COMPANY " +
                            @"WHERE
                                L.COMPANY = :company AND
                                L.OPERATION_CODE = :operationCode AND
                                L.SUB_OPERATION = :subOperation AND
                                L.BUSINESS_TYPE = :businessType AND
                                L.DOC_TYPE = :docType AND
                                L.TRN_CODE = :trnCode AND
                                L.GROUP_ACCOUNT = :groupAccount AND
                                L.EFFECTIVE_DATE = :effectiveDate
                            ORDER BY
                                L.COMPANY,
                                L.OPERATION_CODE,
                                L.SUB_OPERATION,
                                L.BUSINESS_TYPE,
                                L.GROUP_ACCOUNT,
                                L.DOC_TYPE,
                                L.TRN_CODE";
            var convertToGL = Connection.QueryFirstOrDefault<ConvertToGL>(query, convertToGLForGetSingle);
            return convertToGL;
        }

        public IEnumerable<ConvertToGL> SearchConvertToGL(ConvertToGLParameters parameters)
        {
            /// SEARCH
            var condition = "";
            var dynParams = new OracleDynamicParameters();
            if ((parameters.Search != null) || (!string.IsNullOrEmpty(parameters.SearchTermName) && !string.IsNullOrEmpty(parameters.SearchTermValue)))
            {
                var whereCause = QueryBuilder.
                    CreateWhereQuery<ConvertToGLForColumnSearchFilter, ConvertToGLForColumnSearchTerm>
                    (parameters.Search,parameters.SearchTermAlias, parameters.SearchTermName, parameters.SearchTermValue, ref dynParams);
                if(whereCause != "") {
                    condition += "WHERE " + whereCause;
                }
            }

            // ORDER BY
            var orderBy = "";
            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                orderBy = QueryBuilder.CreateOrderQuery<ConvertToGL>(orderByQueryString: parameters.OrderBy, 'L');
                orderBy = $" {(!string.IsNullOrEmpty(orderBy) ? "ORDER BY " + orderBy : "")}";
            }

            // PAGING
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var paging = "OFFSET :skip ROWS FETCH NEXT :take ROWS ONLY";

            // SQL QUERY
            var query = @$"BEGIN OPEN :rslt1 FOR SELECT COUNT(L.COMPANY) FROM CONVERT_TO_GL L {condition};
                                 OPEN :rslt2 FOR SELECT " + selectQuery + @$"{condition}
                                                 {orderBy} {paging};
                            END;";

            dynParams.Add(":rslt1", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":rslt2", OracleDbType.RefCursor, ParameterDirection.Output);
            dynParams.Add(":skip", OracleDbType.Int32, ParameterDirection.Input, skip);

           if(parameters.PageSize == 0) 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, GetListConvertToGL().Count());
            }
            else 
            {
                dynParams.Add(":take", OracleDbType.Int32, ParameterDirection.Input, parameters.PageSize);
            }

            using var multi = Connection.QueryMultiple(query, dynParams);
            var count = multi.ReadSingle<int>();
            var convertToGLList = multi.Read<ConvertToGL>().ToList();
            var results = new PagedList<ConvertToGL>(convertToGLList, count, parameters.PageNumber, parameters.PageSize);

            return results;
        }
        public void CreateConvertToGL(ConvertToGLForCreate convertToGLForCreate)
        {
            string queryCol = @"INSERT INTO CONVERT_TO_GL(COMPANY,
                                                        OPERATION_CODE,
                                                        SUB_OPERATION,
                                                        BUSINESS_TYPE,
                                                        DOC_TYPE,
                                                        TRN_CODE,
                                                        GROUP_ACCOUNT,
                                                        EFFECTIVE_DATE,
                                                        OWNER,
                                                        CREATE_DATE,
                                                        LAST_UPDATE_DATE,
                                                        POST_FLAG";
            string queryValues = @"VALUES(:company,
                                            :operationCode,
                                            :subOperation,
                                            :businessType,
                                            :docType,
                                            :trnCode,
                                            :groupAccount,
                                            :effectiveDate,
                                            :owner,
                                            :createDate,
                                            :lastUpdateDate,
                                            :postFlag";

            if(convertToGLForCreate.postFlag == "1" || convertToGLForCreate.postFlag == "3") {
                queryCol += ", ACCOUNT_CODE1, DEBIT_CREDIT_#1";
                queryValues += ", :accountCode1, :type1";
            }
            if(convertToGLForCreate.postFlag == "2" || convertToGLForCreate.postFlag == "3") {
                queryCol += ", ACCOUNT_CODE2, DEBIT_CREDIT_#2";
                queryValues += ", :accountCode2, :type2";
            }
            queryCol += ") ";
            queryValues += ")";

            Connection.Execute(queryCol + queryValues, convertToGLForCreate);
        }
        public void UpdateConvertToGL(ConvertToGLForUpdate convertToGLForUpdate)
        {
            string query = @"UPDATE CONVERT_TO_GL SET
                                            POST_FLAG=:postFlag,
                                            ACCOUNT_CODE1=:accountCode1, 
                                            DEBIT_CREDIT_#1=:type1,
                                            ACCOUNT_CODE2=:accountCode2, 
                                            DEBIT_CREDIT_#2=:type2,
                                            LAST_UPDATE_DATE=:lastUpdateDate,
                                            FUNCTION=:function
                                        WHERE
                                            COMPANY=:company AND
                                            OPERATION_CODE=:operationCode AND
                                            SUB_OPERATION=:subOperation AND
                                            BUSINESS_TYPE=:businessType AND
                                            DOC_TYPE=:docType AND
                                            TRN_CODE=:trnCode AND
                                            GROUP_ACCOUNT=:groupAccount AND
                                            EFFECTIVE_DATE=:effectiveDate";
            Connection.Execute(query, convertToGLForUpdate);
        }
        public void DeleteConvertToGL(ConvertToGLForGetSingle convertToGLForDelete)
        {
            string query = @"DELETE FROM CONVERT_TO_GL
                            WHERE COMPANY=:company AND
                                    OPERATION_CODE=:operationCode AND
                                    SUB_OPERATION=:subOperation AND
                                    BUSINESS_TYPE=:businessType AND
                                    DOC_TYPE=:docType AND
                                    TRN_CODE=:trnCode AND
                                    GROUP_ACCOUNT=:groupAccount AND
                                    EFFECTIVE_DATE=:effectiveDate";
            Connection.Execute(query, convertToGLForDelete);
        }
    }
}