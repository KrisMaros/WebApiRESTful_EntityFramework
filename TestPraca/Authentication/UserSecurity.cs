using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestPraca.Models;

namespace TestPraca.Authentication
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            using (DBconn conn = new DBconn())
            {
                return conn.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password ==password);
            }
        }
    }
}