using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace DataAccess;

public class SQLDataAccess : ISQLDataAccess
{

    private readonly IConfiguration _config;

    public SQLDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string dappercode,
        U parameters,
        string connectionID = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionID));

        return await connection.QueryAsync<T>(dappercode, parameters,
            commandType: CommandType.Text);
    }

    public async Task SaveData<T>(
        string dappercode,
        T parameters,
        string connectionID = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionID));
        await connection.ExecuteAsync(dappercode, parameters, commandType: CommandType.Text);
    }
}
