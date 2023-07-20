using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

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

        public IEnumerable<GeneralDesc> GetListGeneralDesc(string codeType) 
        {
            var resultRaw = _repositoryManager.GeneralDescRepository.GetListGeneralDesc(codeType);
            return resultRaw;
        }

        public PagedList<GeneralDesc> SearchGeneralDesc(GeneralDescParameters parameters)
        {
            if(parameters.Search != null) {
                foreach(var searchObject in parameters.Search) {
                    if(searchObject.Name.Equals("gdCode", StringComparison.InvariantCultureIgnoreCase)) {
                        searchObject.Value = parameters.codeType + searchObject.Value;
                    }
                }
            }
            var resultRaw = _repositoryManager.GeneralDescRepository.SearchGeneralDesc(parameters);
            return resultRaw;
        }
    }
}