using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service 
{
    public class BillCollectionDateService : IBillCollectionDateService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public BillCollectionDateService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IEnumerable<BillCollectionDateToReturn> GetListBillCollectionDate()
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetAllBillCollectionDate();
            return resultRaw;
        }
        public BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingle billCollectionDateForSingle)
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetCompanyBillCollectionDate(billCollectionDateForSingle);
            return resultRaw;
        }
    }
}