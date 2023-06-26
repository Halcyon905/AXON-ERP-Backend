using AxonsERP.Service.Contracts;
using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.DataTransferObjects;

namespace AxonsERP.Service 
{
    public class TaxRateControlService : ITaxRateControlService 
    {
        private readonly IRepositoryManager _repositoryManager;

        public TaxRateControlService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void CreateTaxRateControl(TaxRateControlDto _TaxRateControl) {}
        public void UpdateTaxRateControl(TaxRateControlDto _TaxRateControl) {}
        public void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList) {}
        public IEnumerable<TaxRateControl> GetListTaxRateControl() 
        {
            return _repositoryManager.TaxRateControl.GetListTaxRateControl();
        }
        public TaxRateControl GetSingleTaxRateControl(string taxCode, string effectiveDate) 
        {
            return null;
        }
    }
}