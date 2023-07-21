using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ICompanyAccChartService
    {
        IEnumerable<CompanyAccChart> GetListCompanyAccChart();
        IEnumerable<CompanyAccChart> SearchCompanyAccChart(CompanyAccChartParameters parameters);
    }
}