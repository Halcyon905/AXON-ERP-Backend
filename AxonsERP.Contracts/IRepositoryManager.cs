namespace AxonsERP.Contracts
{
    public interface IRepositoryManager
    {
        IApplicationCtrlRepository ApplicationCtrl { get; }

        void Commit();
    }
}
