namespace AxonsERP.Contracts
{
    public interface IRepositoryManager
    {
        IApplicationCtrlRepository ApplicationCtrl { get; }
        ITaxRateControlRepository TaxRateControl { get; }
        IGeneralDescRepository GeneralDescRepository { get; }

        void Commit();
    }
}
