using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetListCompany();
        IEnumerable<Company> SearchCompany(CompanyParameters parameters);
    }
}