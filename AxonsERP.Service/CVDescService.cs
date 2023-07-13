using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service 
{
    public class CVDescService : ICVDescService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CVDescService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<CustomerInfo> SearchCustomerInfo(CVDescParameters parameters)
        {
            var resultRaw = _repositoryManager.CVDescRepository.SearchCustomerInfo(parameters);
            return resultRaw;
        }
    }
}