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
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        SqlConnection conn;
        public AddWindow(ref SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
                if (txt_Name.Text == String.Empty || (gender1.IsChecked == false && gender2.IsChecked == false) || (txt_specialty.Text == String.Empty))
                {
                    
                    MessageBox.Show("请检查姓名，性别，专业是否填写完整！");
                    
                    AddWindow addWindow = new AddWindow(ref conn);
                    



                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txt_phone.Text, @"^[1]+[3,4,5,7,8]+\d{9}"))
                {
                    MessageBox.Show("请检查电话号码是否填写完整！");
                    AddWindow addWindow = new AddWindow(ref conn);
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txt_mail.Text, @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?"))
                {
                    MessageBox.Show("请检查邮箱是否填写完整！");

                    AddWindow addWindow = new AddWindow(ref conn);

                }
                else
                {
                    Random ra = new Random();

                    UserInform initUserInfo = new UserInform(ra.Next(100000, 500000),
                        txt_Name.Text,
                        gender1.IsChecked == true ? "男" : "女",
                        txt_specialty.Text,
                        brith.Text,
                        txt_mail.Text,
                        txt_phone.Text);

                    //========================更新两张数据表（后期改成事务处理）================================================================
                    string sqladdUserInfo = "insert into UserInfo values ('" + initUserInfo.id + "', '" + initUserInfo.name + "', '" + initUserInfo.gender
                        + "', '" + initUserInfo.specialty + "', '" + initUserInfo.brith + "', '" + initUserInfo.mail + "', '" + initUserInfo.phone + "')";
                    string sqladdPosiInfo = "insert into PosiInfo(id,nm) values ('" + initUserInfo.id + "', '" + initUserInfo.name + "')";
                    try
                    {
                        SqlCommand sqlcmd1 = new SqlCommand(sqladdUserInfo, conn);
                        sqlcmd1.ExecuteNonQuery();
                        SqlCommand sqlcmd2 = new SqlCommand(sqladdPosiInfo, conn);
                        sqlcmd2.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("更新失败");
                    }
                    HomeWindow homeWindow = new HomeWindow(ref conn);
                    homeWindow.Show();
                    homeWindow.ShowActivated = true;
                    this.Close();

                }




        }
        private void DteBirthday_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
