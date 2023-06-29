using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;

namespace AxonsERP.Service 
{
    public class GeneralDescService : IGeneralDescService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public GeneralDescService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<GeneralDesc> GetListGeneralDesc() 
        {
            var resultRaw = _repositoryManager.GeneralDescRepository.GetListGeneralDesc();
            return resultRaw;
        }
    }
}