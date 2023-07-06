namespace AxonsERP.Service.Contracts
{
    public interface IServiceManager
    {
        ITaxRateControlService TaxRateControlService { get; }
        IGeneralDescService GeneralDescService{ get; }
        IBillCollectionDateService BillCollectionDateService { get; }
    }
}
