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
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;

namespace BookStore
{

    public partial class PageRegistration : Page
    {

        public PageRegistration()
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
            Regex regex = new Regex(@"^(?=.{3,24}$)(?=[^0-9A-z]*)[a-zA-Z0-9_.]*[@.a-z]+[a-z]$");
            if (Email.Length == 0)
            {
                EmailError.Visibility = Visibility.Visible;
                EmailRegexError.Visibility = Visibility.Hidden;
                return -1;
            }
            string email_local = Email.Trim();
            if (email_local.Length == 0)
            {
                return -1;
            }
            if (email_local.Length > 30)
            {
                return -1;
            }
            else if (!regex.IsMatch(Email))
            {
                Error = -2;
                EmailRegexError.Visibility = Visibility.Visible;
                EmailError.Visibility = Visibility.Hidden;
                return Error;
            }
            try
            {
                MailAddress m = new MailAddress(Email);
                EmailError.Visibility = Visibility.Hidden;
                EmailRegexError.Visibility = Visibility.Hidden;
                Error = 0;
                return Error;
            }
            catch (FormatException)
            {
                return -1;
            }
        }
        private int check_surname(string Surname, int Error)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,43}$)^[А-ЯЁ][а-яё]*$");
            if (Surname.Length == 0)
            {
                Error = -1;
                SurnameError.Visibility = Visibility.Visible;
                SurnameLengthError.Visibility = Visibility.Hidden;
                SurnameRegexError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (Surname.Length > 43 || Surname.Length < 1)
            {
                Error = -1;
                SurnameError.Visibility = Visibility.Hidden;
                SurnameLengthError.Visibility = Visibility.Visible;
                SurnameRegexError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (!RegexPerson.IsMatch(Surname)) 
            {
                Error = -5;
                SurnameError.Visibility = Visibility.Hidden;
                SurnameLengthError.Visibility = Visibility.Hidden;
                SurnameRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            else
            {
                SurnameError.Visibility = Visibility.Hidden;
                SurnameLengthError.Visibility = Visibility.Hidden;
                SurnameRegexError.Visibility = Visibility.Hidden;
                return 0;
            }
        }
        private int check_login(string Login, int Error)
        {
            Regex regex = new Regex(@"^(?=.{5,25}$)[A-Za-z0-9][A-z0-9]*$");
            if (Login.Length == 0)
            {
                Error = -1;
                LoginError.Visibility = Visibility.Visible;
                LoginRegexError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (!regex.IsMatch(Login))
            {
                Error = -2;
                LoginRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            else
            {
                LoginError.Visibility = Visibility.Hidden;
                LoginRegexError.Visibility = Visibility.Hidden;
                Error = 0;
                return Error;
            }
        }
        private int check_name(string Name, int Error)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,55}$)^[А-ЯЁ][а-яё]*$");
            if (Name.Length == 0)
            {
                Error = -1;
                NameError.Visibility = Visibility.Visible;
                NameLengthError.Visibility = Visibility.Hidden;
                NameRegexError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (Name.Length < 1 || Name.Length > 55)
            {
                Error = -1;
                NameLengthError.Visibility = Visibility.Visible;
                NameRegexError.Visibility = Visibility.Hidden;
                NameError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (!RegexPerson.IsMatch(Name))
            {
                Error = -5;
                NameRegexError.Visibility = Visibility.Visible;
                NameError.Visibility = Visibility.Hidden;
                return Error;
            }
            else
            {
                NameError.Visibility = Visibility.Hidden;
                NameLengthError.Visibility = Visibility.Hidden;
                NameRegexError.Visibility = Visibility.Hidden;
                Error = 0;
                return Error;
            }
        }
        private int check_card(string Card, int Error)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,19}$)[0-9][0-9][0-9][0-9][ ][0-9][0-9][0-9][0-9][ ][0-9][0-9][0-9][0-9][ ][0-9][0-9][0-9][0-9]$");
            if (Card.Length == 0)
            {
                Error = -1;
                CardError.Visibility = Visibility.Visible;
                CardNumberError.Visibility = Visibility.Hidden;
                CardLengthError.Visibility = Visibility.Hidden;
                CardZeroError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (Card.Length != 19)
            {
                Error = -7;
                CardLengthError.Visibility = Visibility.Visible;
                CardError.Visibility = Visibility.Hidden;
                CardNumberError.Visibility = Visibility.Hidden;
                CardZeroError.Visibility = Visibility.Hidden;
                return Error;
            }
            if (Card == "0000 0000 0000 0000")
            {
                CardZeroError.Visibility = Visibility.Visible;
                CardLengthError.Visibility = Visibility.Hidden;
                CardError.Visibility = Visibility.Hidden;
                CardNumberError.Visibility = Visibility.Hidden;
                Error = -2;
                return Error;
            }
            if (!RegexPerson.IsMatch(Card))
            {
                Error = -6;
                CardNumberError.Visibility = Visibility.Visible;
                CardLengthError.Visibility = Visibility.Hidden;
                CardError.Visibility = Visibility.Hidden;
                CardZeroError.Visibility = Visibility.Hidden;
                return Error;
            }
            else
            {
                CardError.Visibility = Visibility.Hidden;
                CardLengthError.Visibility = Visibility.Hidden;
                CardZeroError.Visibility = Visibility.Hidden;
                Error = 0;
                return Error;
            }
        }
        private int check_password(string Password, int Error)
        {
            Regex regex = new Regex(@"^(?=.{1,20}$)(?=[A-z0-9_А-я]*)[^\s.]+[a-zA-ZА-я0-9_]*$");
            int points = 0;
            if (Password.Length == 0)
            {
                PasswordErrorLength.Visibility = Visibility.Visible;
                PasswordError.Visibility = Visibility.Hidden;
                PasswordRegexError.Visibility = Visibility.Hidden;
                Error = -5;
                return Error;
            }
            else if (Password.Length < 5)
            {
                PasswordError.Visibility = Visibility.Visible;
                PasswordErrorLength.Visibility = Visibility.Hidden;
                PasswordRegexError.Visibility = Visibility.Hidden;
                Error = -5;
                return Error;
            }
            else if (!regex.IsMatch(Password)) {
                PasswordError.Visibility = Visibility.Hidden;
                PasswordErrorLength.Visibility = Visibility.Hidden;
                PasswordRegexError.Visibility = Visibility.Visible;
                Error = -5;
                return Error;
            }
            else
            {
                PasswordError.Visibility = Visibility.Hidden;
                PasswordRegexError.Visibility = Visibility.Hidden;
                PasswordErrorLength.Visibility = Visibility.Hidden;
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
                    case 2:
                        WeakPassword.Visibility = Visibility.Visible;
                        AveragePassword.Visibility = Visibility.Hidden;
                        GoodPassword.Visibility = Visibility.Hidden;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        WeakPassword.Visibility = Visibility.Hidden;
                        AveragePassword.Visibility = Visibility.Visible;
                        GoodPassword.Visibility = Visibility.Hidden;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 4:
                        WeakPassword.Visibility = Visibility.Hidden;
                        AveragePassword.Visibility = Visibility.Hidden;
                        GoodPassword.Visibility = Visibility.Visible;
                        StrongPassword.Visibility = Visibility.Hidden;
                        PasswordError.Visibility = Visibility.Hidden;
                        PasswordErrorLength.Visibility = Visibility.Hidden;
                        break;
                    case 5:
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
                RepeatPasswordError.Visibility = Visibility.Visible;
                Error = -1;
                return Error;
            }
            else
            {
                RepeatPasswordError.Visibility = Visibility.Hidden;
                Error = 0;
                return Error;
            }
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            int Error = 1;
            int ErrorSurname = 0;
            int ErrorName = 0;
            int ErrorLogin = 0;
            int ErrorEmail = 0;
            int ErrorPassword = 0;
            int ErrorCard = 0;
            int ErrorAccount = 0;
            int ErrorRepeatPassword = 0;
            string Surname = "";
            string Name = "";
            string Login = "";
            string Email = "";
            string Card = "";
            string Password = "";
            
            
            Surname = RegistrationSurname.Text.Trim();
            ErrorSurname = check_surname(RegistrationSurname.Text.Trim(),Error);
            Name = RegistrationName.Text.Trim();
            ErrorName = check_name(RegistrationName.Text.Trim(), Error);
            Login = RegistrationLogin.Text.Trim();
            ErrorLogin = check_login(RegistrationLogin.Text.Trim(), Error);
            Email = RegistrationMail.Text.Trim();
            ErrorEmail = check_email(RegistrationMail.Text.Trim(),Error);
            Card = RegistrationCard1.Text.Trim();
            ErrorCard = check_card(Card, Error);         
            
            Password = RegistrationPassword.Password.Trim();
            ErrorPassword = check_password(RegistrationPassword.Password.Trim(), Error);
            ErrorRepeatPassword = check_RepeatPassword(RegistrationRepeatPassword.Password.Trim(), Password, Error);
            Password = GetHash(Password);

            string sqlExpression = "SELECT * FROM Buyers WHERE Buyers_login = @Login";
            SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
            SqlParameter Login_param = new SqlParameter("@Login", Login);
            command.Parameters.Add(Login_param);
            
            SqlDataReader Reader = command.ExecuteReader();
            if (Reader.HasRows)
            {
                ErrorAccount = -5;
                ConectionError.Visibility = Visibility.Visible;
                Reader.Close();
            }
            else
            {
                ConectionError.Visibility = Visibility.Hidden;
                Reader.Close();
                sqlExpression = "SELECT * FROM Employees WHERE Employees_login = @Login";
                command = new SqlCommand(sqlExpression, Manager.connection);
                Login_param = new SqlParameter("@Login", Login);
                command.Parameters.Add(Login_param);

                Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    ErrorAccount = -5;
                    Reader.Close();
                }
                else
                {
                    ErrorAccount = 0;
                    Reader.Close();
                }
            }

            if ((ErrorSurname == 0) && (ErrorName == 0) && (ErrorLogin == 0) && (ErrorEmail == 0) && (ErrorAccount == 0)
                 && (ErrorCard == 0) && (ErrorPassword == 0) && (ErrorRepeatPassword == 0))
            {
                sqlExpression = " INSERT INTO Buyers VALUES (@Buyers_login, @Surname_buyer, @Name_buyer, @Email, @Number_card, @User_password)";
                command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter LoginParam = new SqlParameter("@Buyers_login", Login);
                command.Parameters.Add(LoginParam);
                SqlParameter SurnameParam = new SqlParameter("@Surname_buyer", Surname);
                command.Parameters.Add(SurnameParam);
                SqlParameter NameParam = new SqlParameter("@Name_buyer", Name);
                command.Parameters.Add(NameParam);
                SqlParameter MailParam = new SqlParameter("@Email", Email);
                command.Parameters.Add(MailParam);
                SqlParameter CardParam = new SqlParameter("@Number_card", Card);
                command.Parameters.Add(CardParam);
                SqlParameter PasswordParam = new SqlParameter("@User_password", Password);
                command.Parameters.Add(PasswordParam);
                command.ExecuteNonQuery();
                Reader.Close();
                MessageBox.Show("Данные добавлены!");
                Manager.FrameMainWindow.GoBack();
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }
    }
}
