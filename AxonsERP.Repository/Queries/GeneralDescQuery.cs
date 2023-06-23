using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Repository.Queries
{
    public static class GeneralDescQuery
    {
        public const string GeneralDescColumn = @"g.GDCODE AS Gdcode,
                               SUBSTR(g.GDCODE,6,10) as GdcodeSubString,
                               SUBSTR(g.GDCODE,10,10) as GdcodeForUnitLevel,
                               g.GDTYPE AS Gdtype,
                               g.DESC1 AS Desc1, 
                               g.DESC2 AS Desc2,
                               g.DESC3 AS Desc3,
                               g.DESC4 AS Desc4,
                               g.DESC5 AS Desc5,
                               g.COND1 AS Cond1,
                               g.COND2 AS Cond2,
                               g.COND3 AS Cond3,
                               g.COND4 AS Cond4,
                               g.COND5 AS Cond5,
                               g.ENTRY_STATUS AS EntryStatus,
                               g.ENTRY_STATUS_DATE AS EntryStatusDate,
                               g.OWNER AS Owner,
                               g.CREATE_DATE AS CreateDate,
                               g.LAST_UPDATE_DATE AS LastUpdateDate,
                               g.FUNCTION AS Function,
                               g.DESC3_OLD AS Desc3Old,
                               g.DESC4_OLD AS Desc4Old,
                               g.DESC5_OLD AS Desc5Old,
                               g.OWNER_CREATE AS OwnerCreate";


        // GDTYPE = BKBRH
        public const string BKBRHColumn = @",b.GDCODE AS BnkGdcode, b.DESC1 AS BnkDesc1, b.DESc2 AS BnkDesc2";
        public const string BKBRHJoin = @"LEFT JOIN GENERAL_DESC b ON g.COND1 = b.COND1 AND b.GDTYPE = 'BNKCD'";
        public const string BKBRHSpiltOn = @"BnkGdcode";
        public const string BKBRHPropNameForJoin = "BankBranch";


        // GDTYPE = SPECD
        public const string SPECDColumn = @",s1.DESC1 AS Warehouse, s2.DESC1 AS SubWarehouse";
        public const string SPECDJoin = @" LEFT JOIN GENERAL_DESC s1 ON g.COND1 = SUBSTR(s1.GDCODE,6,10) AND s1.GDTYPE = 'OPRCD' AND (s1.COND2 = 'WHS' OR s1.COND5 = 'Y') 
                                           LEFT JOIN GENERAL_DESC s2 ON g.COND2 = SUBSTR(s2.GDCODE,6,10) AND s2.GDTYPE = 'OPRCD'";
        public const string SPECDSpiltOn = @"Warehouse";
        public const string SPECDPropNameForJoin = "SpecialCode";

        // GDTYPE = NTTYP
        public const string NTTYPColumn = @",ac.AC_NAME_LOCAL AS CompanyAccChartNameLocal,
                                                ac.AC_NAME_ENG AS CompanyAccChartNameEng,
                                                cu.NAME_LOCAL AS CurrencyNameLocal,
                                                cu.NAME_ENG AS CurrencyNameEng";
        public const string NTTYPJoin = @"LEFT JOIN COMPANY_ACC_CHART ac ON g.DESC3 = ac.AC_CODE
                                          LEFT JOIN CURRENCY cu ON g.COND2 = cu.CURRENCY_CODE";
        public const string NTTYPSpiltOn = @"CompanyAccChartNameLocal";
        public const string NTTYPPropNameForJoin = "NoteType";

        // GDTYPE = OPRCD
        // OPER
        public const string OPRCDOperWithLinkColumn = @",l.COMPANY as Company, l.OPERATION_CODE AS OperationCode";
        public const string OPRCDOperWithLinkJoin = @"INNER JOIN LINK_OPERATION L ON L.OPERATION_CODE = G.GDCODE";
        public const string OPRCDOperWithLinkSpiltOn = @"Company";
        public const string OPRCDOperWithLinkSpiltOn2 = "OperationCode";
        public const string OPRCDOperWithLinkPropNameForJoin = "Operation";

        // SUB
        public const string OPRCDSubWithLinkColumn = @",l.COMPANY as Company, l.SUB_OPERATION AS SubOperation";
        public const string OPRCDSubWithLinkJoin = @"INNER JOIN LINK_OPERATION L ON L.SUB_OPERATION = G.GDCODE";
        public const string OPRCDSubWithLinkSpiltOn = @"Company";
        public const string OPRCDSubWithLinkSpiltOn2 = "SubOperation";
        public const string OPRCDSubWithLinkPropNameForJoin = "SubOperation";
        // Program code = GBM23000
        public const string DepartmentInCustomerInfColumn = "";
        public const string DepartmentInCustomerInfJoin = @"INNER JOIN CUSTOMER_INF c ON g.GDCODE = c.department AND CUSTOMER_CODE = :CustomerCode";

        // GDTYPE = EMPLY and CheckType = SUP/SAL
        public const string EMPLYSupSalTypeColumn = "";
        public const string EMPLYSupTypeJoin = @"INNER JOIN GENERAL_DESC g2 ON g2.GDCODE = g.GDCODE AND g2.COND2 = 'SUP'";
        public const string EMPLYSalTypeJoin = @"INNER JOIN GENERAL_DESC g2 ON g2.GDCODE = g.GDCODE AND ( g2.COND2 = 'SUP' OR g2.COND2 = 'SAL' )";
        
        // GDTYPE = EMPLY
        public const string EMPLYColumn = @",u.LOCAL_NAME AS NameLocal, u.ENG_NAME AS NameEng";
        public const string EMPLYJoin = @"LEFT JOIN USER_ID u ON g.desc3 = u.USER_ID";
        public const string EMPLYSpiltOn = @"NameLocal";
        public const string EMPLYPropNameForJoin = "Employee";

        // GDTYPE = UNLVL
        public const string UNLVLColumn = @",SUBSTR(g.GDCODE,10,10)";

    }
}
