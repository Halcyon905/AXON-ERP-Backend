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
    internal class CompanyAccChartRepository : RepositoryBase, ICompanyAccChartRepository
    {
        internal CompanyAccChartRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<CompanyAccChart> GetListCompanyAccChart()
        {
            string query = @"SELECT AC_CODE as acCode,
                                    AC_NAME_LOCAL as nameLocal,
                                    AC_NAME_ENG as nameEng
                            FROM COMPANY_ACC_CHART";

            var companyAccList = Connection.Query<CompanyAccChart>(query);
            return companyAccList;
        }
        public IEnumerable<CompanyAccChart> SearchCompanyAccChart(CompanyAccChartParameters parameters)
        {
            return null;
        }
    }
}