using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface ICompanyAccChartRepository
    {
        IEnumerable<CompanyAccChart> GetListCompanyAccChart();
        IEnumerable<CompanyAccChart> SearchCompanyAccChart(CompanyAccChartParameters parameters);
    }
}