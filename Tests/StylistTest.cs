using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;
using Xunit;
using System;
using System.Collections.Generic;

namespace HairSalon
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
            Client.DeleteAll();
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
            Stylist stylist2 = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");

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

        [Fact]
        public void Test_Find_FindsStylistInDatabase()
        {
            Stylist testStylist = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");
            testStylist.Save();

            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            Assert.Equal(testStylist, foundStylist);
        }

        [Fact]
        public void Test_Update_UpdatesStylistInDatabase()
        {
            Stylist testStylist = new Stylist("Giovanni", "(555)555-5555", "Fast, does good work.");
            testStylist.Save();
            string newName = "Frank";
            string newPhone = "(555)111-2222";
            string newDescription = "Could be faster.";

            testStylist.Update(newName, newPhone, newDescription);

            Stylist result = Stylist.GetAll()[0];
            int compareId = result.GetId();
            Stylist comparisonStylist = new Stylist(newName, newPhone, newDescription, compareId);

            Assert.Equal(comparisonStylist, result);
        }
    }
}
