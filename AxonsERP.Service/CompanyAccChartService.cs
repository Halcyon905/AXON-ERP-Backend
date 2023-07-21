using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Entities.Exceptions;

namespace AxonsERP.Service 
{
    public class CompanyAccChartService : ICompanyAccChartService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CompanyAccChartService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<CompanyAccChart> GetListCompanyAccChart()
        {
            var resultRaw = _repositoryManager.CompanyAccChartRepository.GetListCompanyAccChart();
            return resultRaw;
        }
        public IEnumerable<CompanyAccChart> SearchCompanyAccChart(CompanyAccChartParameters parameters)
        {
            var resultRaw = _repositoryManager.CompanyAccChartRepository.SearchCompanyAccChart(parameters);
            return resultRaw;
        }
    }
}