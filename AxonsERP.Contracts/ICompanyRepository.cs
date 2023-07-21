using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetListCompany();
        IEnumerable<Company> SearchCompany(CompanyParameters parameters);
    }
}