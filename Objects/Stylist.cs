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
                return (idEquality && nameEquality);
            }
        }
    }
}
