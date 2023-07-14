using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using System;

namespace AxonsERP.Contracts 
{
    public interface ICreditControlRepository 
    {
        CreditControl GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle);
    }
}