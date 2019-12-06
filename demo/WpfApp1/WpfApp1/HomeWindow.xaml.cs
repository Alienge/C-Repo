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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Diagnostics;
//using System.IO.Compression.FileSystem;
using System.IO.Compression;
using System.IO;
using Newtonsoft.Json;

namespace WpfApp1
{
   
    /// <summary>
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : Window
    {
        
        public 
            SqlConnection  conn;
        
        public HomeWindow(ref SqlConnection conn1)
        {
            InitializeComponent();
            this.conn = conn1;
           
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(ref conn);
            addWindow.Show();
            addWindow.ShowActivated = true;
            this.Close();
            /*
            try
            {
                conn.Close();
                MessageBox.Show("SQL success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Fail");
            }*/
            /*
            conn.Open();
            string commandStr = "select * from UserInfo";
            SqlCommand sqlCmd = new SqlCommand(commandStr, conn);
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("SQL success");   
            }
            if (conn.State == ConnectionState.Closed)
            {
                MessageBox.Show("SQL Server数据库连接关闭！");
            }
            MessageBox.Show("Executing success");
            SqlDataAdapter sqlDataAda = new SqlDataAdapter(sqlCmd);
            DataSet daSet = new DataSet();

            sqlDataAda.Fill(daSet);
            Window1 window1 = new Window1();


            window1.dataGrid.ItemsSource = daSet.Tables[0].DefaultView;
            window1.Show();
            window1.ShowActivated = true;
            //编辑  
            this.Close();
            */
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {

            string commandStr = "select * from UserInfo";
            SqlCommand sqlCmd = new SqlCommand(commandStr, conn);
            
            SqlDataAdapter sqlDataAda = new SqlDataAdapter(sqlCmd);
            DataSet daSet = new DataSet();

            sqlDataAda.Fill(daSet);
            Window1 window1 = new Window1(ref conn);


            window1.dataGrid.ItemsSource = daSet.Tables[0].DefaultView;
            window1.Show();
            window1.ShowActivated = true;
            //编辑  
            //this.Close();
            
        }

        

    }
}
