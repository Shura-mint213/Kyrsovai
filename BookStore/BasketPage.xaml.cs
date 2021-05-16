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
using Image = Xceed.Document.NET.Image;
using Word = Microsoft;
using System.Reflection;
using System.Drawing;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace BookStore
{
    public partial class BasterPage : Page
    {
        //-------------------Book-------------------
        public class Book
        {
            public byte[] Photos { get; set; }
            public string Name_book { get; set; }
            public string Price { get; set; }
            public int Count { get; set; }
            public string Total_price { get; set; }
            public string Code_book { get; set; }

        }
        public string Login_get { get; set; }

        public void Buy_conclusion()
        {
            double TemporaryTotal = 0;
            List<Book> Books = new List<Book>();

            string sqlExpression = "SELECT dbo.Books.Code_book, dbo.Books.Photos, dbo.Books.Name_book, dbo.Books.Price, dbo.Basket.Quantity " +
                "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                "WHERE(dbo.Basket.Buyers_login = @Buyer_login_value)";
            SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
            SqlParameter Buyer_login_param = new SqlParameter("@Buyer_login_value", Login_get);
            command.Parameters.Add(Buyer_login_param);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Book st = new Book();
                    st.Photos = reader[1] as byte[];
                    st.Name_book = reader[2].ToString();
                    st.Price = reader[3].ToString();
                    st.Count = reader.GetInt32(4);
                    st.Total_price += st.Count * double.Parse(st.Price);
                    Books.Add(st);
                }
                DataTableBasket.ItemsSource = Books;
                DataTableBasket.Columns[6].Visibility = Visibility.Hidden;
                DataTableBasket.Columns[7].Visibility = Visibility.Hidden;
                DataTableBasket.Columns[8].Visibility = Visibility.Hidden;
                DataTableBasket.Columns[9].Visibility = Visibility.Hidden;
                DataTableBasket.Columns[10].Visibility = Visibility.Hidden;
                DataTableBasket.Columns[11].Visibility = Visibility.Hidden;
            }
            reader.Close();
        }
        public BasterPage(string Login)
        {
            InitializeComponent();
            Login_get = Login;
        }
        public void Search_in_the_basket(Book Books)
        {
            Book st = new Book();
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTableBasket.Visibility = Visibility.Visible;
                Buy_conclusion();
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            Book Books = (Book)DataTableBasket.SelectedItem;

            Manager.FrameMainWindow.GoBack();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            DataTableBasket.Visibility = Visibility.Visible;
            double Price = 0;
            string Code_book = "";
            try
            {
                string sqlExpression = "SELECT        dbo.Books.Code_book, dbo.Books.Name_book, dbo.Books.Price, dbo.Basket.Quantity " +
                    "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                    "WHERE(dbo.Basket.Buyers_login = 'Buyer_1')"; //получаем код книг
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Buyer_login_param = new SqlParameter("@Buyer_login_value", Login_get);
                command.Parameters.Add(Buyer_login_param);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book st = new Book();
                        st.Name_book = reader[1].ToString();
                        st.Price = reader[2].ToString();
                        Price += reader.GetInt32(3) * double.Parse(st.Price);
                    }
                }
                reader.Close();
                string Code_order = Login_get + DateTime.Now.ToString("yyyyddMMss");
                string Code_Deliverymans = "1";
                string Date_Order = DateTime.Now.ToString("yyyy.MM.dd");

                sqlExpression = " INSERT INTO Orders VALUES (@Code_orders, @Buyers_login, @Data_order, @Code_Deliverymans, @Total_summ)";
                command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Code_order_Param2 = new SqlParameter("@Code_orders", Code_order);
                command.Parameters.Add(Code_order_Param2);
                SqlParameter Login_get_Param2 = new SqlParameter("@Buyers_login", Login_get);
                command.Parameters.Add(Login_get_Param2);
                SqlParameter Date_Order_Param2 = new SqlParameter("@Data_order", Date_Order);
                command.Parameters.Add(Date_Order_Param2);
                SqlParameter Code_Deliverymans_Param2 = new SqlParameter("@Code_Deliverymans", Code_Deliverymans);
                command.Parameters.Add(Code_Deliverymans_Param2);
                SqlParameter Total_summ_Param2 = new SqlParameter("@Total_summ", Price);
                command.Parameters.Add(Total_summ_Param2);
                command.ExecuteNonQuery();
                //--------------------------word cheque-----------------------

                //---------------------------create Order continue-------------------------
                sqlExpression = "SELECT * FROM Basket where Buyers_login = @Login";
                command = new SqlCommand(sqlExpression, Manager.connection);
                command.Parameters.Add(new SqlParameter("@login", Login_get));
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Code_book = reader[0].ToString();
                }
                reader.Close();
                sqlExpression = "SELECT        dbo.Books.Name_book, dbo.Basket.Quantity, dbo.Basket.Code_book, dbo.Basket.Price " +
                    "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                    "WHERE(dbo.Basket.Buyers_login = @Buyer_login_value)"; 
                command = new SqlCommand(sqlExpression, Manager.connection);
                Buyer_login_param = new SqlParameter("@Buyer_login_value", Login_get);
                command.Parameters.Add(Buyer_login_param);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book st = new Book();

                        Code_book = reader[2].ToString();
                        st.Name_book = reader[0].ToString();
                        st.Price = reader[3].ToString();
                        st.Count = reader.GetInt32(1);
                        reader.Close();
                        sqlExpression = " INSERT INTO Detail_orders VALUES (@Code_orders, @Code_book, @Buyers_login, @Quantity, @Price)";
                        command = new SqlCommand(sqlExpression, Manager.connection);
                        SqlParameter Code_order_Param1 = new SqlParameter("@Code_orders", Code_order);
                        command.Parameters.Add(Code_order_Param1);
                        SqlParameter Code_book_Param1 = new SqlParameter("@Code_book", Code_book);
                        command.Parameters.Add(Code_book_Param1);
                        SqlParameter Login_get_Param1 = new SqlParameter("@Buyers_login", Login_get);
                        command.Parameters.Add(Login_get_Param1);
                        SqlParameter Quantity_Param1 = new SqlParameter("@Quantity", st.Count);
                        command.Parameters.Add(Quantity_Param1);
                        SqlParameter Price_Param1 = new SqlParameter("@Price", st.Price);
                        command.Parameters.Add(Price_Param1);
                        command.ExecuteNonQuery();
                        sqlExpression = "Delete from Basket where Code_book = @Code_book";
                        command = new SqlCommand(sqlExpression, Manager.connection);
                        SqlParameter Code_book_delete_Param = new SqlParameter("@Code_book", Code_book);
                        command.Parameters.Add(Code_book_delete_Param);
                        command.ExecuteNonQuery();
                        Buy_conclusion();
                        sqlExpression = "SELECT        dbo.Books.Name_book, dbo.Basket.Quantity, dbo.Basket.Code_book, dbo.Basket.Price " +
                                  "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                                  "WHERE(dbo.Basket.Buyers_login = @Buyer_login_value)"; 
                        command = new SqlCommand(sqlExpression, Manager.connection);
                        Buyer_login_param = new SqlParameter("@Buyer_login_value", Login_get);
                        command.Parameters.Add(Buyer_login_param);
                        reader = command.ExecuteReader();
                    }
                }
                reader.Close();
                DocX document = DocX.Create(Login_get + "_" + Code_order + ".docx"); 
                sqlExpression = "Select * from Buyers Where Buyers_login = @Login";
                command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Login_Param = new SqlParameter("@Login", Login_get);
                command.Parameters.Add(Login_Param);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    double Total_price = 0;
                    string TempoparySurname = reader[1].ToString();
                    string TempoparyName = reader[2].ToString();
                    document.InsertParagraph("Читайка").Font("Times New Roman").FontSize(18).Bold().Color(System.Drawing.Color.Blue).Alignment = Alignment.center;
                    document.InsertParagraph(TempoparySurname + " " + TempoparyName).Font("Times New Roman").FontSize(16);
                    document.InsertParagraph("Ваш заказ: " + Code_order).Font("Times New Roman").FontSize(16).Bold();
                    reader.Close();
                    sqlExpression = "SELECT  dbo.Books.Name_book, dbo.Books.Price FROM dbo.Detail_orders INNER JOIN " +
                        "dbo.Books ON dbo.Detail_orders.Code_book = dbo.Books.Code_book WHERE(dbo.Detail_orders.Buyers_login = @Login) " +
                        "AND (dbo.Detail_orders.Code_order = @Code_order_value)";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    SqlParameter Login_new_Param = new SqlParameter("@Login", Login_get);
                    command.Parameters.Add(Login_new_Param);
                    SqlParameter Code_order_Param = new SqlParameter("@Code_order_value", Code_order);
                    command.Parameters.Add(Code_order_Param);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                        };
                        reader.Close();
                        Xceed.Document.NET.Table table = document.AddTable(i + 1, 4);
                        int row = 1, coloms = 0;
                        sqlExpression = "SELECT  dbo.Books.Name_book, dbo.Books.Price, dbo.Detail_orders.Quantity FROM dbo.Detail_orders INNER JOIN " +
                        "dbo.Books ON dbo.Detail_orders.Code_book = dbo.Books.Code_book WHERE(dbo.Detail_orders.Buyers_login = @Login) " +
                        "AND (dbo.Detail_orders.Code_order = @Code_order_value)";
                        command = new SqlCommand(sqlExpression, Manager.connection);
                        Login_new_Param = new SqlParameter("@Login", Login_get);
                        command.Parameters.Add(Login_new_Param);
                        Code_order_Param = new SqlParameter("@Code_order_value", Code_order);
                        command.Parameters.Add(Code_order_Param);
                        reader = command.ExecuteReader();
                        table.Rows[0].Cells[0].Paragraphs[0].Append("Названия книги").Font("Times New Roman").FontSize(15);
                        table.Rows[0].Cells[1].Paragraphs[0].Append("Цена").Font("Times New Roman").FontSize(15);
                        table.Rows[0].Cells[2].Paragraphs[0].Append("Количество").Font("Times New Roman").FontSize(15);
                        table.Rows[0].Cells[3].Paragraphs[0].Append("Итоговая цена").Font("Times New Roman").FontSize(15);
                        while (reader.Read())
                        {
                            coloms = 0;
                            string name_book = reader[0].ToString();
                            string price = reader[1].ToString();
                            double count = reader.GetInt32(2);
                            table.Rows[row].Cells[coloms].Paragraphs[0].Append(name_book).Font("Times New Roman").FontSize(15);
                            coloms++;
                            table.Rows[row].Cells[coloms].Paragraphs[0].Append(price).Font("Times New Roman").FontSize(15);
                            coloms++;
                            table.Rows[row].Cells[coloms].Paragraphs[0].Append(Convert.ToString(count)).Font("Times New Roman").FontSize(15);
                            coloms++;
                            table.Rows[row].Cells[coloms].Paragraphs[0].Append(Convert.ToString(count * Convert.ToDouble(price))).Font("Times New Roman").FontSize(15);
                            row++;
                            Total_price = Total_price + count * Convert.ToDouble(price);
                        }
                        document.InsertParagraph().InsertTableAfterSelf(table);
                    }
                    Date_Order = DateTime.Now.ToString("dd.MM.yyyy");
                    document.InsertParagraph("Итоговая сумма к оплате: " + Total_price).Font("Times New Roman").FontSize(17).Bold();
                    document.InsertParagraph("Дата оформления заказа " + Date_Order).Font("Times New Roman").FontSize(16).Alignment = Alignment.right;
                    document.InsertParagraph("Швецов Александр ПКС-304 ").Font("Times New Roman").FontSize(16).Alignment = Alignment.right;
                    reader.Close();
                }
                document.Save();

                DataTableBasket.Visibility = Visibility.Hidden;
                MessageBox.Show("Товар Куплен!");
                Manager.FrameMainWindow.Navigate(new ProductPage(Login_get));
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string Code_book = "";
            Book Books = (Book)DataTableBasket.SelectedItem;
            if (Books == null)
            {
                MessageBox.Show("Выберите товар!");
            }
            else
            {
                try
                {
                    string sqlExpression = "SELECT * FROM Books where Name_book = @NameBook";
                    SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                    command.Parameters.Add(new SqlParameter("@NameBook", Books.Name_book));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Code_book = reader[0].ToString();
                    }
                    reader.Close();
                    sqlExpression = "delete FROM Basket where Code_book = @Code_Book";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    SqlParameter Code_book_param = new SqlParameter("@Code_book", Code_book);
                    command.Parameters.Add(Code_book_param);
                    SqlDataReader Reader = command.ExecuteReader();
                    Reader.Close();
                    Buy_conclusion();
                }
                catch (SqlException error)
                {
                    MessageBox.Show((error.Number).ToString() + " " + error.Message);
                }
            }
        }
        private void AddCountButton_Click(object sender, RoutedEventArgs e)
        {
            Book Books = (Book)DataTableBasket.SelectedItem;
            try
            {
                string sqlExpression = "SELECT dbo.Books.Code_book, dbo.Basket.Quantity, dbo.Basket.Price " +
                    "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                    "WHERE(dbo.Books.Name_book = @Name_book)";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                command.Parameters.Add(new SqlParameter("@Name_book", Books.Name_book));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Books.Code_book = reader[0].ToString();
                    Books.Count = reader.GetInt32(1);
                    Books.Price = reader[2].ToString();
                }
                reader.Close();

                sqlExpression = "Update Basket set Quantity = (Quantity+1) WHERE Code_book = @Code_book";
                command = new SqlCommand(sqlExpression, Manager.connection);
                command.Parameters.Add(new SqlParameter("@Code_book", Books.Code_book));
                reader = command.ExecuteReader();
                reader.Close();
                Buy_conclusion();

            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }
        private void SubCountButton_Click(object sender, RoutedEventArgs e)
        {

            Book Books = (Book)DataTableBasket.SelectedItem;
            try
            {
                string sqlExpression = "SELECT dbo.Books.Code_book, dbo.Basket.Quantity, dbo.Basket.Price " +
                    "FROM dbo.Basket INNER JOIN dbo.Books ON dbo.Basket.Code_book = dbo.Books.Code_book " +
                    "WHERE(dbo.Books.Name_book = @Name_book)";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                command.Parameters.Add(new SqlParameter("@Name_book", Books.Name_book));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Books.Code_book = reader[0].ToString();
                    Books.Count = reader.GetInt32(1);
                    Books.Price = reader[2].ToString();
                }
                reader.Close();
                if (Books.Count != 1)
                {
                    sqlExpression = "Update Basket set Quantity = (Quantity-1) WHERE Code_book = @Code_book";
                    command = new SqlCommand(sqlExpression, Manager.connection);
                    command.Parameters.Add(new SqlParameter("@Code_book", Books.Code_book));
                    reader = command.ExecuteReader();
                    reader.Close();
                    Buy_conclusion();
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show((error.Number).ToString() + " " + error.Message);
            }
        }
    }
}
