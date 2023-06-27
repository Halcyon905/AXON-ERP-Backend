using AxonsERP.Service.Contracts;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.DataTransferObjects;

namespace AxonsERP.Service 
{
    public class TaxRateControlService : ITaxRateControlService 
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public TaxRateControlService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateTaxRateControl(TaxRateControlDto _TaxRateControl) {}
        public void UpdateTaxRateControl(TaxRateControlDto _TaxRateControl) {}
        public void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList) {}
        public IEnumerable<TaxRateControl> GetListTaxRateControl() 
        {
            var resultRaw = _repositoryManager.TaxRateControl.GetListTaxRateControl();
            return resultRaw;
        }
        public TaxRateControl GetSingleTaxRateControl(string taxCode, DateTime effectiveDate) 
        {
            var resultRaw = _repositoryManager.TaxRateControl.GetSingleTaxRateControl(taxCode, effectiveDate);
            return resultRaw;
        }
    }
}