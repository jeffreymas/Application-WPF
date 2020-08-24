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
    /// Interaction logic for UserControlSupplier.xaml
    /// </summary>
    public partial class UserControlSupplier : UserControl
    {
        MyContext myContext = new MyContext();
        public UserControlSupplier()
        {
            InitializeComponent();
            dgSupplier.ItemsSource = myContext.Suppliers.ToList();
        }
        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("Input cant be null");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to Insert this data ?", "Insert Arlet!!!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        var input = new Supplier(txtName.Text, DpJoinDate.SelectedDate);
                        myContext.Suppliers.Add(input);
                        myContext.SaveChanges();
                        MessageBox.Show("1 row has benn inserted");
                        txtName.Text = "";

                        dgSupplier.ItemsSource = myContext.Suppliers.ToList();
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
                    int Id = Convert.ToInt32(txtId.Text);
                    var supplier = myContext.Suppliers.Find(Id);
                    supplier.Name = txtName.Text;
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been updated");
                    dgSupplier.ItemsSource = myContext.Suppliers.ToList();
                    txtName.Text = "";
                    txtId.Text = "";
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure want to delete this data ?", "Delete Arlet!!!", MessageBoxButton.YesNo);
            var ii = Convert.ToInt32(txtId.Text);
            var items = myContext.Item.Where(I => I.Supplier.Id == ii).ToList();
            switch (result)
            {
                case MessageBoxResult.Yes:
                    foreach (Item i in items)
                    {
                        int iid = i.Id;
                        var item = myContext.Item.Find(iid);
                        myContext.Item.Remove(item);
                        myContext.SaveChanges();
                    }
                    int Id = Convert.ToInt32(txtId.Text);
                    var supplier = myContext.Suppliers.Find(Id);
                    myContext.Suppliers.Remove(supplier);
                    myContext.SaveChanges();
                    MessageBox.Show("1 row has been deleted");
                    dgSupplier.ItemsSource = myContext.Suppliers.ToList();
                    txtName.Text = "";
                    txtId.Text = "";
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
            if (dgSupplier.SelectedItem != null)
            {
                var supplier = dgSupplier.SelectedItem as Supplier;
                txtName.Text = supplier.Name;
                txtId.Text = Convert.ToString(supplier.Id);
                DpJoinDate.SelectedDate = supplier.JoinDate; 
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
            var filteredData = myContext.Suppliers.Where(Q => Q.Id.ToString().Contains(txtSearch.Text) || Q.Name.Contains(txtSearch.Text)).ToList(); ;
            dgSupplier.ItemsSource = filteredData;
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
        }
    }
}
