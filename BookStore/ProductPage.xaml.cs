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

    public partial class ProductPage : Page
    {
        public class Book
        {
            public byte[] Photos { get; set; }
            public string Name_book { get; set; }
            public string Price { get; set; }
        }

        public string Login_get { get; set; }
        public string Name_book { get; set; }
        public ProductPage(String Login)
        {
            InitializeComponent();
            Book Books = (Book)DataTableProduct.SelectedItem;
            Login_get = Login;
        }
        

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            DataTableProduct.Visibility = Visibility.Visible;
            try
            {       
                string sqlExpression = "SELECT * FROM dbo.Books";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                List<Book> Books = new List<Book>();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book st = new Book();
                        st.Photos = reader[1] as byte[];
                        st.Name_book = reader[2].ToString();
                        st.Price = reader[6].ToString();
                        Books.Add(st);
                    }
                    DataTableProduct.ItemsSource = Books;
                    DataTableProduct.Columns[4].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[5].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[6].Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("stop");
                }
                reader.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Book Books = (Book)DataTableProduct.SelectedItem;
            string Code_book = "";
            string Price = "";
            if (Books == null)
            {
                MessageBox.Show("Выберите товар!");
            }
            else
            {
                string sqlExpression = "SELECT * FROM Books where Name_book = @NameBook";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                command.Parameters.Add(new SqlParameter("@NameBook", Books.Name_book));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Code_book = reader[0].ToString();
                    Price = reader[6].ToString();
                }
                reader.Close();
                string Login = Login_get;
                sqlExpression = "SELECT * FROM Basket WHERE Code_book = @Code_book_value AND Buyers_login = @Login_get_value";
                command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Code_book_param = new SqlParameter("@Code_book_value", Code_book);
                command.Parameters.Add(Code_book_param);
                SqlParameter Login_get_param = new SqlParameter("@Login_get_value", Login);
                command.Parameters.Add(Login_get_param);
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    MessageBox.Show("Такой товар уже есть в корзине");
                }
                else
                {
                    Reader.Close();
                    int Quantity = 1;
                    sqlExpression = "insert into Basket(Code_book, Buyers_login, Quantity, Price ) " +
                        "VALUES(@Code_book_value, @Login_get_value, @Quantity_value, @Price_value)";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    Code_book_param = new SqlParameter("@Code_book_value", Code_book);
                    command.Parameters.Add(Code_book_param);
                    Login_get_param = new SqlParameter("@Login_get_value", Login);
                    command.Parameters.Add(Login_get_param);
                    SqlParameter Quantity_param = new SqlParameter("@Quantity_value", Quantity);
                    command.Parameters.Add(Quantity_param);
                    SqlParameter Price_param = new SqlParameter("@Price_value", Price);
                    command.Parameters.Add(Price_param);
                    command.ExecuteNonQuery();
                }
                Reader.Close();
            }
        }

        private void ButtonBaster_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new BasterPage(Login_get));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new EditBuyerPage(Login_get));
        }
        public string CheckSymbol(string like)
        {
            like = like.Replace("[", "[[]");
            like = like.Replace(";", "[;]");
            like = like.Replace("--", "[--]");
            like = like.Replace("'", "''");
            like = like.Replace("_", "[_]");
            like = like.Replace("%", "[%]");
            return like;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTableProduct.Visibility = Visibility.Visible;
            try
            {
                string like = SearchTextBox.Text.Trim();
                like = CheckSymbol(like);
                string sqlExpression = "SELECT * FROM Books WHERE Name_book Like '%" + like + "%'"  ;
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                List<Book> Books = new List<Book>();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book st = new Book(); 
                        st.Photos = reader[1] as byte[];
                        st.Name_book = reader[2].ToString();
                        st.Price = reader[6].ToString();
                        Books.Add(st);
                    }
                    DataTableProduct.ItemsSource = Books;
                    DataTableProduct.Columns[4].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[5].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[6].Visibility = Visibility.Hidden;
                }
                reader.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show((er.Number).ToString() + " " + er.Message);
            }
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new OrderPage(Login_get));
        }

        private void InfoOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Book Books = (Book)DataTableProduct.SelectedItem;
            if (Books != null)
            {
                Manager.FrameMainWindow.Navigate(new InfoOrderPage(Books.Name_book));
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }
    }   
}
