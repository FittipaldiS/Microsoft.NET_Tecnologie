using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace DataAccess.Tests
{
    [TestClass()]
    public class DataAccessConnectionTests
    {
        [TestMethod()]
        public void StringConnection()
        {
            string connectionString = "Server=DESKTOP-E2NGEBR;Database=bundesliga;Trusted_Connection=True;TrustServerCertificate=True;";

            SqlConnection connection = new SqlConnection(connectionString: connectionString);


            connection.Open();


            connection.Close();
            //var name = "DESKTOP-E2NGEBR";
            //var connectionString = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;


            Assert.Fail();
        }
    }
}