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
        private LoggedUser _user;

        public LoginService()
        {
            this.setConnection();
            _user = new LoggedUser();
        }


        public bool LogIn(string userName, string password)
        {
            PasswordUtils utils = new PasswordUtils();
            string temp = utils.HashString(password);
            string hashedDBPassword = this.getUserPassword(userName);
            if (hashedDBPassword == null)
            {
                throw new NoSuchUserException("Wrong user name.");
            }
            bool output = utils.HashString(password).Equals(hashedDBPassword);
            if (output)
                App.Current.Properties["LoggedUser"] = _user;
            return output;
        }

        private string getUserPassword(string username)
        {          
            try
            {
                _conn.Open();
                OracleCommand command = new OracleCommand() { Connection = _conn};
                command.CommandText = $"select password, id_role, user_name, id_user from novakova25.person where USER_NAME like '{username}'";
                command.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    _user.Role = (UserRole)Convert.ToInt32(reader[1]);
                    _user.UserName = username;
                    _user.Id = Convert.ToInt32(reader[3]);
                    return Convert.ToString(reader[0]);
                }             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return null;
        }

        private void setConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["OrclConnectionString"].ConnectionString;
            try
            {
                _conn = new OracleConnection(connString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
    }
}
