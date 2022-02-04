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
        public int valid;

        public ShowImageWindow(int id)
        {
            valid = 0;
            _service = new MedicamentService();
            InitializeComponent();
            this.Reload(id);
        }

        public void Reload(int id)
        {
            try
            {
                this.ImageBox.Source = _service.GetMedImage(id);
                valid = 1;
            }
            catch (Exception)
            {
                this.valid = 0;
                MessageBoxResult result = MessageBox.Show("Pre zadaný liek nie je obrázok v DB.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public int isValid(int id)
        {
            return this.valid;
        }

    }
}
