using AxonsERP.Contracts;
using AxonsERP.Contracts.Utils;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AxonsERP.Repository
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private IApplicationCtrlRepository? _applicationCtrlRepository;
        private ITaxRateControlRepository? _taxRateControlRepository;
        private bool _disposed;

        public RepositoryManager(IConfiguration configuration)
        {
            _connection = new OracleConnection(OracleUtil.GetConnectionString(configuration));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IApplicationCtrlRepository ApplicationCtrl
        {
            get { return _applicationCtrlRepository ??= new ApplicationCtrlRepository(_transaction); }
        }

        public ITaxRateControlRepository TaxRateControl 
        {
            get { return _taxRateControlRepository ??= new TaxRateControlRepository(_transaction);}
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                    _connection.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
