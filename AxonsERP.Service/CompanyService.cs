using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.Exceptions;

namespace AxonsERP.Service 
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CompanyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<Company> GetListCompany()
        {
            var resultRaw = _repositoryManager.CompanyRepository.GetListCompany();
            return resultRaw;
        }
        public IEnumerable<Company> SearchCompany(CompanyParameters parameters)
        {
            var resultRaw = _repositoryManager.CompanyRepository.SearchCompany(parameters);
            return resultRaw;
        }
    }
}