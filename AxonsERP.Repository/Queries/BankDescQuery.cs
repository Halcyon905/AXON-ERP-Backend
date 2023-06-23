namespace AxonsERP.Repository.Queries
{
    public static class BankDescQuery
    {
        public const string BankDescColumns =
            @"BANK_CODE AS BankCode, 
            NAME_LOCAL AS NameLocal, 
            NAME_ENG AS NameEng, 
            ACCOUNT_NO AS AccountNo, 
            ACCOUNT_CODE AS AccountCode, 
            CURRENCY AS Currency, 
            ADDRESS1 AS Address1, 
            ADDRESS2 AS Address2, 
            DEFAULT_FLAG AS DefaultFlag, 
            COMPANY AS Company, 
            NO_BOOK AS NoBook, 
            NAME_BOOK AS NameBook, 
            ADDRESS_BOOK1 AS AddressBook1, 
            ADDRESS_BOOK2 AS AddressBook2, 
            OWNER AS Owner, 
            CREATE_DATE AS CreateDate,
            LAST_UPDATE_DATE AS LastUpdateDate, 
            FUNCTION AS Function, 
            ACCOUNT_CODE_OLD AS AccountCodeOld,
            ABBR_NAME AS AbbrName,
            STATUS AS Status,
            OWNER_CREATE AS OwnerCreate,
            LOGO_BANK_NAME AS LogoBankName,
            ACCOUNT_NO_BANK AS AccountNoBank";

        public const string BankDescColumnsWithAlias =
            @"b.BANK_CODE AS BankCode, 
            b.NAME_LOCAL AS NameLocal, 
            b.NAME_ENG AS NameEng, 
            b.ACCOUNT_NO AS AccountNo, 
            b.ACCOUNT_CODE AS AccountCode, 
            b.CURRENCY AS Currency, 
            b.ADDRESS1 AS Address1, 
            b.ADDRESS2 AS Address2, 
            b.DEFAULT_FLAG AS DefaultFlag, 
            b.COMPANY AS Company, 
            b.NO_BOOK AS NoBook, 
            b.NAME_BOOK AS NameBook, 
            b.ADDRESS_BOOK1 AS AddressBook1, 
            b.ADDRESS_BOOK2 AS AddressBook2, 
            b.OWNER AS Owner, 
            b.CREATE_DATE AS CreateDate,
            b.LAST_UPDATE_DATE AS LastUpdateDate, 
            b.FUNCTION AS Function, 
            b.ACCOUNT_CODE_OLD AS AccountCodeOld,
            b.ABBR_NAME AS AbbrName,
            b.STATUS AS Status,
            b.OWNER_CREATE AS OwnerCreate,
            b.LOGO_BANK_NAME AS LogoBankName,
            b.ACCOUNT_NO_BANK AS AccountNoBank";

        public const string BankDescWithBankDayJoinColumns = @"d.CLOSING_BALANCE AS ClosingBalance";
        public const string BankDescWithBankDayJoinTables =
            @"LEFT JOIN BANK_STATEMENT_DAY d
                ON b.COMPANY = d.COMPANY AND 
                   b.BANK_CODE = d.BANK_CODE AND 
                   b.ACCOUNT_NO_BANK = d.ACCOUNT_NO AND 
                   TRANSACTION_DATE = (SELECT MAX(TRANSACTION_DATE) FROM BANK_STATEMENT_DAY
                                       WHERE d.COMPANY = COMPANY AND d.BANK_CODE = BANK_CODE AND d.ACCOUNT_NO = ACCOUNT_NO)";
    }
}
