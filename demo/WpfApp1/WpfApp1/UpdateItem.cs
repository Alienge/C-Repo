using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
   
        public class UpdateItem
        {
            public string Version { get; set; }  //版本号
            public string UpdateContent { get; set; }  //更新信息
            public string DownloadUri { get; set; }  //更新包的下载地址
            public string Time { get; set; }  //更新时间
            public string Size { get; set; }  //更新包大小
        }

    
}
