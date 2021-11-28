using Oracle.ManagedDataAccess.Client;
using Pharmacy_IS.Model;
using Pharmacy_IS.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_IS.ViewModel.Service
{
    class LoginService
    {
        private OracleConnection _conn;

        public LoginService()
        {
            //this.setConnection();
        }

        //return true if password matches with psswd in db
        //TODO set Current Logged User
        public bool LogIn(string userName, string password)
        {
            PasswordUtils utils = new PasswordUtils();
            string temp = utils.HashString(password);
            //string hashedDBPassword = this.getUserPassword(userName);
            //if (hashedDBPassword == null)
            //{
            //    throw new NoSuchUserException("Wrong user name.");
            //}
            //return (utils.HashString(password).Equals(hashedDBPassword));
            return true;
        }

        private string getUserPassword(string username)
        {
            try
            {
                _conn.Open();
                OracleCommand command = new OracleCommand() { Connection = _conn};
                command.CommandText = $"select password from user where username like {username}";
                command.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToString(reader[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return null;
        }

        private void setConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["OrclConnectionString"].ConnectionString;
            try
            {
                _conn = new OracleConnection(connString);
                _conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
