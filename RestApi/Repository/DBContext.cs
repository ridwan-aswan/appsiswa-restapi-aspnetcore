using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;

namespace RestApi.Repository
{

    public interface IDBContext : IDisposable { 
    

        IDbConnection db { get; }
    
    }

    public class DBContext :IDBContext

    {
        private MySqlConnection _conn;
        private readonly string _connectionString;
        private IDbConnection _db;

        public DBContext(IConfiguration configuration)
        { 

            _connectionString = configuration.GetConnectionString("DefaultConnection");


            if (_db == null)
            {
                _db = GetOpenConnection(_connectionString);
            }

        }


        private IDbConnection GetOpenConnection(string connectionString)
        {

            try

            {
                _conn = new MySqlConnection(_connectionString);

                _conn.Open();

            }
            catch (Exception err)
            {

                Debug.WriteLine("Error Connection : " + err.Message);

            }

            return _conn;
        }

        public void Dispose()
        {
            _conn.Close();
        }

        public IDbConnection db
        {
            get { return _db ?? (_db = GetOpenConnection(_connectionString)); }
        }

 }
}
