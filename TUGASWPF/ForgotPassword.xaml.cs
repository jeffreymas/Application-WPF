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
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using TUGASWPF.Contexts;
using System.Text.RegularExpressions;

namespace TUGASWPF
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    
    public partial class ForgotPassword : Window
    {
        MyContext myContext = new MyContext();
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var email = myContext.Suppliers.Where(Q => Q.Email.Contains(txtEmail.Text)).SingleOrDefault();
            if (email == null)
            {
                lblStatus.Content = "Sorry! Please enter existing email";
                txtEmail.Focus();
                return;
            }
            Guid guid = Guid.NewGuid();
            SendEmail(email.Email, guid);
            email.Newpass = 1;
            email.Password = BCrypt.Net.BCrypt.HashString(guid.ToString(), 12);
            myContext.SaveChanges();
            MessageBox.Show("Password has been sent to " + email.Email);

            login login = new login();
            Close();
            login.Show();
        }
        public void SendEmail(string email, Guid guid)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("jeffreyalaflah@gmail.com", "WPF Foundation");
            mail.Subject = "Your WPF account: New Password " + DateTime.Now.ToString("dddd, MMMM dd yyyy");
            mail.Body = "Dear User, <br><br>We have been reset your password to :<br><br><b>" + guid.ToString() + "</b><br><br> Thank you, <br> WPF Help Desk";
            mail.IsBodyHtml = true;

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("jeffreyalaflah@gmail.com", "indramayuplaza");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }
    }
}
