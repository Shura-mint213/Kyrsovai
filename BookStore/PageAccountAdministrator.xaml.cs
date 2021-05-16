using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace BookStore
{
    public partial class PageAccountAdministrator : Page
    {
        public class Order
        {
            public string Code_orders { get; set; }
            public string Login_user { get; set; }
            public string Orders_date { get; set; }
            public string Price { get; set; }
        }
        public class Book
        {
            public byte[] Photos { get; set; }
            public string Name_book { get; set; }
            public string Price { get; set; }
        }
        public class Admin
        {
            public string Login { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Passport { get; set; }
        }
        public string Login { get; set; }
        public void Order_product()
        {
            try
            {
                string sqlExpression = "SELECT Code_order, Buyers_login, Date_order, Total_summ " +
                    "FROM dbo.Orders ";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                List<Order> Orders = new List<Order>();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order st = new Order();
                        st.Code_orders = reader[0].ToString();
                        st.Login_user = reader[1].ToString();
                        st.Orders_date = reader[2].ToString().Substring(0, reader[2].ToString().Length - 8);
                        st.Price = reader[3].ToString();
                        Orders.Add(st);
                    }
                    OrderDataGrid.ItemsSource = Orders;
                    OrderDataGrid.Columns[5].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[6].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[7].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[8].Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Нет заказов");
                }
                reader.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show((er.Number).ToString() + " " + er.Message);
            }
        }
        public void Admin_table()
        {
            try
            {
                string sqlExpression = "SELECT dbo.Employees.* FROM dbo.Employees WHERE(Employees_login <> @Login)";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Login_parameter = new SqlParameter("@Login", Login);
                command.Parameters.Add(Login_parameter);
                List<Admin> Admins = new List<Admin>();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Admin ad = new Admin();
                        ad.Login = reader[0].ToString();
                        ad.Surname = reader[1].ToString();
                        ad.Name = reader[2].ToString();
                        ad.Email = reader[3].ToString();
                        ad.Passport = reader[4].ToString() + " " + reader[5].ToString();
                        Admins.Add(ad);
                    }
                    DataTableAdmin.ItemsSource = Admins;
                    DataTableAdmin.Columns[6].Visibility = Visibility.Hidden;
                    DataTableAdmin.Columns[7].Visibility = Visibility.Hidden;
                    DataTableAdmin.Columns[8].Visibility = Visibility.Hidden;
                    DataTableAdmin.Columns[9].Visibility = Visibility.Hidden;
                    DataTableAdmin.Columns[10].Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Администраторов нет");
                    DataTableProduct.Visibility = Visibility.Visible;
                }
                reader.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }
        public PageAccountAdministrator(string Login_get)
        {
            InitializeComponent();
            Login = Login_get;
        }

        private void AccountAdministratorExit_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }


        private void RegistrationAdmin_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new PageRegistrationAdmin());
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            OrderDataGrid.Visibility = Visibility.Hidden;
            DataTableProduct.Visibility = Visibility.Hidden;
            DataTableProduct.Visibility = Visibility.Visible;
            DataTableAdmin.Visibility = Visibility.Hidden;
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
                    DataTableProduct.Columns[5].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[6].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[7].Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Книг нет");
                    DataTableProduct.Visibility = Visibility.Hidden;
                }
                reader.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            DataTableProduct.Visibility = Visibility.Hidden;
            OrderDataGrid.Visibility = Visibility.Hidden;
            DataTableAdmin.Visibility = Visibility.Visible;
            Admin_table();
        }
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            Book books = (Book)DataTableProduct.SelectedItem;
            string Code_book = "";
            if (books != null)
            {
                string sqlExpression = "SELECT Code_book, Name_book, Price " +
                "FROM dbo.Books " +
                "WHERE Name_book = @Name_book_value and Price = " + books.Price.Replace(",", ".");
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Name_book_param = new SqlParameter("@Name_book_value", books.Name_book);
                command.Parameters.Add(Name_book_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Code_book = reader[0].ToString();
                        books.Name_book = reader.GetString(1);
                    }
                }
                reader.Close();
                sqlExpression = "DELETE from dbo.Books WHERE Code_book = @Code_book_value";
                command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter CodeBookParameter = new SqlParameter("@Code_book_value", Code_book);
                command.Parameters.Add(CodeBookParameter);
                command.ExecuteNonQuery();
                Admin_table();
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
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
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Admin Admins = (Admin)DataTableAdmin.SelectedItem;
            if (Admins != null)
            {
                string sqlExpretion = "DELETE from dbo.Employees WHERE Employees_login = @Login";
                SqlCommand command = new SqlCommand(sqlExpretion, Manager.connection);
                SqlParameter Login_parameter = new SqlParameter("@Login", Admins.Login);
                command.Parameters.Add(Login_parameter);
                command.ExecuteNonQuery();
                Admin_table();
            }
            else
            {
                MessageBox.Show("Выберите Администратора");
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.Navigate(new AddProductPage(Login));
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
            OrderDataGrid.Visibility = Visibility.Hidden;
            DataTableAdmin.Visibility = Visibility.Hidden;
            DataTableProduct.Visibility = Visibility.Visible;
            try
            {
                string like = SearchTextBox.Text.Trim();
                like = CheckSymbol(like);
                string sqlExpression = "SELECT * FROM Books WHERE Name_book Like '%" + like + "%'";
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
                    DataTableProduct.Columns[5].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[6].Visibility = Visibility.Hidden;
                    DataTableProduct.Columns[7].Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Книги такой нет");
                    DataTableProduct.Visibility = Visibility.Hidden;
                }
                reader.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show((er.Number).ToString() + " " + er.Message);
            }
        }
        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            Book Books = (Book)DataTableProduct.SelectedItem;
            string Code_book = "";
            if (Books != null)
            {
                string sqlExpression = "SELECT Code_book, Name_book, Price " +
                "FROM dbo.Books " +
                "WHERE Name_book = @Name_book_value and Price = " + Books.Price.Replace(",", ".");
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Name_book_param = new SqlParameter("@Name_book_value", Books.Name_book);
                command.Parameters.Add(Name_book_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Code_book = reader[0].ToString();
                    }
                }
                reader.Close();
                Manager.FrameMainWindow.Navigate(new EditProductPage(Login, Code_book));
            }
        }

        private void OrderProduct_Click(object sender, RoutedEventArgs e)
        {
            OrderDataGrid.Visibility = Visibility.Visible;
            DataTableAdmin.Visibility = Visibility.Hidden;
            DataTableProduct.Visibility = Visibility.Hidden;
            Order_product();
        }
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = (Order)OrderDataGrid.SelectedItem;
            try
            {
                string SqlExpresion = "Delete from Orders where Code_order = @Code_orders_vulue";
                SqlCommand command = new SqlCommand(SqlExpresion, Manager.connection);
                SqlParameter Code_orders_parameter = new SqlParameter("@Code_orders_vulue", order.Code_orders);
                command.Parameters.Add(Code_orders_parameter);
                command.ExecuteNonQuery();
                MessageBox.Show("Заказ получен");
                Order_product();
            }
            catch (SqlException Error)
            {
                MessageBox.Show((Error.Number).ToString() + " " + Error.Message);
            }
        }
    }
}
