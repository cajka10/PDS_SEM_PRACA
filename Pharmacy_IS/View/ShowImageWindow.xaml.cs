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

namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for ShowImageWindow.xaml
    /// </summary>
    public partial class ShowImageWindow : Window
    {
        private MedicamentService _service;

        public ShowImageWindow(int id)
        {
            _service = new MedicamentService();
            InitializeComponent();
            this.Reload(id);
        }

        public void Reload(int id)
        {
            try
            {
                this.ImageBox.Source = _service.GetMedImage(id);
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Nepodarilo sa zobrazit obrazok.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
