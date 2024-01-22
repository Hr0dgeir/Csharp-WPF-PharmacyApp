using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");

        private void btn_(object sender, RoutedEventArgs e)
        {
            Stock frm = new Stock();
            frm.Show();
            this.Close();
        }

        private void stok_yenile(object sender, RoutedEventArgs e)
        {
            Add_Stock frm = new Add_Stock();
            frm.Show();
            this.Close();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This application is made for Pharmacy stock management and sales transactions. " +
                "Also, this is the first WPF application I have made. " +
                "You can add drugs from the add new drug section. You can delete the drug or update the stock from stock management, " +
                "use the sell window to make sales.");
        }

        private void btn_log_out_Click(object sender, RoutedEventArgs e)
        {
            register_login frm = new register_login();
            frm.Show();
            this.Close();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            its_myfault();
            refresh_stock();
            refresh_price();
        }

        private void btn_set_price_Click(object sender, RoutedEventArgs e)
        {
            Set_price frm = new Set_price();
            frm.Show();
            this.Close();
        }
        public void refresh_stock()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select ID,Drug_name,Stock from Add_drug", connection);
            DataTable dt = new DataTable();
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            connection.Close();
            Show_stock_main.ItemsSource = dt.DefaultView;
        }
        public void refresh_price()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select ID,Drug_name,Price from Add_drug", connection);
            DataTable dt = new DataTable();
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            connection.Close();
            Show_price_main.ItemsSource = dt.DefaultView;
        }
        public void its_myfault()
        {
            
        }
    }
}
