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
// connect sql client
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        void button1_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSQLServerFunc();
        }
        // connect sqlserver
        public void ConnectionSQLServerFunc()
        {
            //string strConn = "server=.;User Id=sa;Pwd=123456;Database=UserInfoManage";
            string strConn = @"data source=(local);initial catalog=UserInfoManage;integrated security=true";
            SqlConnection conn = new SqlConnection(strConn);
            {
                try
                {
                    conn.Open();
                    //string commandStr = "select * from UserInfo";
                    //SqlCommand sqlCmd = new SqlCommand(commandStr, conn);
                    if (conn.State == ConnectionState.Open)
                    {
                        MessageBox.Show("SQL success");
 
                    }
                    if (conn.State == ConnectionState.Closed)
                    {
                        MessageBox.Show("SQL Server数据库连接关闭！");
                    }
                    
                    HomeWindow homeWindow = new HomeWindow(ref conn);
                    homeWindow.Show();
                    homeWindow.ShowActivated = true; 
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Please Check the SQL connection");
                }



                this.Close();

            }
            //MessageBox.Show("Executing Finished");

        }
    }
   
}

