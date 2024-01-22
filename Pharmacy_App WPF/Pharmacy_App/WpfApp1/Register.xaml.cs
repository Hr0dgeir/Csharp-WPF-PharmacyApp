using System;
using System.Collections.Generic;
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
    /// Register.xaml etkileşim mantığı
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-NT9V6AB;Initial Catalog=Pharmacy_App;Integrated Security=True");

        private void btn_show_login_Click(object sender, RoutedEventArgs e)
        {
            register_login frm = new register_login();
            frm.Show();
            this.Close();           
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            if (txt_register_name.Text != "" && txt_register_surname.Text != "" && txt_register_mail.Text != "" && txt_register_password.Text != "" && txt_register_code.Text != "")
            {
                if (txt_register_password.Text.Length <= 4)
                {
                    MessageBox.Show("Your Password to weak");
                }
                else
                {
                    if (txt_register_code.Text == global_code.ToString())
                    {
                        string hashed_password = Sha256convertor.sha256hash_(txt_register_password.Text);
                        string mail_check = txt_register_mail.Text.ToLower();
                        if (mail_check.Contains("@gmail"))
                        {
                            connection.Open();
                            string add_new_worker = "insert into Register (Name,Surname,Mail,Password,Code) values (@Name,@Surname,@Mail,@Password,@Code)";
                            SqlCommand add = new SqlCommand(add_new_worker,connection);
                            add.Parameters.AddWithValue("@Name",txt_register_name.Text);
                            add.Parameters.AddWithValue("@Surname",txt_register_surname.Text);
                            add.Parameters.AddWithValue("@Mail",txt_register_mail.Text);
                            add.Parameters.AddWithValue("@Password",hashed_password);
                            add.Parameters.AddWithValue("@Code",txt_register_code.Text);
                            add.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Your mail must be gmail");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Code incorrect");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all required Boxes");
            }
        }
        int global_code;

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
        public void create_code()
        {
            int code;
            Random rnd = new Random();
            code = rnd.Next(0,999999);
            lbl_show_code.Content = code.ToString();
            global_code = code;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            create_code();
        }
    }    
}
