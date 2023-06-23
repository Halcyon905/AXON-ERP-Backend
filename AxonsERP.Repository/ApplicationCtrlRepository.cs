using AxonsERP.Contracts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Repository
{
    internal class ApplicationCtrlRepository : RepositoryBase, IApplicationCtrlRepository
    {
        internal ApplicationCtrlRepository(IDbTransaction transaction)
               : base(transaction)
        {

        }

        public int GetLenControlAcc()
        {
            var sql = @"SELECT  
                             LEN_CONTROL_ACC
                        FROM APPLICATION_CTRL";

            var result = Connection.QueryFirstOrDefault<int>(sql);
            return result;
        }
    }
}
