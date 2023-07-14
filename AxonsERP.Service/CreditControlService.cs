using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service
{
    public class CreditControlService : ICreditControlService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CreditControlService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public CreditControl GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle)
        {
            var resultRaw = _repositoryManager.CreditControlRepository.GetSingleCreditControl(creditControlForGetSingle);
            return resultRaw;
        }
        public IEnumerable<CreditControl> SearchCreditControl(CreditControlParameters parameters)
        {
            var resultRaw = _repositoryManager.CreditControlRepository.SearchCreditControl(parameters);
            return resultRaw;
        }
    }
}