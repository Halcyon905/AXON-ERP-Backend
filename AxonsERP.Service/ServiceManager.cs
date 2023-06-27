using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Service.Contracts;
using AxonsERP.Service;

namespace AxonsERP.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITaxRateControlService> _taxRateControlService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper) 
        {
            _taxRateControlService = new Lazy<ITaxRateControlService>(() => new TaxRateControlService(repositoryManager, mapper));
        }

        public ITaxRateControlService TaxRateControlService => _taxRateControlService.Value;
    }
}