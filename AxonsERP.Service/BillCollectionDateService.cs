using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;
using System.Reflection;

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
            BillCollectionDateToReturn _billCollectionDateToReturn = _mapper.Map<BillCollectionDateToReturn>(resultRaw.ToList()[0]);
            
            List<int> startList = new List<int>();
            List<int> endList = new List<int>();

            string nameStart, nameEnd;
            if(_billCollectionDateToReturn.billColCalculate == "BICAL5") {
                nameStart = "billCollectionDateWeekStart";
                nameEnd = "billCollectionDateWeekEnd";
            }
            else {
                nameStart = "billCollectionDateMonthStart";
                nameEnd = "billCollectionDateMonthEnd";
            }

            foreach(var obj in resultRaw) {
                startList.Add((int) obj.GetType().GetProperty(nameStart).GetValue(obj, null));
                endList.Add((int) obj.GetType().GetProperty(nameEnd).GetValue(obj, null));
            }
            _billCollectionDateToReturn.billCollectionDateStart = startList;
            _billCollectionDateToReturn.billCollectionDateEnd = endList;

            return _billCollectionDateToReturn;
        }
    }
}