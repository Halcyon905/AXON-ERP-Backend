using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.DataTransferObjects;
using System;

namespace AxonsERP.Contracts 
{
    public interface ITaxRateControlRepository 
    {
        TaxRateControl CreateTaxRateControl(TaxRateControlDto _taxRateControl);
        void UpdateTaxRateControl(TaxRateControlDto _taxRateControl);
        void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList);
        IEnumerable<TaxRateControl> GetListTaxRateControl();
        TaxRateControl GetSingleTaxRateControl(string taxCode, DateTime effectiveDate);
    }
}