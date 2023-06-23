using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AxonsERP.Repository.Extensions
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();

        private readonly List<OracleParameter> oracleParameters = new();

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value, int? size = null)
        {
            OracleParameter oracleParameter;
            if (size.HasValue)
            {
                oracleParameter = new OracleParameter(name, oracleDbType, size.Value, value, direction);
            }
            else
            {
                oracleParameter = new OracleParameter(name, oracleDbType, value, direction);
            }

            oracleParameters.Add(oracleParameter);
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            oracleParameters.Add(oracleParameter);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);


            if (command is OracleCommand oracleCommand)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }
    }
}
