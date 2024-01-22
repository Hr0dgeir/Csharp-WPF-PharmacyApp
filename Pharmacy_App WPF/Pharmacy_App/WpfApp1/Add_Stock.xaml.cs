using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Add_Stock.xaml etkileşim mantığı
    /// </summary>
    public partial class Add_Stock : Window
    {
        public Add_Stock()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
            MessageBox.Show("To delete, type the id in the line you want to delete, then press the delete data button. " +
                "To add a new stock, write the id number of the product you want to add to the search textbox, " +
                "and then write the stock quantity in the set stock section.");
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("delete from Add_drug where Id = "+txt_delete.Text +" ",connection);
            delete.ExecuteNonQuery();
            MessageBox.Show("Succesfully");
            connection.Close();
            refresh();
        }
        public void refresh()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select ID,Drug_name,Stock from Add_drug", connection);
            DataTable dt = new DataTable();
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            connection.Close();
            Data_grid.ItemsSource = dt.DefaultView;
        }

        public void set_stock()
        {
            connection.Open();
            SqlCommand update = new SqlCommand("update Add_drug set Stock='" + txt_Stock.Text + "' Where ID='" + txt_search.Text + "' ", connection);
            update.ExecuteNonQuery();
            MessageBox.Show("Succesfully Stock Updated");
            connection.Close();
            refresh();
        }

        private void btn_set_Click(object sender, RoutedEventArgs e)
        {
            set_stock();
        }

        private void btn_return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }
    }
}
