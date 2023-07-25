using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.Exceptions;

namespace AxonsERP.Service 
{
    public class LinkOperationService : ILinkOperationService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public LinkOperationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<LinkOperation> GetListLinkOperation(string mainColumn)
        {
            var resultRaw = _repositoryManager.LinkOperationRepository.GetListLinkOperation(mainColumn);
            return resultRaw;
        }
        public IEnumerable<LinkOperation> SearchLinkOperation(LinkOperationParameters parameters)
        {
            var resultRaw = _repositoryManager.LinkOperationRepository.SearchLinkOperation(parameters);
            return resultRaw;
        }
    }
}