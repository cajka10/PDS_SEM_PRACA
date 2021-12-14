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
using Pharmacy_IS.Model;

namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginService _loginService;        
        public LoginWindow()
        {
            InitializeComponent();
            _loginService = new LoginService();

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
            try
            {
                if (_loginService.LogIn(this.UserNameTextBox.Text, this.PasswordTextBox.Password))
                {
                    Console.WriteLine("Current logged user: " + ((LoggedUser)App.Current.Properties["LoggedUser"]).ToString());
                    MainWindow main = new MainWindow();
                    this.Close();
                    main.Show();
                }           
            }
            catch (NoSuchUserException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBoxResult result = MessageBox.Show("Zadaný user neexistuje.", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBoxResult result = MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
