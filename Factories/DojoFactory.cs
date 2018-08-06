using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;

namespace DapperApp.Factories
{
    public class DojoFactory : IFactoryDojo<Dojo>
    {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=C3ll@rd00r;port=3306;database=mydb;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Dojo item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO Dojos (Name, Description, Length, Elevation, Latitude, Longitude) VALUES (@Name, @Description, @Length, @Elevation, @Latitude, @Longitude)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Dojo> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM Dojos");
            }
        }
        public Dojo FindById(long id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var query =
                @"
                SELECT * FROM dojos WHERE Id = @Id;
                SELECT * FROM ninjas WHERE Dojo_Id = @Id;
                ";

                using (var multi = dbConnection.QueryMultiple(query, new {Id = id}))
                {
                    Dojo dojo = multi.Read<Dojo>().Single();
                    dojo.ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }
            }
        }
    }
}