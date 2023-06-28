using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ITaxRateControlService
    {
        TaxRateControl CreateTaxRateControl(TaxRateControlForCreate _taxRateControlForCreate);
        void UpdateTaxRateControl(TaxRateControlForUpdate _taxRateControlForUpdate);
        void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList);
        IEnumerable<TaxRateControl> GetListTaxRateControl();
        TaxRateControl GetSingleTaxRateControl(string TaxCode, DateTime effectiveDate);
    }
}