using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace AxonsERP.Repository 
{
    internal class TaxRateControlRepository : RepositoryBase, ITaxRateControlRepository
    {
        internal TaxRateControlRepository(IDbTransaction transaction)
               : base(transaction)
        {

        }

        public void CreateTaxRateControl(TaxRateControl _TaxRateControl) 
        {

        }
        public void UpdateTaxRateControl(TaxRateControl _TaxRateControl) 
        {

        }
        public void DeleteTaxRateControl(List<Dictionary<string, object>> TaxRateControlList) 
        {

        }
        public List<TaxRateControl> GetListTaxRateControl() 
        {
            return null;
        }
        public TaxRateControl GetSingleTaxRateControl(TaxRateControl _TaxRateControl) 
        {
            return null;
        }
    }
}