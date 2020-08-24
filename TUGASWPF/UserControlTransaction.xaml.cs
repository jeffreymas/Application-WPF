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
using TUGASWPF.Contexts;
using TUGASWPF.Models;

namespace TUGASWPF
{
    /// <summary>
    /// Interaction logic for UserControlTransaction.xaml
    /// </summary>

    public partial class UserControlTransaction : UserControl
    {
        MyContext myContext = new MyContext();
        public UserControlTransaction()
        {
            InitializeComponent();
            dgTransaction.ItemsSource = myContext.Transaction.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("Are you sure want to Insert this data ?", "Insert Arlet!!!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var input = new Transaction(DpOrderDate.SelectedDate);
                    myContext.Transaction.Add(input);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has benn inserted");
                   
                    dgTransaction.ItemsSource = myContext.Transaction.ToList();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure want to Update this data ?", "Update Arlet!!!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int Id = Convert.ToInt32(txtId.Text);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been updated");
                    dgTransaction.ItemsSource = myContext.Transaction.ToList();
                    txtId.Text = "";
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure want to delete this data ?", "Delete Arlet!!!", MessageBoxButton.YesNo);
            
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int Id = Convert.ToInt32(txtId.Text);
                    var transaction = myContext.Transaction.Find(Id);
                    myContext.Transaction.Remove(transaction);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been deleted");
                    dgTransaction.ItemsSource = myContext.Transaction.ToList();
                    txtId.Text = "";
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void txtId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void dgTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTransaction.SelectedItem != null)
            {
                var supplier = dgTransaction.SelectedItem as Transaction;
                txtId.Text = Convert.ToString(supplier.Id);
                DpOrderDate.SelectedDate = supplier.OrderDate;
            }
        }

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var filteredData = myContext.Suppliers.Where(Q => Q.Id.ToString().Contains(txtSearch.Text)).ToList(); ;
            dgTransaction.ItemsSource = filteredData;
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
        }
    }
}
