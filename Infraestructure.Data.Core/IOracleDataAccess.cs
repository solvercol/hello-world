using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace Infraestructure.Data.Core
{
    public interface IOracleDataAccess
    {
        DataTable ExecuteDataTable(string sql, CommandType cmdType);
        DataTable ExecuteDataTable(string sql, CommandType cmdType, params OracleParameter[] parameters);
        object ExecuteScalar(string sql, CommandType cmdType);
        object ExecuteScalar(string sql, CommandType cmdType, params OracleParameter[] parameters);
        void ExecuteNonquery(string sql, CommandType cmdType);
        void ExecuteNonquery(string sql, CommandType cmdType, params OracleParameter[] parameters);
    }
}