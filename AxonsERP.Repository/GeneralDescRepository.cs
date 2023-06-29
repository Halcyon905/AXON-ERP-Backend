using AxonsERP.Contracts;
using AxonsERP.Entities.Models;
using Dapper;
using System.Data;

namespace AxonsERP.Repository
{
    internal class GeneralDescRepository : RepositoryBase, IGeneralDescRepository
    {
        internal GeneralDescRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        
        public IEnumerable<GeneralDesc> GetListGeneralDesc()
        {
            var generalDescList = Connection.Query<GeneralDesc>(@"SELECT GDCODE as gdCode,
                                                                DESC1 as desc1,
                                                                DESC2 as desc2
                                                                FROM GENERAL_DESC
                                                                WHERE GDTYPE = 'TXCOD'");
            foreach(var generalDesc in generalDescList) {
                generalDesc.gdCode = generalDesc.gdCode.Substring(5, generalDesc.gdCode.Length - 5);
            }
            return generalDescList;
        }
    }
}