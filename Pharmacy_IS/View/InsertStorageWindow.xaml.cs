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
using Pharmacy_IS.Model.Entities;

namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for InsertStorageWindow.xaml
    /// </summary>
    public partial class InsertStorageWindow : Window
    {
        private State state;

        public InsertStorageWindow(State p_state)
        {
            state = p_state;

            InitializeComponent();
            if (state == State.Editing)
            {
                this.heading.Content = "Editing Storage Record";
                this.confirmButton.Content = "Confirm";
            }
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
