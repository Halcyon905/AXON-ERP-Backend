using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.Exceptions;

namespace AxonsERP.Service 
{
    public class TRNDescService : ITRNDescService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public TRNDescService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<TRNDesc> GetListTRNDesc()
        {
            var resultRaw = _repositoryManager.TRNDescRepository.GetListTRNDesc();
            return resultRaw;
        }
        public IEnumerable<TRNDesc> SearchTRNDesc(TRNDescParameters parameters)
        {
            var resultRaw = _repositoryManager.TRNDescRepository.SearchTRNDesc(parameters);
            return resultRaw;
        }
    }
}