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

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = this.GetId() == newClient.GetId();
                bool nameEquality = this.GetName() == newClient.GetName();
                bool descriptionEquality = this.GetDescription() == newClient.GetDescription();
                bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
                return (idEquality && nameEquality && descriptionEquality && stylistIdEquality);
            }
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, description, stylist_id) OUTPUT INSERTED.id VALUES (@StylistName, @StylistDescription, @StylistId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StylistName";
            nameParameter.Value = this.GetName();

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@StylistDescription";
            descriptionParameter.Value = this.GetDescription();

            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = this.GetStylistId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);
            cmd.Parameters.Add(stylistIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>  {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int ClientId = rdr.GetInt32(0);
                string ClientName = rdr.GetString(1);
                string ClientDescription = rdr.GetString(2);
                int StylistId = rdr.GetInt32(3);

                Client newClient = new Client(ClientName, ClientDescription, StylistId, ClientId);
                allClients.Add(newClient);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allClients;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Client Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = searchId.ToString();
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int clientId = 0;
            string clientName = null;
            string clientDescription = null;
            int stylistId = 0;
            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                clientDescription = rdr.GetString(2);
                stylistId = rdr.GetInt32(3);
            }
            Client foundClient = new Client(clientName, clientDescription, stylistId, clientId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundClient;
        }

        public void Update(string newName, string newDescription, int newStylistId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @ClientName, description = @ClientDescription, stylist_id = @StylistId WHERE id = @ClientId;", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@ClientName";
            nameParameter.Value = newName;

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@ClientDescription";
            descriptionParameter.Value = newDescription;

            SqlParameter stylistIdParameter = new SqlParameter();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = newStylistId.ToString();

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = this.GetId().ToString();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(descriptionParameter);
            cmd.Parameters.Add(stylistIdParameter);
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@ClientId";
            idParameter.Value = this.GetId().ToString();
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }
    }
}
