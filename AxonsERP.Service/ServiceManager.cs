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
        private readonly Lazy<ICVDescService> _cvDescService;
        private readonly Lazy<ICreditControlService> _creditControlService;
        private readonly Lazy<IConvertToGLService> _convertToGLService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper) 
        {
            _taxRateControlService = new Lazy<ITaxRateControlService>(() => new TaxRateControlService(repositoryManager, mapper));
            _generalDescService = new Lazy<IGeneralDescService>(() => new GeneralDescService(repositoryManager, mapper));
            _billCollectionDateService = new Lazy<IBillCollectionDateService>(() => new BillCollectionDateService(repositoryManager, mapper));
            _cvDescService = new Lazy<ICVDescService>(() => new CVDescService(repositoryManager, mapper));
            _creditControlService = new Lazy<ICreditControlService>(() => new CreditControlService(repositoryManager, mapper));
            _convertToGLService = new Lazy<IConvertToGLService>(() => new ConvertToGLService(repositoryManager, mapper));
        }

        public ITaxRateControlService TaxRateControlService => _taxRateControlService.Value;
        public IGeneralDescService GeneralDescService => _generalDescService.Value;
        public IBillCollectionDateService BillCollectionDateService => _billCollectionDateService.Value;
        public ICVDescService CVDescService => _cvDescService.Value;
        public ICreditControlService CreditControlService => _creditControlService.Value;
        public IConvertToGLService ConvertToGLService => _convertToGLService.Value;
    }
}