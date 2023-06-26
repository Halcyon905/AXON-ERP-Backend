using AxonsERP.Entities.Models;
using System.Collections.Generic;

namespace AxonsERP.Contracts 
{
    public interface ITaxRateControlRepository 
    {
        void CreateTaxRateControl(TaxRateControl _TaxRateControl);
        void UpdateTaxRateControl(TaxRateControl _TaxRateControl);
        void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList);
        List<TaxRateControl> GetListTaxRateControl();
        TaxRateControl GetSingleTaxRateControl(TaxRateControl _TaxRateControl);
    }
}