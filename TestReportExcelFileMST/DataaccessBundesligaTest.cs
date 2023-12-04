using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReportExcelFileMST
{
    [TestClass]
    public class DataaccessBundesligaTest
    {
        [TestMethod]
        public void GetAllPlayer() 
        {
            using (var context = new BundesligaContext())
            {
                var players = context.Spielers.ToList();

                Assert.IsNotNull(players);
            }

        }

        [TestMethod]
        public void GetAllTeam()
        {
            using (var context = new BundesligaContext())
            {
                var teams = context.Vereins.ToList();

                Assert.IsNotNull(teams);
            }

        }

        [TestMethod]
        public void GetAllMatch()
        {
            using (var context = new BundesligaContext())
            {
                var speils = context.Spiels.ToList();

                Assert.IsNotNull(speils);
            }

        }
    }
}
