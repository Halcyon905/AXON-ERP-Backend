using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service 
{
    public class ConvertToGLService : IConvertToGLService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public ConvertToGLService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<ConvertToGL> GetListConvertToGL() 
        {
            var resultRaw = _repositoryManager.ConvertToGLRepository.GetListConvertToGL();
            return resultRaw;
        }
    }
}