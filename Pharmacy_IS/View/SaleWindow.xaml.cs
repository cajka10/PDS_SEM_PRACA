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
using Pharmacy_IS.ViewModel.Service;
namespace Pharmacy_IS.View
{
    /// <summary>
    /// Interaction logic for SaleWindow.xaml
    /// </summary>
    /// 
    public class MyItem
    {
        public int Quantity { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }
        public MyItem(int pId,string pName,int pQuantity)
        {
            this.Id = pId;
            this.Name = pName;
            this.Quantity = pQuantity;
        }
    }

    public partial class SaleWindow : Window
    {
        MedicamentService _medicamentService;
        Dictionary<int, string> sortimentList;
        public SaleWindow()
        {
            _medicamentService = new MedicamentService();

            InitializeComponent();
            sortimentList = _medicamentService.getMedicamentsNames();
            foreach (var item in sortimentList)
            {
                var itm = new ListBoxItem();
                itm.Content = item.Value;
                this.sortimentListBox.Items.Add(itm);
            }

        }

        private void sortimentTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = sortimentListBox.SelectedIndex;
            if (sortimentListBox.SelectedItem != null)
            {
                var text = ((ListBoxItem)sortimentListBox.SelectedValue).Content.ToString();
                orderListView.Items.Add(new MyItem(sortimentList.ElementAt(index).Key, text, 1));
            }
        }

        private void increaseButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void decreaseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
