using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;

namespace AxonsERP.Service.Contracts 
{
    public interface ITaxRateControlService 
    {
        void CreateTaxRateControl(TaxRateControlDto _taxRateControlDto);
        void UpdateTaxRateControl(TaxRateControlDto _taxRateControlDto);
        void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList);
        IEnumerable<TaxRateControl> GetListTaxRateControl();
        TaxRateControl GetSingleTaxRateControl(string TaxCode, DateTime effectiveDate);
    }
}