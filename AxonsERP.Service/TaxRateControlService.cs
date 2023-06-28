using AxonsERP.Service.Contracts;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.RequestFeatures;

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

        public TaxRateControl CreateTaxRateControl(TaxRateControlForCreate _taxRateControlForCreate) 
        {
            var _taxRateControl = _mapper.Map<TaxRateControlDto>(_taxRateControlForCreate);

            _taxRateControl.createDate = DateTime.Now;
            _taxRateControl.lastUpdateDate = DateTime.Now;
            _taxRateControl.function = "A";

            var resultRaw = _repositoryManager.TaxRateControl.CreateTaxRateControl(_taxRateControl);
            _repositoryManager.Commit();

            return resultRaw;
        }
        public void UpdateTaxRateControl(TaxRateControlForUpdate _taxRateControlForUpdate) 
        {
            var _taxRateControl = _mapper.Map<TaxRateControlDto>(_taxRateControlForUpdate);

            _taxRateControl.lastUpdateDate = DateTime.Now;
            _taxRateControl.function = "C";

            _repositoryManager.TaxRateControl.UpdateTaxRateControl(_taxRateControl);
            _repositoryManager.Commit();
        }
        public void DeleteTaxRateControl(List<Dictionary<string, object>> taxRateControlList) {}
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