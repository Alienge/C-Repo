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

using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Diagnostics;
//using System.IO.Compression.FileSystem;
using System.IO.Compression;
using System.IO;
using Newtonsoft.Json;
using updatePro1;



namespace updatePro1
{
    /// <summary>
    /// updateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class updateWindow : Window
    {
        private Uri uri = new Uri(@"https://wzc543.oss-cn-beijing.aliyuncs.com/data/update.json");
        private UpdateItem UpdateInfo;
        public updateWindow()
        {
            InitializeComponent();
            LoadingData();
            button7.Visibility = Visibility.Collapsed;
        }

        private async void button6_Click(object sender, RoutedEventArgs e)
        {
            download_name.Text = "正在下载最新版本文件，请耐心等待";
            string dir = System.IO.Directory.GetCurrentDirectory();
            string zipfile = System.IO.Path.Combine(dir, "WpfApp.zip");
            bool success = await Task.Run(() =>
            {
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(UpdateInfo.DownloadUri, zipfile);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            if (success)
            {
                download_name.Text = "文件已下载，正在复制文件";
            }
            else
            {
                download_name.Text = "下载新版本文件失败，请重试";
                return;
            }
            //下载完成后，杀死前面的前进程，调用新进程

            string appname = "WpfApp1";
            Process[] processes = Process.GetProcessesByName(appname);
            foreach (var p in processes)
                p.Kill();


            bool success2 = await Task.Run(() =>
            {
                try
                {
                    string extractPath = System.IO.Path.Combine(dir, "NewVersion");
                    ZipFile.ExtractToDirectory(zipfile, extractPath);

                    foreach (string item in Directory.GetFiles(extractPath))
                    {
                        File.Copy(item, System.IO.Path.Combine(dir, System.IO.Path.GetFileName(item)), true);
                    }
                    File.Delete(zipfile);
                    DirectoryInfo di = new DirectoryInfo(extractPath);
                    di.Delete(true);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            if (success2)
            {
                download_name.Text = "更新完成，您可以启动应用了";
                button7.Visibility = Visibility.Visible;

            }
            else
            {
                download_name.Text = "更新失败，请重试";
            }
        }

        //获取json信息进行确认更新
        private async Task<bool> LoadingData()
        {
            download_name.Text = "正在下载更新文件信息";
            bool success = await Task.Run(() =>
            {
                try
                {
                    WebClient client = new WebClient();
                    byte[] data = client.DownloadData(uri);
                    string test = Encoding.UTF8.GetString(data);
                    UpdateInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateItem>(Encoding.UTF8.GetString(data));
                    //UpdateInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateItem>(GetString(data));
                    // MessageBox.Show(UpdateInfo.DownloadUri);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            if (success)
                download_name.Text = "已获取最新版本信息，可以进行更新";
            else
            {
                download_name.Text = "无法获取更新信息";
            }
            return success;

        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "WpfApp1.exe"));   //A程序完整路径
            string appname = "updatePro1";
            Process[] processes = Process.GetProcessesByName(appname);
            foreach (var p in processes)
                p.Kill();
        }

    }
}

