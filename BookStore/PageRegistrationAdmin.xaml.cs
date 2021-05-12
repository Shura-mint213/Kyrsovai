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
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BookStore
{
    public partial class PageRegistrationAdmin : Page
    {
        public PageRegistrationAdmin()
        {
            InitializeComponent();
        }
        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
        private int check_email(string Email, int Error)
        {
            if (Email.Length == 0)
            {
                Error = -1;
                return Error;
            }
            string email_local = Email.Trim();
            if (email_local.Length == 0)
            {
                Error = -2;
                return Error;
            }
            if (email_local.Length > 30)
            {
                Error = -3;
                return Error;
            }
            try
            {
                MailAddress m = new MailAddress(Email);
                Error = 0;
                return Error;
            }
            catch (FormatException)
            {
                Error = -4;
                return Error;
            }
        }
        private int check_surname(string Surname, int Error, Regex RegexPerson)
        {
            if (Surname.Length == 0)
            {
                Error = -1;
                return Error;
            }
            else if (!RegexPerson.IsMatch(Surname))
            {
                Error = -5;
                return Error;
            }
            else
            {
                return 0;
            }
        }
        private int check_login(string Login, int Error)
        {
            if (Login.Length == 0)
            {
                Error = -1;
                return Error;
            }
            else
            {
                Error = 0;
                return Error;
            }
        }
        private int check_name(string Name, int Error, Regex RegexPerson)
        {
            if (Name.Length == 0)
            {
                Error = -1;
                return Error;
            }
            else if (!RegexPerson.IsMatch(Name))
            {
                Error = -5;
                return Error;
            }
            else
            {
                Error = 0;
                return Error;
            }
        }
        private int check_passport_series(string card, int Error)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.IsMatch(card))
            {
                Error = -4;
                return Error;
            }
            else if (card.Length == 0)
            {
                Error = -1;
                return Error;
            }
            else
            {
                Error = 0;
                return Error;
            }
        }
        private int check_passport_number(string card, int Error)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (!regex.IsMatch(card))
            {
                Error = -4;
                return Error;
            }
            if (card.Length == 0)
            {
                Error = -1;
                return Error;
            }
            else
            {
                Error = 0;
                return Error;
            }
        }
        private int check_password(string Password, int Error)
        {
            int points = 0;
            if (Password.Length == 0)
            {
                PasswordErrorLength.Visibility = Visibility.Visible;
                PasswordError.Visibility = Visibility.Hidden;
                Error = -5;
                return Error;
            }
            else if (Password.Length < 5)
            {
                PasswordError.Visibility = Visibility.Visible;
                PasswordErrorLength.Visibility = Visibility.Hidden;
                Error = -5;
                return Error;
            }
            else
            {
                PasswordError.Visibility = Visibility.Hidden;
                Error = 0;
                points++;
                if (Password.Length > 8)
                {
                    points++;
                }
                if (Password.Any(c => char.IsUpper(c)))
                {
                    points++;
                }
                if (Password.Any(c => char.IsDigit(c)))
                {
                    points++;
                }
                switch (points)
                {
                    case 1:
                        WeakPassword.Visibility = Visibility.Visible;
                        AveragePassword.Visibility = Visibility.Hidden;
                        GoodPassword.Visibility = Visibility.Hidden;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        WeakPassword.Visibility = Visibility.Hidden;
                        AveragePassword.Visibility = Visibility.Visible;
                        GoodPassword.Visibility = Visibility.Hidden;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        WeakPassword.Visibility = Visibility.Hidden;
                        AveragePassword.Visibility = Visibility.Hidden;
                        GoodPassword.Visibility = Visibility.Visible;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 4:
                        WeakPassword.Visibility = Visibility.Hidden;
                        AveragePassword.Visibility = Visibility.Hidden;
                        GoodPassword.Visibility = Visibility.Hidden;
                        StrongPassword.Visibility = Visibility.Visible;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                }
                return Error; ;
            }
        }
        private int check_RepeatPassword(string RepeatPassword, string Password, int Error)
        {
            if (Password != RepeatPassword)
            {
                Error = -1;
                return Error;
            }
            else
            {
                Error = 0;
                return Error;
            }
        }
        private void RegistrationContinueAdmin_Click(object sender, RoutedEventArgs e)
        {
            int Error = 1;
            int ErrorSurname = 0;
            int ErrorName = 0;
            int ErrorLogin = 0;
            int ErrorEmail = 0;
            int ErrorPassword = 0;
            int ErrorSeries = 0;
            int ErrorNumber = 0;
            int ErrorRepeatPassword = 0;
            string Surname = "";
            string Name = "";
            string Login = "";
            string Email = "";
            string Series = "";
            string Number = "";
            string Password = "";
            string Role = "1";
            Regex RegexPerson = new Regex(@"^[A-zА-яёЁ]+$");

            Surname = RegistrationSurnameAdmin.Text.Trim();
            ErrorSurname = check_surname(RegistrationSurnameAdmin.Text.Trim(), Error, RegexPerson);
            Name = RegistrationNameAdmin.Text.Trim();
            ErrorName = check_name(RegistrationNameAdmin.Text.Trim(), Error, RegexPerson);
            Login = RegistrationLoginAdmin.Text.Trim();
            ErrorLogin = check_login(RegistrationLoginAdmin.Text.Trim(), Error);
            Email = RegistrationEmailAdmin.Text.Trim();
            ErrorEmail = check_email(RegistrationEmailAdmin.Text.Trim(), Error);
            Series = RegistrationSeriesAdmin.Text.Trim();
            ErrorSeries = check_passport_series(RegistrationSeriesAdmin.Text, Error);
            Number = RegistrationNumberAdmin.Text.Trim();
            ErrorNumber = check_passport_number(RegistrationNumberAdmin.Text, Error);
            Password = RegistrationPasswordAdmin.Text.Trim();
            ErrorPassword = check_password(RegistrationPasswordAdmin.Text.Trim(), Error);
            ErrorRepeatPassword = check_RepeatPassword(RegistrationRepeatPasswordAdmin.Text.Trim(), Password, Error);
            Login = GetHash(Login);
            Password = GetHash(Password);
            if ((ErrorSurname == 0) && (ErrorName == 0) && (ErrorLogin == 0) && (ErrorEmail == 0)
                 && (ErrorSeries == 0) && (ErrorNumber == 0) && (ErrorPassword == 0) && (ErrorRepeatPassword == 0))
            {
                Manager.connection.Open();
                string sqlExpression = " INSERT INTO Employees VALUES (@Employees_login, @Surname_Employees, @Name_Employees, @Email, @Passport_series, @Passport_number, @User_password, @Code_role)";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter LoginParam = new SqlParameter("@Employees_login", Login);
                command.Parameters.Add(LoginParam);
                SqlParameter SurnameParam = new SqlParameter("@Surname_Employees", Surname);
                command.Parameters.Add(SurnameParam);
                SqlParameter NameParam = new SqlParameter("@Name_Employees", Name);
                command.Parameters.Add(NameParam);
                SqlParameter MailParam = new SqlParameter("@Email", Email);
                command.Parameters.Add(MailParam);
                SqlParameter PassportSiriesParam = new SqlParameter("@Passport_series", Series);
                command.Parameters.Add(PassportSiriesParam);
                SqlParameter PassportNumberParam = new SqlParameter("@Passport_number", Number);
                command.Parameters.Add(PassportNumberParam);
                SqlParameter PasswordParam = new SqlParameter("@User_password", Password);
                command.Parameters.Add(PasswordParam);
                SqlParameter RoleParam = new SqlParameter("@Code_role", Role);
                command.Parameters.Add(RoleParam);
                command.ExecuteNonQuery();

                MessageBox.Show("Данные добавлены!");
                Manager.FrameMainWindow.GoBack();
            }

        }

        private void RegistrationCloseAdmin_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }
    }
}
