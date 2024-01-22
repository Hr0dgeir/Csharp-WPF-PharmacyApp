using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// register_login.xaml etkileşim mantığı
    /// </summary>
    public partial class register_login : Window
    {
        public register_login()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");

        private void Show_register_Click(object sender, RoutedEventArgs e)
        {
            Register frm = new Register();
            frm.Show();
            this.Close();
        }
        
        private void btn_sign_in_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            string hashed_password = Sha256convertor.sha256hash_(txt_login_password.Text);
            SqlCommand login = new SqlCommand("select * from Register where Mail=@mail and Password=@password",connection);
            login.Parameters.AddWithValue("@mail",txt_mail.Text);
            login.Parameters.AddWithValue("@password",hashed_password);
            login.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(login);

            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Succesfully");
                
                MainWindow frm = new MainWindow();
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Password or Mail incorrect");
            }
            connection.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public class Sha256convertor
        {
            public static string sha256hash_(string rawData)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }
}
