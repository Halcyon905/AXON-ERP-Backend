namespace AxonsERP.Contracts
{
    public interface IRepositoryManager
    {
        IApplicationCtrlRepository ApplicationCtrl { get; }
        ITaxRateControlRepository TaxRateControl { get; }

        void Commit();
    }
}
