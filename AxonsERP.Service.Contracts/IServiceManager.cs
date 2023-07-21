namespace AxonsERP.Service.Contracts
{
    public interface IServiceManager
    {
        ITaxRateControlService TaxRateControlService { get; }
        IGeneralDescService GeneralDescService{ get; }
        IBillCollectionDateService BillCollectionDateService { get; }
        ICVDescService CVDescService { get; }
        ICreditControlService CreditControlService { get; }
        IConvertToGLService ConvertToGLService { get; }
        ICompanyAccChartService CompanyAccChartService { get; }
        ITRNDescService TRNDescService { get; }
        ICompanyService CompanyService { get; }
    }
}
