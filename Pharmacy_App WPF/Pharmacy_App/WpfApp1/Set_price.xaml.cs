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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Set_price.xaml etkileşim mantığı
    /// </summary>
    public partial class Set_price : Window
    {
        public Set_price()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select ID,Drug_name,Price from Add_drug", connection);
            DataTable dt = new DataTable();
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            connection.Close();
            show_price.ItemsSource = dt.DefaultView;
        }

        public void set_price()
        {
            connection.Open();
            SqlCommand set_price = new SqlCommand("update Add_drug set Price='"+txt_price.Text+"'where ID ='"+txt_id.Text+"' ",connection);
            set_price.ExecuteNonQuery();
            MessageBox.Show("Succesfully");
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            set_price();
            refresh();
        }

        private void btn_show_mainwindows_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Close();
        }
    }
}
