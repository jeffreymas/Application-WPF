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
    /// Interaction logic for UserControlTransactionItem.xaml
    /// </summary>
    public partial class UserControlTransactionItem : UserControl
    {
        MyContext myContext = new MyContext();
        int cbTrans;
        int cbItems;
        public UserControlTransactionItem()
        {
            InitializeComponent();
            dgTransactionItem.ItemsSource = myContext.TransactionItem.ToList();
            cbTransaction.ItemsSource = myContext.Transaction.Select(i => i.Id ).ToList();
            cbItem.ItemsSource = myContext.Item.ToList(); 
        }

        private void clearText()
        {
            txtId.Clear();
            txtQuantity.Clear();
            cbTransaction.SelectedValue = null;
            cbItem.SelectedValue = null;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var transaction = myContext.Transaction.Find(Convert.ToInt32(cbTrans));
            var item = myContext.Item.Find(Convert.ToInt32(cbTrans));
            if (txtQuantity.Text.Equals("") || transaction == null || item == null)
            {
                MessageBox.Show("Input cant be null");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to Insert this data ?", "Insert Arlet!!!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        var input = new TransactionItem(Convert.ToInt32(txtQuantity.Text), transaction, item);
                        myContext.TransactionItem.Add(input);
                        myContext.SaveChanges();
                        MessageBox.Show("1 row has benn inserted");
                 
                        dgTransactionItem.ItemsSource = myContext.TransactionItem.ToList();
                        break;
                    case MessageBoxResult.No:
                        break;
                }

            }

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure want to Update this data ?", "Update Arlet!!!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    var transaction = myContext.Transaction.Find(Convert.ToInt32(cbTrans));
                    var item = myContext.Item.Find(Convert.ToInt32(cbTrans));
                    int Id = Convert.ToInt32(txtId.Text);
                    var TransactionItem = myContext.TransactionItem.Find(Id);
                    TransactionItem.Quantity = Convert.ToInt32(txtQuantity.Text);
                    TransactionItem.Transaction = transaction;
                    TransactionItem.Item = item;
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been updated");
                    dgTransactionItem.ItemsSource = myContext.TransactionItem.ToList();
                    this.clearText();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Wrong Input Data");
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
                    var transactionitem = myContext.TransactionItem.Find(Id);
                    myContext.TransactionItem.Remove(transactionitem);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been deleted");
                    dgTransactionItem.ItemsSource = myContext.TransactionItem.ToList();
                    this.clearText();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void txtId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var filteredData = myContext.TransactionItem.Where(Q => Q.Id.ToString().Contains(txtSearch.Text) || Q.Quantity.ToString().Contains(txtSearch.Text)).ToList();
            dgTransactionItem.ItemsSource = filteredData;
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.clearText();
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtSupplier_Id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
         

        private void cbItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbItems = Convert.ToInt32(cbItem.SelectedValue.ToString());
        }

        private void cbTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbTrans = Convert.ToInt32(cbTransaction.SelectedValue.ToString());
        }

        private void dgTransactionItem_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var transactionitem = dgTransactionItem.SelectedItem as TransactionItem;
            if (dgTransactionItem.SelectedItem != null)
            {
                if (transactionitem.Transaction == null)
                {

                    txtId.Text = Convert.ToString(transactionitem.Id);
                    txtQuantity.Text = Convert.ToString(transactionitem.Quantity);
                }
                else
                {
                    txtId.Text = Convert.ToString(transactionitem.Id);
                    txtQuantity.Text = Convert.ToString(transactionitem.Quantity);
                }
            }
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
