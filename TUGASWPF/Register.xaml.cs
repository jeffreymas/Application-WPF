using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TUGASWPF.Contexts;
using TUGASWPF.Models;

namespace TUGASWPF
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        MyContext myContext = new MyContext();
        public Register()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            txtSNameReg.Text = "";
            txtEmailReg.Text = "";
            pwConfirmReg.Password = "";
            pwReg.Password = "";
        }
        public void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var hasbcrypt = BCrypt.Net.BCrypt.HashPassword(pwReg.Password, 13);
            var regis = myContext.Suppliers.Where(I => I.Email.ToString().Contains(txtEmailReg.Text)).SingleOrDefault();
            if (regis != null)
            {
                MessageBox.Show("Email has been registered. Please use another email.");
                txtEmailReg.Focus();
            }
            else
            {
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to save this data?",
                        "Confirmation",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var input = new Supplier()
                        {
                            Name = txtSNameReg.Text,
                            Email = txtEmailReg.Text,
                            Password = hasbcrypt,
                            JoinDate = dateJoinDate.SelectedDate.Value
                        };
                        myContext.Suppliers.Add(input);
                        myContext.SaveChanges();
                    }
                }
                MessageBox.Show("You have Registered successfully.");
                Reset();
                login login = new login();
                login.Show();
                this.Close();
            }
        }

        private void txtSNameReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void dateJoinDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateJoinDate.SelectedDate.Value == null)
            {
                lblNameStatus.Content = "Please enter join date";
                btnRegister.IsEnabled = false;
            }
            else
            {
                lblNameStatus.Content = "";
                btnRegister.IsEnabled = true;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtEmailReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtEmailReg.Text.Length == 0)
            {
                lblNameStatus.Content = "Please enter an email !";
                btnRegister.IsEnabled = false;
            }
            else if (!Regex.IsMatch(txtEmailReg.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                lblNameStatus.Content = "Enter a valid email";
                txtEmailReg.Focus();
            }
            else
            {
                lblNameStatus.Content = "";
                btnRegister.IsEnabled = true;
            }
        }

        private void pwReg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwReg.Password.Length == 0)
            {
                lblNameStatus.Content = "Please enter the password";
                btnRegister.IsEnabled = false;
                pwReg.Focus();
            }
            else
            {
                lblNameStatus.Content = "";
                btnRegister.IsEnabled = true;
            }
        }

        private void pwConfirmReg_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pwConfirmReg.Password != pwReg.Password)
            {
                lblNameStatus.Content = "Confirm password must be same as password";
                btnRegister.IsEnabled = false;
                pwConfirmReg.Focus();
            }
            else
            {
                lblNameStatus.Content = "";
                btnRegister.IsEnabled = true;
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
