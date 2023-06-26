using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using System;

namespace AxonsERP.Contracts 
{
    public interface ITaxRateControlRepository 
    {
        void CreateTaxRateControl(TaxRateControl _TaxRateControl);
        void UpdateTaxRateControl(TaxRateControl _TaxRateControl);
        void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList);
        IEnumerable<TaxRateControl> GetListTaxRateControl();
        TaxRateControl GetSingleTaxRateControl(string taxCode, DateTime effectiveDate);
    }
}