using System.Data;

namespace AxonsERP.Repository
{
    internal abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection? Connection { get { return Transaction.Connection; } }

        protected RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
