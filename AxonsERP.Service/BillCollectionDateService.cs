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
        private Dictionary<string, string> columnWithCodes = new Dictionary<string, string>()
                                                                                        {
                                                                                            {"billColCalculate", "BICAL"},
                                                                                            {"departmentCode", "OPRCD"}
                                                                                        };

        public BillCollectionDateService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void addPrefix<T>(T target)
        {
            foreach(var prop in target.GetType().GetProperties())
            {
                if(columnWithCodes.ContainsKey(prop.Name))
                {
                    prop.SetValue(target, columnWithCodes[prop.Name] + prop.GetValue(target));
                }
            }
        }
        public IEnumerable<BillCollectionDateToReturn> GetListBillCollectionDate()
        {
            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetAllBillCollectionDate();
            return resultRaw;
        }
        public BillCollectionDateSingleToReturn GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDate)
        {
            addPrefix<BillCollectionDateForGetSingle>(billCollectionDate);

            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetSingleBillCollectionDate(billCollectionDate);
            return resultRaw;
        }
        public IEnumerable<BillCollectionDateToReturn> SearchBillCollectionDate(BillCollectionDateParameters parameters)
        {
            if(parameters.Search != null) {
                foreach(var searchObject in parameters.Search) {
                    if(searchObject.Name.Equals("department", StringComparison.InvariantCultureIgnoreCase)) {
                        searchObject.Value = "OPRCD" + searchObject.Value;
                    }
                    else if(searchObject.Name.Equals("bill_col_calculate", StringComparison.InvariantCultureIgnoreCase)) {
                        searchObject.Value = "BICAL" + searchObject.Value;
                    }
                }
            }

            var resultRaw = _repositoryManager.BillCollectionDateRepository.SearchBillCollectionDate(parameters);
            return resultRaw;
        }
        public BillCollectionDateToReturn GetCompanyBillCollectionDate(BillCollectionDateForSingleCustomer billCollectionDateForSingle)
        {
            addPrefix<BillCollectionDateForSingleCustomer>(billCollectionDateForSingle);

            var resultRaw = _repositoryManager.BillCollectionDateRepository.GetCompanyBillCollectionDate(billCollectionDateForSingle);

            if(!resultRaw.Any()) {
                BillCollectionDateToReturn notFound = new BillCollectionDateToReturn();
                
                notFound.billCollectionDateStart = new List<int>();
                notFound.billCollectionDateEnd = new List<int>();

                return notFound;
            }

            BillCollectionDateToReturn _billCollectionDateToReturn = _mapper.Map<BillCollectionDateToReturn>(resultRaw.ToList()[0]);
            
            List<int> startList = new List<int>();
            List<int> endList = new List<int>();

            string nameStart, nameEnd;
            if(_billCollectionDateToReturn.billColCalculate == "5") {
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
            BillCollectionDateForSingleCustomer _billCollectionForSingle = _mapper.Map<BillCollectionDateForSingleCustomer>(billCollectionDateForUpdate);
            var result = GetCompanyBillCollectionDate(_billCollectionForSingle);
            if(!result.billCollectionDateStart.Any()) {
                throw new BillCollectionDateNotFoundException(billCollectionDateForUpdate.customerCode, billCollectionDateForUpdate.billColCalculate);
            }

            BillCollectionDateForGetSingle billCollectionDate = _mapper.Map<BillCollectionDateForGetSingle>(billCollectionDateForUpdate);
            billCollectionDate.dateOne = billCollectionDateForUpdate.newStartDate;
            billCollectionDate.dateTwo = billCollectionDateForUpdate.newEndDate;

            BillCollectionDateSingleToReturn _billCollectionDateToReturn = GetSingleBillCollectionDate(billCollectionDate);
            if(_billCollectionDateToReturn != null)
            {
                throw new BillCollectionDateDuplicateException();
            }

            BillCollectionDateForUpdateDto billCollectionDateForUpdateDto = _mapper.Map<BillCollectionDateForUpdateDto>(billCollectionDateForUpdate);

            billCollectionDateForUpdateDto.function = "C";
            billCollectionDateForUpdateDto.lastUpdateDate = DateTime.Now;
            addPrefix<BillCollectionDateForUpdateDto>(billCollectionDateForUpdateDto);

            _repositoryManager.BillCollectionDateRepository.UpdateBillCollectionDate(billCollectionDateForUpdateDto);
            _repositoryManager.Commit();
        }
        public void DeleteBillCollectionDateByCompany(IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany)
        {
            foreach(var billColInfo in billCollectionDateForDeleteMany)
            {
                addPrefix<BillCollectionDateForDeleteMany>(billColInfo);
            }
            _repositoryManager.BillCollectionDateRepository.DeleteBillCollectionDateByCompany(billCollectionDateForDeleteMany);
            _repositoryManager.Commit();
        }
        public void DeleteBillCollectionDateByDate(BillCollectionDateForDelete billCollectionDateForDelete)
        {
            addPrefix<BillCollectionDateForDelete>(billCollectionDateForDelete);

            _repositoryManager.BillCollectionDateRepository.DeleteBillCollectionDateByDate(billCollectionDateForDelete);
            _repositoryManager.Commit();
        }
        public BillCollectionDateForGetSingle CreateBillCollectionDate(BillCollectionDateForCreate billCollectionDateForCreate)
        {
            BillCollectionDateForGetSingle billCollectionDate = _mapper.Map<BillCollectionDateForGetSingle>(billCollectionDateForCreate);

            if(billCollectionDate.billColCalculate == "5") {
                billCollectionDate.dateOne = billCollectionDateForCreate.weekNo;
                billCollectionDate.dateTwo = billCollectionDateForCreate.dayOfWeek;
            }
            else {
                billCollectionDate.dateOne = billCollectionDateForCreate.startDate;
                billCollectionDate.dateTwo = billCollectionDateForCreate.endDate;
            }

            BillCollectionDateSingleToReturn _billCollectionDateToReturn = GetSingleBillCollectionDate(billCollectionDate);
            if(_billCollectionDateToReturn != null)
            {
                throw new BillCollectionDateDuplicateException();
            }

            BillCollectionDateForCreateDto billCollectionDateForCreateDto = _mapper.Map<BillCollectionDateForCreateDto>(billCollectionDateForCreate);

            billCollectionDateForCreateDto.createDate = DateTime.Now;
            billCollectionDateForCreateDto.lastUpdateDate = DateTime.Now;
            billCollectionDateForCreateDto.function = "A";
            addPrefix<BillCollectionDateForCreateDto>(billCollectionDateForCreateDto);

            _repositoryManager.BillCollectionDateRepository.CreateBillCollectionDate(billCollectionDateForCreateDto);
            _repositoryManager.Commit();

            return billCollectionDate;
        }
    }
}