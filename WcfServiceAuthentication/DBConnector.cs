using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;

namespace WcfServiceAuthentication
{
    public class DBConnector
    {
        public bool ValidateLogin(string username, string password)
        {
            string hashPW = sha256(password);
            string connectionString = "Server=localhost,Port=3306;Database=world;Uid=root;Pwd=*w&6 pO9 HXX0cX+&@7P;";
            string selectQuery = "SELECT * FROM world.users WHERE username = '" + username + "' AND password = '" + hashPW + "'";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(selectQuery, conn);
            
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();


            if (reader.HasRows){
                // User is logged in maybe do FormsAuthentication.SetAuthcookie(username);
                reader.Close();
                conn.Close();
                return true;
            }
            else {
                reader.Close();
                conn.Close();
                return false;
            }
        }
        static string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}