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


    public partial class EditBuyerPage : Page
    {
        
            public string login { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Number_curd { get; set; }
            public string Password { get; set; }
        
        public string Login { get; set; }
        public EditBuyerPage(string Login_get)
        {
            InitializeComponent();
            Login = Login_get;
            string sqlExpression = "SELECT * FROM Buyers";
            SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    login = reader[0].ToString();
                    Surname = reader[1].ToString();
                    Name = reader[2].ToString();
                    Email = reader[3].ToString();
                    Number_curd = reader[4].ToString();
                    Password = reader[5].ToString();
                }
                reader.Close();
                RegistrationSurname.Text = Surname;
                RegistrationName.Text = Name;
                RegistrationMail.Text = Email;
                RegistrationCard1.Text = Number_curd;
            }
            else
            {
                MessageBox.Show("stop");
            }
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
            Regex regex = new Regex(@"^(?=.{1,25}$)[A-Za-z0-9][A-z0-9]*$");
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
            else if (!regex.IsMatch(Password))
            {
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
            int ErrorOldPassword = 0;
            string OldPassword = "";
            string SurnameEdit = "";
            string NameEdit = "";
            string LoginEdit = "";
            string EmailEdit = "";
            string CardEdit = "";
            string PasswordEdit = "";

            SurnameEdit = RegistrationSurname.Text.Trim();
            ErrorSurname = check_surname(RegistrationSurname.Text.Trim(), Error);
            NameEdit = RegistrationName.Text.Trim();
            ErrorName = check_name(RegistrationName.Text.Trim(), Error);
            OldPassword = RegistrationOldPassword.Password.Trim();
            EmailEdit = RegistrationMail.Text.Trim();
            ErrorEmail = check_email(RegistrationMail.Text.Trim(), Error);
            CardEdit = RegistrationCard1.Text.Trim();
            ErrorCard = check_card(CardEdit, Error);
            PasswordEdit = RegistrationPassword.Password.Trim();
            if (OldPassword.Length == 0)
            {
                ErrorPassword = 0;
                ErrorRepeatPassword = 0;
                ErrorOldPassword = 0;
                OldPassword = Password;
                PasswordEdit = Password; 
            }
            else
            {
                PasswordEdit = GetHash(PasswordEdit);
                OldPassword = GetHash(OldPassword);
                if (OldPassword == Password)
                {
                    ErrorOldPassword = check_password(RegistrationOldPassword.Password.Trim(), Error);
                    ErrorPassword = check_password(RegistrationPassword.Password.Trim(), Error);
                    ErrorRepeatPassword = check_RepeatPassword(GetHash(RegistrationRepeatPassword.Password.Trim()), PasswordEdit, Error);
                }
                else
                {
                    ErrorOldPassword = -1;
                }

            }
            try
            {
                string sqlExpression = "SELECT * FROM Buyers WHERE Buyers_login = @Login AND User_password = @User_password";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter ChechLogin_param = new SqlParameter("@Login", Login);
                command.Parameters.Add(ChechLogin_param);
                SqlParameter ChechPassword_param = new SqlParameter("@User_password", OldPassword);
                command.Parameters.Add(ChechPassword_param);
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    ErrorAccount = 0;
                    Reader.Close();
                }
                else
                {
                    ErrorAccount = -5;
                }
                if ((ErrorSurname == 0) && (ErrorName == 0) && (ErrorLogin == 0) && (ErrorEmail == 0) && (ErrorAccount == 0)
                     && (ErrorOldPassword == 0) && (ErrorCard == 0) && (ErrorPassword == 0) && (ErrorRepeatPassword == 0))
                {
                    sqlExpression = "Update Buyers set Surname_buyer = @New_Surname, Name_buyer = @New_Name_buyer," +
                    " Email = @New_Email, Number_card = @New_Number_card, User_password = @New_User_password where" +
                    " Surname_buyer = @Surname AND Name_buyer = @Name_buyer AND Email = @Email AND Number_card = @Number_card" +
                    " AND User_password = @User_password";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    SqlParameter New_Login_param = new SqlParameter("@New_login", LoginEdit);
                    command.Parameters.Add(New_Login_param);
                    SqlParameter Login_param = new SqlParameter("@login", login);
                    command.Parameters.Add(Login_param);
                    SqlParameter Surname_param = new SqlParameter("@Surname", Surname);
                    command.Parameters.Add(Surname_param);
                    SqlParameter New_Surname_param = new SqlParameter("@New_Surname", SurnameEdit);
                    command.Parameters.Add(New_Surname_param);
                    SqlParameter Name_buyer_param = new SqlParameter("@Name_buyer", Name);
                    command.Parameters.Add(Name_buyer_param);
                    SqlParameter New_Name_buyer_param = new SqlParameter("@New_Name_buyer", NameEdit);
                    command.Parameters.Add(New_Name_buyer_param);
                    SqlParameter Email_param = new SqlParameter("@Email", Email);
                    command.Parameters.Add(Email_param);
                    SqlParameter New_Email_param = new SqlParameter("@New_Email", EmailEdit);
                    command.Parameters.Add(New_Email_param);
                    SqlParameter Number_card_param = new SqlParameter("@Number_card", Number_curd);
                    command.Parameters.Add(Number_card_param);
                    SqlParameter New_Number_card_param = new SqlParameter("@New_Number_card", CardEdit);
                    command.Parameters.Add(New_Number_card_param);
                    SqlParameter User_password_param = new SqlParameter("@User_password", Password);
                    command.Parameters.Add(User_password_param);
                    SqlParameter New_User_password_param = new SqlParameter("@New_User_password", PasswordEdit);
                    command.Parameters.Add(New_User_password_param);
                    command.ExecuteNonQuery();
                    Reader.Close();
                    MessageBox.Show("Данные отредактированы!");
                    Manager.FrameMainWindow.GoBack();
                }
            }
            catch (SqlException er)
            {
                MessageBox.Show((er.Number).ToString() + " " + er.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }
    }
}