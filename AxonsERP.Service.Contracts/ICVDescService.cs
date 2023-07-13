using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ICVDescService
    {
        IEnumerable<CustomerInfo> SearchCustomerInfo(CVDescParameters parameters);
    }
}