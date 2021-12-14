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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy_IS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool menuColapsed = false;
        bool medsBarColapsed,receiptsBarColapsed;

        public MainWindow()
        {
            InitializeComponent();

            medsBarColapsed = true;
            medsGrid.Height = medsGridHeader.Height;

            receiptsBarColapsed = true;
            receiptsGrid.Height = medsGridHeader.Height;
            receiptsGrid.Margin = new Thickness(0, 45, 0, 0);
 

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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (menuColapsed)
            {
                menuColapsed = false;
                leftPanel.Width = 250;
            }
            else
            {
                menuColapsed = true;
                leftPanel.Width = 50;

            }
        }

        private void medsBarHeightChange(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"pack://application:,,/View/arrowicon.png");
            if (medsBarColapsed)
            {
                medsBarColapsed = false;
                medsGrid.Height = 250;

                bi.Rotation = Rotation.Rotate180;
                bi.EndInit();
                arrowIconMeds.Source = bi;

                receiptsGrid.Margin = new Thickness(0, 250, 0, 0);
            }
            else
            {
                bi.EndInit();
                arrowIconMeds.Source = bi;
                medsBarColapsed = true;
                medsGrid.Height = 40;
                receiptsGrid.Margin = new Thickness(0, 45, 0, 0);

            }
        }

        private void Label_allMeds_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void showAllMeds(object sender, MouseButtonEventArgs e)
        {
            this.MedicamentsUC.Visibility = Visibility.Visible;
            this.StorageUC.Visibility = Visibility.Hidden;
            this.BestsellersUC.Visibility = Visibility.Hidden;

        }

        private void showStorage(object sender, MouseButtonEventArgs e)
        {
            this.MedicamentsUC.Visibility = Visibility.Hidden;
            this.StorageUC.Visibility = Visibility.Visible;
            this.BestsellersUC.Visibility = Visibility.Hidden;

        }

        private void showBestsellers(object sender, MouseButtonEventArgs e)
        {
            this.MedicamentsUC.Visibility = Visibility.Hidden;
            this.StorageUC.Visibility = Visibility.Hidden;
            this.BestsellersUC.Visibility = Visibility.Visible;
        }

        private void receiptsBarHeightChange(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"pack://application:,,/View/arrowicon.png");
            if (receiptsBarColapsed)
            {
                receiptsBarColapsed = false;
                receiptsGrid.Height = 250;

                bi.Rotation = Rotation.Rotate180;
                bi.EndInit();
                arrowIconReceipt.Source = bi;
            }
            else
            {
                receiptsBarColapsed = true;
                receiptsGrid.Height = 45;

                bi.EndInit();
                arrowIconReceipt.Source = bi;

            }
        }
    }
}
