using AxonsERP.Service.Contracts;
using AxonsERP.Entities.Models;
using AutoMapper;
using AxonsERP.Contracts;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service
{
    public class CreditControlService : ICreditControlService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public CreditControlService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public CreditControl GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle)
        {
            creditControlForGetSingle.departmentCode = "OPRCD" + creditControlForGetSingle.departmentCode;
            creditControlForGetSingle.billColCalculate = "BICAL" + creditControlForGetSingle.billColCalculate;

            var resultRaw = _repositoryManager.CreditControlRepository.GetSingleCreditControl(creditControlForGetSingle);
            return resultRaw;
        }
        public IEnumerable<CreditControl> SearchCreditControl(CreditControlParameters parameters)
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
            var resultRaw = _repositoryManager.CreditControlRepository.SearchCreditControl(parameters);
            return resultRaw;
        }
    }
}