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

namespace BookStore
{

    public partial class OrderPage : Page
    {
        public class Book
        {
            public string Code_book { get; set; }
            public string Name_book { get; set; }
            public string Surname_avtor { get; set; }
            public string Name_avtor { get; set; }
            public string Language_book { get; set; }
            public string Price { get; set; }
            public string Data_exit { get; set; }
        }
        public class Order
        {
            public string Code_orders { get; set; }
            public string Login_user { get; set; }
            public string Orders_date { get; set; }
            public string Price { get; set; }
        }
        public string Login_get { get; set; }
        public OrderPage(string Login)
        {
            InitializeComponent();
            Login_get = Login;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderDataGrid.Visibility = Visibility.Visible;
            try
            { 
                string sqlExpression = "SELECT Code_order, Buyers_login, Date_order, Total_summ " +
                    "FROM dbo.Orders " +
                    "WHERE(Buyers_login = @Login)";
                SqlCommand command = new SqlCommand(sqlExpression, Manager.connection);
                SqlParameter Login_param = new SqlParameter("@Login", Login_get);
                command.Parameters.Add(Login_param);
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
                    OrderDataGrid.Columns[4].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[5].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[6].Visibility = Visibility.Hidden;
                    OrderDataGrid.Columns[7].Visibility = Visibility.Hidden;
                }
                reader.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show((er.Number).ToString() + " " + er.Message);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.FrameMainWindow.GoBack();
        }
    }
}
