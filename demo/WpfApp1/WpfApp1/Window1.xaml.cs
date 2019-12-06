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
using System.Data;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    
    public partial class Window1 : Window
    {
        SqlConnection conn;
        public Window1(ref SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }
        // edit infomation
        void Edit_Click(object sender, RoutedEventArgs e)
        {
            /*
            var value = dataGrid.SelectedItem;
            FrameworkElement objElement = dataGrid.Columns[0].GetCellContent(value);
            MessageBox.Show(objElement.Name.ToString());
            */
            DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;
            UserInform userinfo = new UserInform(int.Parse(mySelectedElement.Row[0].ToString()),
                mySelectedElement.Row[1].ToString(),
                mySelectedElement.Row[2].ToString(),
                mySelectedElement.Row[3].ToString(),
                mySelectedElement.Row[4].ToString(),
                mySelectedElement.Row[5].ToString(),
                mySelectedElement.Row[6].ToString());

            //有待改进（查询Posi表，在Posi中默认填写对应的值或者修改）
            //string sqladdPosiInfo = "insert into PosiInfo(id,nm) values ('" + initUserInfo.id + "', '" + initUserInfo.name + "')";

            PosiWindow posiWindow = new PosiWindow(userinfo,ref conn);
            posiWindow.Show();
            posiWindow.ShowActivated = true;
            //this.Close();
            //string result = mySelectedElement.Row[0].ToString();
            //MessageBox.Show(userinfo.id.ToString());

            //string selectItem = ((((DataGrid)sender).SelectedItem) as DataRowView)["ID"].ToString();
            //string currentChoiceT = selectItem.
        }
        // delete information
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;
            UserInform userinfo = new UserInform(int.Parse(mySelectedElement.Row[0].ToString()),
                mySelectedElement.Row[1].ToString(),
                mySelectedElement.Row[2].ToString(),
                mySelectedElement.Row[3].ToString(),
                mySelectedElement.Row[4].ToString(),
                mySelectedElement.Row[5].ToString(),
                mySelectedElement.Row[6].ToString());

            string sqldeletePosiInfo = "delete from PosiInfo where id = '" + userinfo.id + "'";
            string sqldeleteUserInfo = "delete from UserInfo where id = '" + userinfo.id + "'";
            try
            {
                SqlCommand sqlcmd1 = new SqlCommand(sqldeletePosiInfo, conn);
                sqlcmd1.ExecuteNonQuery();
                SqlCommand sqlcmd2 = new SqlCommand(sqldeleteUserInfo, conn);
                sqlcmd2.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                MessageBox.Show("删除失败");
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

        void Select_Click(object sender, RoutedEventArgs e)
        {

            DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;
            UserInform userinfo = new UserInform(int.Parse(mySelectedElement.Row[0].ToString()),
                mySelectedElement.Row[1].ToString(),
                mySelectedElement.Row[2].ToString(),
                mySelectedElement.Row[3].ToString(),
                mySelectedElement.Row[4].ToString(),
                mySelectedElement.Row[5].ToString(),
                mySelectedElement.Row[6].ToString());

            PosiWindow posiWindow = new PosiWindow(userinfo, ref conn,1);
            posiWindow.Show();
            posiWindow.ShowActivated = true;
            //this.Close();

            /*
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
            this.Close();*/


        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
