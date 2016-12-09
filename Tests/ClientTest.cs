using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;
using Xunit;
using System;
using System.Collections.Generic;

namespace HairSalon
{
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = hair_salon_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        [Fact]
        public void Test_ClientsEmptyAtFirst()
        {
            int result = Client.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
            Client client1 = new Client("Mako", "Tips well, comes in once a week.", 1);
            Client client2 = new Client("Mako", "Tips well, comes in once a week.", 1);

            Assert.Equal(client1, client2);
        }

        [Fact]
        public void Test_Save_SavesClientToDatabase()
        {
            Client testClient = new Client("Mako", "Tips well, comes in once a week.", 1);
            testClient.Save();

            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client>{testClient};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToClientObject()
        {
            Client testClient = new Client("Mako", "Tips well, comes in once a week.", 1);
            testClient.Save();

            Client savedClient = Client.GetAll()[0];

            int testId = testClient.GetId();
            int result = savedClient.GetId();

            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsClientInDatabase()
        {
            Client testClient = new Client("Mako", "Tips well, comes in once a week.", 1);
            testClient.Save();

            Client foundClient = Client.Find(testClient.GetId());

            Assert.Equal(testClient, foundClient);
        }

        [Fact]
        public void Test_Update_UpdatesClientInDatabase()
        {
            Client testClient = new Client("Mako", "Tips well, comes in once a week.", 1);
            testClient.Save();
            string newName = "Yukiko";
            string newDescription = "Comes in twice a month; friend\'s daughter, treat well.";
            int newStylistId = 2;

            testClient.Update(newName, newDescription, newStylistId);

            Client result = Client.GetAll()[0];
            int compareId = result.GetId();
            Client comparisonClient = new Client(newName, newDescription, newStylistId, compareId);

            Assert.Equal(comparisonClient, result);
        }
    }
}
