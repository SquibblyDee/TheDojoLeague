using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;

namespace DapperApp.Factories
{
    public class NinjaFactory : IFactoryNinja<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=C3ll@rd00r;port=3306;database=mydb;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Ninja item)
        {
            using (IDbConnection dbConnection = Connection) 
            {
                string query =  "INSERT INTO Ninjas (Name, Level, Description, Dojo_Id) VALUES (@Name, @Level, @Description, @Dojo_Id)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM Ninjas");
            }
        }
        public Ninja FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM Ninjas WHERE Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Ninja> NinjasWithDojos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = "SELECT * FROM Ninjas LEFT JOIN Dojos ON Ninjas.Dojo_Id WHERE Ninjas.Dojo_Id = Dojos.Id";
                dbConnection.Open();

                IEnumerable<Ninja> myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return myNinjas;
            }
        }

        public Ninja NinjaWithDojo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM Ninjas JOIN Dojos ON Ninjas.Dojo_Id WHERE Ninjas.Dojo_Id = Dojos.Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Ninja> NinjasWithDojo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM Ninjas LEFT JOIN Dojos ON Ninjas.Dojo_Id WHERE Ninjas.Dojo_Id = Dojos.Id";
                dbConnection.Open();

                IEnumerable<Ninja> myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return myNinjas;
            }
        }

        public dynamic Banish(int id)
        {
            using (IDbConnection dbConnection = Connection) 
            {
                string query =  $"UPDATE Ninjas SET Dojo_Id=3 WHERE Id = {id}";
                dbConnection.Open();
                dbConnection.Execute(query);
                return id;
            }
        }

        public dynamic Recruit(int id, int dojoid)
        {
            using (IDbConnection dbConnection = Connection) 
            {
                string query =  $"UPDATE Ninjas SET Dojo_Id = {dojoid} WHERE Id = {id}";
                dbConnection.Open();
                dbConnection.Execute(query);
                return id;
            }
        }

    }
}