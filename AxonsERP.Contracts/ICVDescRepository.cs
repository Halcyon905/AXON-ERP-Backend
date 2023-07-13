using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
using System;

namespace AxonsERP.Contracts 
{
    public interface ICVDescRepository 
    {
        IEnumerable<CustomerInfo> SearchCustomerInfo(CVDescParameters parameters);
        IEnumerable<CustomerInfo> GetAllCustomerInfo();
    }
}