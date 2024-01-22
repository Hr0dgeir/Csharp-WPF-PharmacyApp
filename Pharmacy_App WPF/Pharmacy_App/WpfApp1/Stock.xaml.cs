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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Stock.xaml etkileşim mantığı
    /// </summary>
    public partial class Stock : Window
    {
        public Stock()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            combobox_Prescriptions.Items.Add("Reçeteli");
            combobox_Prescriptions.Items.Add("Reçetesiz");
        }

        private void ekle_click(object sender, RoutedEventArgs e)
        {
            if (txt_company.Text != "" && txt_drug_name.Text != "" && txt_are_of_use.Text != "" && txt_expradition_date.Text != "" && combobox_Prescriptions.Text != "")
            {
                connection.Open();
                string add_new_drug = "insert into Add_drug (Drug_name,Company_name,expration_date,area_of_use,prescriptions,Stock) values (@name,@company_name,@date,@area_use,@prescriptions,@stock)";
                SqlCommand add = new SqlCommand(add_new_drug,connection);
                add.Parameters.AddWithValue("@name",txt_drug_name.Text);
                add.Parameters.AddWithValue("@company_name",txt_company.Text);
                add.Parameters.AddWithValue("@date",txt_expradition_date.Text);
                add.Parameters.AddWithValue("@area_use",txt_are_of_use.Text);
                add.Parameters.AddWithValue("@prescriptions",combobox_Prescriptions.Text);
                add.Parameters.AddWithValue("@stock",0);
                add.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Succesfully");
            }
            else
            {
                MessageBox.Show("Please Fill all required areas");
            }
        }

        private void main_window(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Close();
        }
    }
}
