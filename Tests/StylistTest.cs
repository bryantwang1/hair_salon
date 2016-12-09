using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;
using Xunit;
using System;
using System.Collections.Generic;

namespace HairSalonTest
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = hair_salon_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        [Fact]
        public void Test_StylistsEmptyAtFirst()
        {
            int result = Stylist.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Stylist stylist1 = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");
            Stylist stylist2 = new Stylist("Mako", "(555)555-5556", "Could be faster.");

            Assert.Equal(stylist1, stylist2);
        }

        [Fact]
        public void Test_Save_SavesStylistToDatabase()
        {
            Stylist testStylist = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");
            testStylist.Save();

            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToStylistObject()
        {
            Stylist testStylist = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");
            testStylist.Save();

            Stylist savedStylist = Stylist.GetAll()[0];

            int testId = testStylist.GetId();
            int result = savedStylist.GetId();

            Assert.Equal(testId, result);
        }
    }
}
