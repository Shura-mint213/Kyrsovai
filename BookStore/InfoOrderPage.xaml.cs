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

namespace BookStore
{
    public partial class InfoOrderPage : Page
    {
        public class Books
        {
            public byte[] Photos { get; set; }
            public string Name_book { get; set; }
            public string Name_avtor { get; set; }
            public string Surname_avtor { get; set; }
            public string Price { get; set; }
            public string Language { get; set; }
            public string Realese_date { get; set; }
            public string Cover_type { get; set; }
        }

        public byte[] Photo { get; set; }

        public InfoOrderPage(string Name_book)
        {
            InitializeComponent();
            List<Books> Book = new List<Books>();
            string Sqlexpresion= "SELECT * FROM Books where Name_book = @Name_book_value";
            SqlCommand command = new SqlCommand(Sqlexpresion, Manager.connection);
            SqlParameter Name_book_parameter = new SqlParameter("@Name_book_value", Name_book);
            command.Parameters.Add(Name_book_parameter);
            SqlDataReader reader = command.ExecuteReader();
            Books bk = new Books();
            if (reader.Read())
            {
                byte[] stream = reader[1] as byte[];
                if (stream != null)
                {
                    MemoryStream memoryStream = new MemoryStream(stream);
                    try
                    {
                        ImageBook.Source = BitmapFrame.Create(memoryStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                    catch (NotSupportedException)
                    {
                        ImageBook.Source = null;
                    }
                }
                var image = new Books();
                image.Photos = stream;
                Photo = reader[1] as byte[];
                NameAvtor.Text = reader[3].ToString();
                SurnameAvtor.Text = reader[4].ToString();
                Language.Text = reader[5].ToString();
                Price.Text = reader[6].ToString();
                RealeseDate.Text = reader[8].ToString();
                NameBook.Text = reader[2].ToString();
                Covertype.Text = reader[7].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }
    }
}
