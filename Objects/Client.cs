using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace HairSalon.Objects
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _description;
        private int _stylistId;

        public Client(string ClientName, string ClientDescription, int StylistId, int Id = 0)
        {
            _id = Id;
            _name = ClientName;
            _description = ClientDescription;
            _stylistId = StylistId;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDescription()
        {
            return _description;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }
    }
}
