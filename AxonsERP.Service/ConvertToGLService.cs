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
        private Dictionary<string, List<string>> columnWithCodes = new Dictionary<string, List<string>>()
                                                                                                {
                                                                                                    {"OPRCD", new List<string>(){"operationCode", "subOperation", "businessType"}},
                                                                                                    {"DCTYP", new List<string>(){"docType"}},
                                                                                                    {"PRDAC", new List<string>(){"groupAccount"}}
                                                                                                };

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
        public ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle)
        {
            foreach(var prop in convertToGLForGetSingle.GetType().GetProperties())
            {
                foreach(var _list in columnWithCodes.Values)
                {
                    if(prop.GetValue(convertToGLForGetSingle) != null && _list.Contains(prop.Name)) {
                        prop.SetValue(convertToGLForGetSingle, columnWithCodes.FirstOrDefault(x => x.Value == _list).Key + prop.GetValue(convertToGLForGetSingle));
                    }
                }
            }
            var resultRaw = _repositoryManager.ConvertToGLRepository.GetSingleConvertToGL(convertToGLForGetSingle);
            return resultRaw;
        }
    }
}