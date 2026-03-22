using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PaysisReconAPI.DatabaseContext
{
    public interface IDataDbContext : IDisposable
    {
        NpgsqlConnection GetOpenSqlConnection();
    }

    public class ReconDBContext : IDataDbContext
    {
        private readonly string _connectionString;

        public ReconDBContext(IConfiguration configuration)
        {
            // Read connection string from IConfiguration
            _connectionString = configuration.GetConnectionString("PostGreDB");
        }

        public void Dispose()
        {
            //this.Dispose();
        }

        public NpgsqlConnection GetOpenSqlConnection()
        {
            NpgsqlConnection con = new NpgsqlConnection();
            con.ConnectionString = this._connectionString;
            con.Open();

            return con;
        }
    }
}
