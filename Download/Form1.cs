using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Download
{
    public partial class Form1 : Form
    {
        private Download dl;
        private List<string> files;
        public Form1()
        {
            InitializeComponent();

            files = new List<string>();
            
            files.Add("VCallisto201109121432.jpg");
            
            files.Add("CallistoTVData.ctv");
            files.Add("VCallisto201108251613.jpg");
            files.Add("livetv-8776-5.ts");
            files.Add("test.rar");

            dl = new Download();
            dl.DownloadEvent += new Download.DownloadCompleteEventHandler(OnDownloadComplete);
            if(files.Count>0)
                dl.start("http://www.mycallistotv.com/istore/00000000000/" + files[0], "D:\\Documents\\DownloadTest\\" + files[0], files[0]);
            
        }


        private void OnDownloadComplete(object sender, DownloadCompleteEventArgs e)
        {
            files.RemoveAt(0);
            if(files.Count>0)
                dl.start("http://www.mycallistotv.com/istore/00000000000/" + files[0], "D:\\Documents\\DownloadTest\\" + files[0], files[0]);

        }
    }
}
