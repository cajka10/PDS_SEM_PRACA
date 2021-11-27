using Pharmacy_IS.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginService _loginService;
        private OracleConnection _conn;
        
        public LoginWindow()
        {
            InitializeComponent();
            _loginService = new LoginService();
            this.setConnection();

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonMin_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void ButtonClose_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _loginService.LogIn(this.UserNameTextBox.Text, this.PasswordTextBox.Password);
        }

        private void setConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["OrclConnectionString"].ConnectionString;
            _conn = new OracleConnection(connString);
            try
            {
                _conn.Open();
            //    using (OracleCommand command = new OracleCommand())
            //    {
            //        command.Connection = _conn;
            //        command.CommandText = "select * from grofcik5.my_table";
            //        command.CommandType = System.Data.CommandType.Text;
            //        OracleDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Console.WriteLine("****************************************");
            //            Console.WriteLine(reader[0] + " - " + reader[1] + reader[2]);
            //            Console.WriteLine("****************************************");
            //        }
            //    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
