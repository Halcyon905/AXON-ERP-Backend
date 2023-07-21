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
        private IGeneralDescRepository? _generalDescRepository;
        private IBillCollectionDateRepository? _billCollectionDateRepository;
        private ICVDescRepository? _cvDescRepository;
        private ICreditControlRepository? _creditControlRepository;
        private IConvertToGLRepository? _convertToGLRepository;
        private ICompanyAccChartRepository? _companyAccChartRepository;
        private ITRNDescRepository? _trnDescRepository;
        private ICompanyRepository? _companyRepository;
        private bool _disposed;

        public RepositoryManager(IConfiguration configuration)
        {
            _connection = new OracleConnection(OracleUtil.GetConnectionString(configuration));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IApplicationCtrlRepository ApplicationCtrl
        {
            get { return _applicationCtrlRepository ??= new ApplicationCtrlRepository(_transaction);}
        }

        public ITaxRateControlRepository TaxRateControl 
        {
            get { return _taxRateControlRepository ??= new TaxRateControlRepository(_transaction);}
        }

        public IGeneralDescRepository GeneralDescRepository
        {
            get { return _generalDescRepository ??= new GeneralDescRepository(_transaction);}
        }
        public IBillCollectionDateRepository BillCollectionDateRepository
        {
            get { return _billCollectionDateRepository ??= new BillCollectionDateRepository(_transaction);}
        }
        public ICVDescRepository CVDescRepository
        {
            get { return _cvDescRepository ??= new CVDescRepository(_transaction);}
        }
        public ICreditControlRepository CreditControlRepository
        {
            get { return _creditControlRepository ??= new CreditControlRepository(_transaction);}
        }
        public IConvertToGLRepository ConvertToGLRepository
        {
            get { return _convertToGLRepository ??= new ConvertToGLRepository(_transaction);}
        }
        public ICompanyAccChartRepository CompanyAccChartRepository
        {
            get { return _companyAccChartRepository ??= new CompanyAccChartRepository(_transaction);}
        }
        public ITRNDescRepository TRNDescRepository
        {
            get { return _trnDescRepository ??= new TRNDescRepository(_transaction);}
        }
        public ICompanyRepository CompanyRepository
        {
            get { return _companyRepository ??= new CompanyRepository(_transaction);}
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
