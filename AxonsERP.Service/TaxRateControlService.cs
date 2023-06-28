using AxonsERP.Service.Contracts;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.Exceptions;
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
            _taxRateControlForCreate.taxCode = "TXCOD" + _taxRateControlForCreate.taxCode;

            if(GetSingleTaxRateControl(_taxRateControlForCreate.taxCode, _taxRateControlForCreate.effectiveDate) != null) {
                throw new TaxRateControlDuplicateException(_taxRateControlForCreate.taxCode, _taxRateControlForCreate.effectiveDate);
            }

            var _taxRateControl = _mapper.Map<TaxRateControlDto>(_taxRateControlForCreate);

            _taxRateControl.createDate = DateTime.Now;
            _taxRateControl.lastUpdateDate = DateTime.Now;
            _taxRateControl.function = "A";

            var resultRaw = _repositoryManager.TaxRateControl.CreateTaxRateControl(_taxRateControl);
            if(resultRaw != null) {
                _repositoryManager.Commit();
            }

            return resultRaw;
        }
        public void UpdateTaxRateControl(TaxRateControlForUpdate _taxRateControlForUpdate) 
        {
            _taxRateControlForUpdate.taxCode = "TXCOD" + _taxRateControlForUpdate.taxCode;

            if(GetSingleTaxRateControl(_taxRateControlForUpdate.taxCode, _taxRateControlForUpdate.effectiveDate) == null) {
                throw new TaxRateControlNotFoundException(_taxRateControlForUpdate.taxCode, _taxRateControlForUpdate.effectiveDate);
            }

            var _taxRateControl = _mapper.Map<TaxRateControlDto>(_taxRateControlForUpdate);

            _taxRateControl.lastUpdateDate = DateTime.Now;
            _taxRateControl.function = "C";

            _repositoryManager.TaxRateControl.UpdateTaxRateControl(_taxRateControl);
            _repositoryManager.Commit();
        }
        public void DeleteTaxRateControl(List<TaxRateControlForDelete> taxRateControlList) 
        {
            foreach(var taxRateControl in taxRateControlList) {
                taxRateControl.taxCode = "TXCOD" + taxRateControl.taxCode;
            }

            _repositoryManager.TaxRateControl.DeleteTaxRateControl(taxRateControlList);
            _repositoryManager.Commit();
        }
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