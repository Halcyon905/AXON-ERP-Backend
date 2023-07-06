using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Service.Contracts;
using AxonsERP.Service;

namespace AxonsERP.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITaxRateControlService> _taxRateControlService;
        private readonly Lazy<IGeneralDescService> _generalDescService;
        private readonly Lazy<IBillCollectionDateService> _billCollectionDateService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper) 
        {
            _taxRateControlService = new Lazy<ITaxRateControlService>(() => new TaxRateControlService(repositoryManager, mapper));
            _generalDescService = new Lazy<IGeneralDescService>(() => new GeneralDescService(repositoryManager, mapper));
            _billCollectionDateService = new Lazy<IBillCollectionDateService>(() => new BillCollectionDateService(repositoryManager, mapper));
        }

        public ITaxRateControlService TaxRateControlService => _taxRateControlService.Value;
        public IGeneralDescService GeneralDescService => _generalDescService.Value;
        public IBillCollectionDateService BillCollectionDateService => _billCollectionDateService.Value;
    }
}