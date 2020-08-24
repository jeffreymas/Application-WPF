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
    /// Interaction logic for UserControlItem.xaml
    /// </summary>
    public partial class UserControlItem : UserControl
    {

        MyContext myContext = new MyContext();
        int cbSupp;
        public UserControlItem()
        {
            InitializeComponent();
            dgItem.ItemsSource = myContext.Item.ToList();
            cbSupplier.ItemsSource = myContext.Suppliers.ToList();
        }

        private void clearText()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cbSupplier.SelectedValue = null; 
        }
        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var supplier = myContext.Suppliers.Find(Convert.ToInt32(cbSupp)); 
            if (txtName.Text.Equals("") || supplier == null)
            {
                MessageBox.Show("Input cant be null");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to Insert this data ?", "Insert Arlet!!!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        var input = new Item(txtName.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtStock.Text), supplier);
                        myContext.Item.Add(input);
                        myContext.SaveChanges();
                        MessageBox.Show("1 row has benn inserted");
                        txtName.Text = "";

                        dgItem.ItemsSource = myContext.Item.ToList();
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
                    var supplier = myContext.Suppliers.Find(Convert.ToInt32(cbSupp));
                    int Id = Convert.ToInt32(txtId.Text);
                    var Item = myContext.Item.Find(Id);
                    Item.Name = txtName.Text;
                    Item.Price = Convert.ToInt32(txtPrice.Text);
                    Item.Stock = Convert.ToInt32(txtStock.Text);
                    Item.Supplier = supplier;
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been updated");
                    dgItem.ItemsSource = myContext.Item.ToList();
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
                    var item = myContext.Item.Find(Id);
                    myContext.Item.Remove(item);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been deleted");
                    dgItem.ItemsSource = myContext.Item.ToList();
                    this.clearText();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void txtId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void dgSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgItem.SelectedItem as Item;
            if (dgItem.SelectedItem != null)
            {
                if (item.Supplier == null )
                {
                    
                    txtName.Text = item.Name;
                    txtId.Text = Convert.ToString(item.Id);
                    txtPrice.Text = Convert.ToString(item.Price);
                    txtStock.Text = Convert.ToString(item.Stock);
                }else{  
                    txtName.Text = item.Name;
                    txtId.Text = Convert.ToString(item.Id);
                    txtPrice.Text = Convert.ToString(item.Price);
                    txtStock.Text = Convert.ToString(item.Stock); 
                    cbSupplier.Text = item.Supplier.Name;
                }
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
            var filteredData = myContext.Item.Where(Q => Q.Id.ToString().Contains(txtSearch.Text) || Q.Name.Contains(txtSearch.Text) || Q.Price.ToString().Contains(txtSearch.Text) || Q.Stock.ToString().Contains(txtSearch.Text)).ToList();  
            dgItem.ItemsSource = filteredData;
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

        private void cbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbSupp = Convert.ToInt32(cbSupplier.SelectedValue.ToString());
        }
    }
}
