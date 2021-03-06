using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace HairSalon.Objects
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _description;

        public Stylist(string StylistName, string StylistPhone, string StylistDescription, int Id = 0)
        {
            _id = Id;
            _name = StylistName;
            _phone = StylistPhone;
            _description = StylistDescription;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = this.GetId() == newStylist.GetId();
                bool nameEquality = this.GetName() == newStylist.GetName();
                bool phoneEquality = this.GetPhone() == newStylist.GetPhone();
                bool descriptionEquality = this.GetDescription() == newStylist.GetDescription();
                return (idEquality && nameEquality && phoneEquality && descriptionEquality);
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

        public string GetPhone()
        {
            return _phone;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, phone, description) OUTPUT INSERTED.id VALUES (@StylistName, @StylistPhone, @StylistDescription);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StylistName";
            nameParameter.Value = this.GetName();

            SqlParameter phoneParameter = new SqlParameter();
            phoneParameter.ParameterName = "@StylistPhone";
            phoneParameter.Value = this.GetPhone();

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@StylistDescription";
            descriptionParameter.Value = this.GetDescription();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(phoneParameter);
            cmd.Parameters.Add(descriptionParameter);

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

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>  {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistPhone = rdr.GetString(2);
                string stylistDescription = rdr.GetString(3);

                Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistDescription, stylistId);
                allStylists.Add(newStylist);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allStylists;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Stylist Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = searchId.ToString();
            cmd.Parameters.Add(idParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int stylistId = 0;
            string stylistName = null;
            string stylistPhone = null;
            string stylistDescription = null;
            while(rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                stylistName = rdr.GetString(1);
                stylistPhone = rdr.GetString(2);
                stylistDescription = rdr.GetString(3);
            }
            Stylist foundStylist = new Stylist(stylistName, stylistPhone, stylistDescription, stylistId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundStylist;
        }

        public void Update(string newName, string newPhone, string newDescription)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @StylistName, phone = @StylistPhone, description = @StylistDescription WHERE id = @StylistId;", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StylistName";
            nameParameter.Value = newName;

            SqlParameter phoneParameter = new SqlParameter();
            phoneParameter.ParameterName = "@StylistPhone";
            phoneParameter.Value = newPhone;

            SqlParameter descriptionParameter = new SqlParameter();
            descriptionParameter.ParameterName = "@StylistDescription";
            descriptionParameter.Value = newDescription;

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = this.GetId().ToString();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(phoneParameter);
            cmd.Parameters.Add(descriptionParameter);
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

            SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients WHERE stylist_id = @StylistId;", conn);

            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = this.GetId();
            cmd.Parameters.Add(idParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Client> GetClients()
        {
            List<Client> allClients = new List<Client> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId", conn);
            SqlParameter idParameter = new SqlParameter();
            idParameter.ParameterName = "@StylistId";
            idParameter.Value = this.GetId();
            cmd.Parameters.Add(idParameter);

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
    }
}
