using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.Exceptions;
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
        public BillCollectionDateSingleToReturn GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDate)
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetSingleBillCollectionDate(billCollectionDate);
            return resultRaw;
        }
        public IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters)
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.SearchBillCollectionDate(parameters);
            return resultRaw;
        }
        public BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingleCustomer billCollectionDateForSingle)
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetCompanyBillCollectionDate(billCollectionDateForSingle);

            if(!resultRaw.Any()) {
                throw new BillCollectionDateNotFoundException(billCollectionDateForSingle.customerCode, billCollectionDateForSingle.billColCalculate);
            }

            BillCollectionDateToReturn _billCollectionDateToReturn = _mapper.Map<BillCollectionDateToReturn>(resultRaw.ToList()[0]);
            
            List<int> startList = new List<int>();
            List<int> endList = new List<int>();

            string nameStart, nameEnd;
            if(_billCollectionDateToReturn.billColCalculate == "BICAL5") {
                nameStart = "billCollectionDateWeekEnd";
                nameEnd = "billCollectionDateWeekStart";
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

        public void UpdateBillCollectionDate(BillCollectionDateForUpdate billCollectionDateForUpdate)
        {
            BillCollectionDateForSingleCustomer _billCollectionForSingle = new BillCollectionDateForSingleCustomer();

            _billCollectionForSingle.customerCode = billCollectionDateForUpdate.customerCode;
            _billCollectionForSingle.departmentCode = billCollectionDateForUpdate.departmentCode;
            _billCollectionForSingle.billColCalculate = billCollectionDateForUpdate.billColCalculate;

            var result = _repositoryManager.BillCollectionDateRepository.GetCompanyBillCollectionDate(_billCollectionForSingle);
            if(!result.Any()) {
                throw new BillCollectionDateNotFoundException(billCollectionDateForUpdate.customerCode, billCollectionDateForUpdate.billColCalculate);
            }

            BillCollectionDateForUpdateDto billCollectionDateForUpdateDto = _mapper.Map<BillCollectionDateForUpdateDto>(billCollectionDateForUpdate);

            billCollectionDateForUpdateDto.function = "C";
            billCollectionDateForUpdateDto.lastUpdateDate = DateTime.Now;

            _repositoryManager.BillCollectionDateRepository.UpdateBillCollectionDate(billCollectionDateForUpdateDto);
            _repositoryManager.Commit();
        }
        public void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany)
        {
            _repositoryManager.BillCollectionDateRepository.DeleteBillCollectionDateByCompany(billCollectionDateForDeleteMany);
            _repositoryManager.Commit();
        }
        public void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete)
        {
            _repositoryManager.BillCollectionDateRepository.DeleteBillCollectionDateByDate(billCollectionDateForDelete);
            _repositoryManager.Commit();
        }
        public BillCollectionDateForGetSingle CreateBillCollectionDate(BillCollectionDateForCreate billCollectionDateForCreate)
        {
            BillCollectionDateForCreateDto billCollectionDateForCreateDto = _mapper.Map<BillCollectionDateForCreateDto>(billCollectionDateForCreate);

            billCollectionDateForCreateDto.createDate = DateTime.Now;
            billCollectionDateForCreateDto.lastUpdateDate = DateTime.Now;
            billCollectionDateForCreateDto.function = "A";

            _repositoryManager.BillCollectionDateRepository.CreateBillCollectionDate(billCollectionDateForCreateDto);
            _repositoryManager.Commit();
            
            BillCollectionDateForGetSingle billCollectionDate = new BillCollectionDateForGetSingle();
            billCollectionDate.customerCode = billCollectionDateForCreate.customerCode;
            billCollectionDate.billColCalculate = billCollectionDateForCreate.billColCalculate;
            billCollectionDate.departmentCode = billCollectionDateForCreate.departmentCode;

            if(billCollectionDate.billColCalculate == "BICAL5") {
                billCollectionDate.dateOne = billCollectionDateForCreate.weekNo;
                billCollectionDate.dateTwo = billCollectionDateForCreate.dayOfWeek;
            }
            else {
                billCollectionDate.dateOne = billCollectionDateForCreate.startDate;
                billCollectionDate.dateTwo = billCollectionDateForCreate.endDate;
            }

            return billCollectionDate;
        }
    }
}