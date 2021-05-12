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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;

namespace BookStore
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Manager.connection.Open();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new PageRegistration());
        }
        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            string Password = "";
            string Login = LoginMain.Text.Trim();
            //string Code_role = "1";
            if (PassowdMain.MaxLength > 0)
            {
                Password = PassowdMain.Text;
            }
            Password = GetHash(PasswordBox.Password);
            try
            {
                string sqlExpression = "SELECT * FROM dbo.Buyers WHERE(Buyers_login COLLATE SQL_Latin1_General_CP1_CS_AS = @Login)" +
                                        " AND User_password = @Password";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Login_param = new SqlParameter("@Login", Login);
                command.Parameters.Add(Login_param);
                SqlParameter Password_param = new SqlParameter("@Password", Password);
                command.Parameters.Add(Password_param);

                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    Manager.FrameMainWindow.Navigate(new ProductPage(Login));
                    Reader.Close();
                }
                else
                {
                    Reader.Close();
                    sqlExpression = "SELECT * FROM Employees WHERE(Employees_login COLLATE SQL_Latin1_General_CP1_CS_AS = @Login)" +
                                    " AND User_password = @Password";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    Login_param = new SqlParameter("@Login", Login);
                    command.Parameters.Add(Login_param);
                    Password_param = new SqlParameter("@Password", Password);
                    command.Parameters.Add(Password_param);
                    Reader.Close();
                    Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {

                        Manager.FrameMainWindow.Navigate(new PageAccountAdministrator(Login));
                        Reader.Close();
                    }
                    else
                    {
                        MainError.Visibility = Visibility;
                        Reader.Close();
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }
        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
        private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();


        private void ShowPasswordFunction()
        {
            PassowdMain.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Hidden;
            PassowdMain.Text = PasswordBox.Password;
        }

        private void HidePasswordFunction()
        {
            PassowdMain.Visibility = Visibility.Hidden;
            PasswordBox.Visibility = Visibility.Visible;
        }

        private void PawordButton_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            if (number == 1)
            {
                HidePasswordFunction();
                number = 0;
            }
            else
            {
                ShowPasswordFunction();
                number++;
            }
        }
    }
}
