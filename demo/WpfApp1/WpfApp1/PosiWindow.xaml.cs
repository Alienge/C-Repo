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

namespace WpfApp1
{
    /// <summary>
    /// PosiWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PosiWindow : Window
    {
        private UserInform userinfo;
        SqlConnection conn;
        public PosiWindow(UserInform userinfo, ref SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            this.userinfo = userinfo;
            txt_Name1.Text = userinfo.name;
            txt_id.Text = userinfo.id.ToString();
            

        }
        public PosiWindow(UserInform userinfo, ref SqlConnection conn, int flag)
        {
            InitializeComponent();
            this.conn = conn;
            this.userinfo = userinfo;
            txt_Name1.Text = userinfo.name;
            txt_id.Text = userinfo.id.ToString();
            string sqlselectPosiInfo = "select * from PosiInfo where id = '" + userinfo.id + "'";
            string position = String.Empty;
            decimal salary = 0.0M;
            try
            {
                
                SqlCommand sqlcmd1 = new SqlCommand(sqlselectPosiInfo, conn);
                SqlDataReader reader = sqlcmd1.ExecuteReader();
                //只有唯一标识
                while (reader.Read())
                {
                    position = reader["position"].ToString();
                    salary = Convert.ToDecimal(reader["Salary"]);
                }
                UserPosi userPosi = new UserPosi(userinfo.id, userinfo.name, position, salary);
                txt_position.Text = position;
                txt_salary.Text = salary.ToString();

            }
            catch (Exception ee)
            {
                MessageBox.Show("查询失败");
            }
           
        }
        // 提交职位表，更新职位表即可
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            UserPosi userPosi = new UserPosi(int.Parse(txt_id.Text),
                        txt_Name1.Text,
                        txt_position.Text,
                        Convert.ToDecimal(txt_salary.Text)
                       );
            string sqladdUserInfo = "update PosiInfo set position = '" + userPosi.position + "', Salary = '" + userPosi.salary + "'" + 
                "where id = '"+ userPosi.id+"'";
            try
            {
                SqlCommand sqlcmd1 = new SqlCommand(sqladdUserInfo, conn);
                sqlcmd1.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                MessageBox.Show("更新失败");
            }
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
            this.Close();
        }
    }
}
