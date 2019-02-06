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

namespace loginScreen
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        public loginWindow()
        {
            InitializeComponent();
        }

       private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            //1Create an object for Sql connection with sqlCon name
            //1pass connection string inside that copy from SQL Server Object Explorer
            SqlConnection sqlCon = new SqlConnection(@"Data Source=BIKASH-PC\SQLEXPRESS;Initial Catalog=loginDB;Integrated Security=True");
            //2 add try catch block
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();//2sql function
                //4 declare query 
                String query = "SELECT COUNT(1) FROM tblUser WHERE Username = @Username AND Password =@Password";
                //5 Create an object of SqlCommand with first parameter as query and second sql connection object sqlCon 
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                //6 passing values for username and password
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                //declare variable count and executer this command
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is Incorrect");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);//2exception message passed
            }
            finally
            {
                sqlCon.Close();//3 close sql connection
            }

        }
    }
}
