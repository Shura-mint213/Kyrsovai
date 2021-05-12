using Microsoft.Win32;
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
using System.IO;
using System.Text.RegularExpressions;

namespace BookStore
{
    public partial class AddProductPage : Page
    {
        public string Login { get; set; }
        public AddProductPage(string Get_Login)
        {
            InitializeComponent();
            Login = Get_Login;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }

        private void Add_Photo_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                PhotoBookTextBox.Text = dialog.FileName;
            }
            else
                PhotoBookTextBox = null;
        }
        private int Check_name_book(string Name_book)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,100}$)(?=[^.-А-яё]*)[-A-zА-я0-9 '_.ё!?]*$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Name_book))
            {
                Error = -5;
                NameBookRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            NameBookRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_name_avtor(string Name_avtor)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё-]+$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Name_avtor))
            {
                Error = -5;
                NameAvtorRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            NameAvtorRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_surname_avtor(string Surname_avtor)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё-]+$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Surname_avtor))
            {
                Error = -5;
                SurnameAvtorRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            SurnameAvtorRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_language_book(string Language_book)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,50}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё]+$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Language_book))
            {
                Error = -5;
                LanguageBookRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            LanguageBookRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_price_book(string Price_book)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^0-9]*)[0-9]*.[0-9][0-9]$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Price_book))
            {
                Error = -5;
                PriceBookRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            PriceBookRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_cover_type_book(string Cover_type)
        {
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё ]+$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Cover_type))
            {
                Error = -5;
                CoverTypeBookRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            CoverTypeBookRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_release_data_book(string Release_data)
        {
            Regex RegexPerson = new Regex(@"^(?=.{4}$)(?=[^0-9]*)[0-9][0-9][0-9][0-9]$");
            int Error = 0;
            if (!RegexPerson.IsMatch(Release_data))
            {
                Error = -5;
                ReleaseDataRegexError.Visibility = Visibility.Visible;
                DataRegexError.Visibility = Visibility.Hidden;
                return Error;
            }
            else if (Convert.ToInt32(Release_data) < 2000 && (Convert.ToInt32(Release_data) > 2021))
            {
                Error = -5;
                ReleaseDataRegexError.Visibility = Visibility.Hidden;
                DataRegexError.Visibility = Visibility.Visible;
                return Error;
            }
            ReleaseDataRegexError.Visibility = Visibility.Hidden;
            DataRegexError.Visibility = Visibility.Hidden;
            return 0;
        }
        private int Check_path_file(string Path_file)
        {
            int Error = 0;
            if (Path_file.Length <= 0)
            {
                Error = -5;
                FileDownloadError.Visibility = Visibility.Visible;
                return Error;
            }
            FileDownloadError.Visibility = Visibility.Hidden;
            return 0;
        }
        private void Add_Product_Button_Click(object sender, RoutedEventArgs e)
        {
            int error = 0;
            int Code_book = 0;
            string sqlExpression = "SELECT Code_book FROM dbo.Books";
            SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    if (Code_book < Convert.ToInt32(reader[0].ToString().Substring(4)))
                    {
                        Code_book = Convert.ToInt32(reader[0].ToString().Substring(4));
                    }
                    else { }
                }
                Code_book++;
            }
            reader.Close();
            int ErrorNameBook = Check_name_book(NameBookTextbox.Text);
            int ErrorNameAvtor = Check_name_avtor(NameAvtorTextBox.Text);
            int ErrorSurnameAvtor = Check_surname_avtor(SurnameAvtorTextBox.Text);
            int ErrorLanguageBook = Check_language_book(LanguageBookTextBox.Text);
            int ErrorPriceBook = Check_price_book(PriceBookTextBox.Text);
            int ErrorCoverTypeBook = Check_cover_type_book(CoverTypeTextBox.Text);
            int ErrorReleaseDataBook = Check_release_data_book(ReleaseDataTextBox.Text);
            if (Check_path_file(PhotoBookTextBox.Text) >= 0)
            {
                FileInfo fInfo = new FileInfo(PhotoBookTextBox.Text);
                long numBytes = fInfo.Length;

                if (numBytes > 10485760)
                {
                    MessageBox.Show("Error download file!");
                    error = -11;
                }
                byte[] imageData = null;

                FileStream fStream = new FileStream(PhotoBookTextBox.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageData = br.ReadBytes((int)numBytes);


                if (error >= 0 && (ErrorNameBook >= 0) && (ErrorNameAvtor >= 0) && (ErrorSurnameAvtor >= 0) && (ErrorLanguageBook >= 0) &&
                    (ErrorPriceBook >= 0) && (ErrorCoverTypeBook >= 0) && (ErrorReleaseDataBook >= 0))
                {
                    sqlExpression = "INSERT INTO Books (Code_book,Photos,Name_book,Name_avtor,Surname_avtor,Language_book,Price,Data_exit,Cover_type,Employees_login) " +
                        "VALUES (@Code_book, @Photos, @Name_book, @Name_avtor, @Surname_avtor, @Language_book, @Price, @Data_exit, @Cover_type, @Employees_login)";
                    try
                    {
                        command = new SqlCommand(sqlExpression, Manager.connection);
                        SqlParameter CodeBookParam = new SqlParameter("@Code_book", "Book" + Code_book);
                        command.Parameters.Add(CodeBookParam);
                        SqlParameter PhotosParam = new SqlParameter("@Photos", imageData);
                        command.Parameters.Add(PhotosParam);
                        SqlParameter NameBookParam = new SqlParameter("@Name_book", NameBookTextbox.Text);
                        command.Parameters.Add(NameBookParam);
                        SqlParameter NameAvtorParam = new SqlParameter("@Name_avtor", NameAvtorTextBox.Text);
                        command.Parameters.Add(NameAvtorParam);
                        SqlParameter SurnameAvtorParam = new SqlParameter("@Surname_avtor", SurnameAvtorTextBox.Text);
                        command.Parameters.Add(SurnameAvtorParam);
                        SqlParameter LanguageBookParam = new SqlParameter("@Language_book", LanguageBookTextBox.Text);
                        command.Parameters.Add(LanguageBookParam);
                        SqlParameter PriceParam = new SqlParameter("@Price", PriceBookTextBox.Text);
                        command.Parameters.Add(PriceParam);
                        SqlParameter Data_exitParam = new SqlParameter("@Data_exit", ReleaseDataTextBox.Text);
                        command.Parameters.Add(Data_exitParam);
                        SqlParameter CoverTypeParam = new SqlParameter("@Cover_type", CoverTypeTextBox.Text);
                        command.Parameters.Add(CoverTypeParam);
                        SqlParameter EmployeesLoginParam = new SqlParameter("@Employees_login", Login);
                        command.Parameters.Add(EmployeesLoginParam);
                        command.ExecuteNonQuery();
                        reader.Close();
                        MessageBox.Show("Товар добавлен");
                        Manager.FrameMainWindow.Navigate(new PageAccountAdministrator(Login));
                    }
                    catch (SqlException Error)
                    {
                        MessageBox.Show(Error.Message);
                    }
                }
            }
        }
    }
}
