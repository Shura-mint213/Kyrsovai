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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;

namespace BookStore
{
    public partial class EditProductPage : Page
    {
        public string Login { get; set; }
        public string Old_login { get; set; }
        public byte[] Photos { get; set; }
        public string NameBook { get; set; }
        public string NameAvtor { get; set; }
        public string SurnameAvtor { get; set; }
        public string LanguageBook { get; set; }
        public string PriceBook { get; set; }
        public string CoverTypeBook { get; set; }
        public string ReleaseDataBook { get; set; }
        public EditProductPage(string Login_get, string Code_book_get)
        {
            InitializeComponent();
            Login = Login_get;

            string sqlExpression = "SELECT * FROM Books Where Code_book = @Code_book_value";
            SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
            SqlParameter CodeBookParameter = new SqlParameter("@Code_book_value", Code_book_get);
            command.Parameters.Add(CodeBookParameter);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    Photos = reader[1] as byte[];
                    NameBookTextbox.Text = reader[2].ToString();
                    NameAvtorTextBox.Text = reader[3].ToString();
                    SurnameAvtorTextBox.Text = reader[4].ToString();
                    LanguageBookTextBox.Text = reader[5].ToString();
                    PriceBookTextBox.Text = reader[6].ToString().Replace(",", ".").Substring(0, reader[6].ToString().Length - 2);

                    CoverTypeTextBox.Text = reader[7].ToString();
                    ReleaseDataTextBox.Text = reader[8].ToString();
                    Old_login = reader[9].ToString();
                }
            }
            NameBook = NameBookTextbox.Text;
            NameAvtor = NameAvtorTextBox.Text;
            SurnameAvtor = SurnameAvtorTextBox.Text;
            LanguageBook = LanguageBookTextBox.Text;
            PriceBook = PriceBookTextBox.Text;
            CoverTypeBook = CoverTypeTextBox.Text;
            ReleaseDataBook = ReleaseDataTextBox.Text;
            reader.Close();
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
            Regex RegexPerson = new Regex(@"^(?=.{1,100}$)(?=[^.-А-яё]*)[-A-zА-я0-9_.ё! '?]+$");
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
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё]+$");
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
            Regex RegexPerson = new Regex(@"^(?=.{1,24}$)(?=[^А-ЯЁ]*)[А-ЯЁ][А-яё-]+$");
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
            Regex RegexPerson = new Regex(@"^(?=.{1,50}$)(?=[^А-ЯЁ]*)[А-ЯЁ][а-яё ]+$");
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
            byte[] imageData = null;
            if (PhotoBookTextBox.Text != "")
            {
                FileInfo fInfo = new FileInfo(PhotoBookTextBox.Text);
                long numBytes = fInfo.Length;

                if (numBytes > 10485760)
                {
                    MessageBox.Show("Error download file!");
                    error = -11;
                }


                FileStream fStream = new FileStream(PhotoBookTextBox.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageData = br.ReadBytes((int)numBytes);
            }

            if ((ErrorNameBook >= 0) && (ErrorNameAvtor >= 0) && (ErrorSurnameAvtor >= 0) && (ErrorLanguageBook >= 0) &&
                (ErrorPriceBook >= 0) && (ErrorCoverTypeBook >= 0) && (ErrorReleaseDataBook >= 0))
            {
                if (imageData == null)
                    imageData = Photos;
                sqlExpression = "Update Books set  Name_book = @New_Name_book, Name_avtor = @New_Name_avtor," +
                " Surname_avtor = @New_Surname_avtor, Language_book = @New_Language_book, Price = @New_Price, Data_exit = @New_Data_exit," +
                " Cover_type = @New_Cover_type WHERE" +
                " Name_book = @Name_book AND Name_avtor = @Name_avtor AND Surname_avtor = @Surname_avtor" +
                " AND Language_book = @Language_book AND Price = @Price AND Data_exit = @Data_exit AND" +
                    " Cover_type = @Cover_type";

                try
                {
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    SqlParameter NewPhotosParam = new SqlParameter("@New_Photos", imageData);
                    command.Parameters.Add(NewPhotosParam);
                    SqlParameter PhotosParam = new SqlParameter("@Photos", Photos);
                    command.Parameters.Add(PhotosParam);
                    SqlParameter NewNameBookParam = new SqlParameter("@New_Name_book", NameBookTextbox.Text);
                    command.Parameters.Add(NewNameBookParam);
                    SqlParameter NewNameAvtorParam = new SqlParameter("@New_Name_avtor", NameAvtorTextBox.Text);
                    command.Parameters.Add(NewNameAvtorParam);
                    SqlParameter NewSurnameAvtorParam = new SqlParameter("@New_Surname_avtor", SurnameAvtorTextBox.Text);
                    command.Parameters.Add(NewSurnameAvtorParam);
                    SqlParameter NewLanguageBookParam = new SqlParameter("@New_Language_book", LanguageBookTextBox.Text);
                    command.Parameters.Add(NewLanguageBookParam);
                    SqlParameter NewPriceParam = new SqlParameter("@New_Price", PriceBookTextBox.Text);
                    command.Parameters.Add(NewPriceParam);
                    SqlParameter NewData_exitParam = new SqlParameter("@New_Data_exit", ReleaseDataTextBox.Text);
                    command.Parameters.Add(NewData_exitParam);
                    SqlParameter NewCoverTypeParam = new SqlParameter("@New_Cover_type", CoverTypeTextBox.Text);
                    SqlParameter NameBookParam = new SqlParameter("@Name_book", NameBook);
                    command.Parameters.Add(NameBookParam);
                    SqlParameter NameAvtorParam = new SqlParameter("Name_avtor", NameAvtor);
                    command.Parameters.Add(NameAvtorParam);
                    SqlParameter SurnameAvtorParam = new SqlParameter("@Surname_avtor", SurnameAvtor);
                    command.Parameters.Add(SurnameAvtorParam);
                    SqlParameter LanguageBookParam = new SqlParameter("@Language_book", LanguageBook);
                    command.Parameters.Add(LanguageBookParam);
                    SqlParameter PriceParam = new SqlParameter("@Price", PriceBook);
                    command.Parameters.Add(PriceParam);
                    SqlParameter Data_exitParam = new SqlParameter("@Data_exit", ReleaseDataBook);
                    command.Parameters.Add(Data_exitParam);
                    SqlParameter CoverTypeParam = new SqlParameter("@Cover_type", CoverTypeBook);
                    command.Parameters.Add(CoverTypeParam);
                    command.ExecuteNonQuery();
                    reader.Close();
                    MessageBox.Show("Товар Отредактирован");
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
