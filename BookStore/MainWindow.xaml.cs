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
using System.Security.Cryptography;


namespace BookStore
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = @"PC\SQLEXPRESS"; 
                builder.InitialCatalog = "BookStore";
                builder.IntegratedSecurity = true;
                Manager.connection = new SqlConnection(builder.ConnectionString);
            }
            catch (SqlException e)
            {
                MessageBox.Show((e.Number).ToString() + " " + e.Message);
            }
            FrameMainWindow.Navigate(new MainPage());
            Manager.FrameMainWindow = FrameMainWindow;
        }
    }
}
