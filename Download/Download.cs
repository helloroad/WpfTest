using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Timers;
using System.Net.NetworkInformation;

namespace Download
{
    class Download
    {

        public delegate void DownloadCompleteEventHandler(object sender, DownloadCompleteEventArgs e);
        public event DownloadCompleteEventHandler DownloadEvent;

        private string url, localDir, fileName;
        private long startPos = 0;
        private FileStream fs;
        private Stream responeStream;
        private HttpWebRequest request;
        private Timer reConnectTimer;

        

        public Download()
        {


        }

        public void restart()
        {
            Console.Out.WriteLine("restart");

            //If file exists, get the pointer of the stream
            if (File.Exists(localDir))
            {
                fs = File.OpenWrite(localDir);
                startPos = fs.Length;
                fs.Seek(startPos, SeekOrigin.Current);
                Console.Out.WriteLine("startPos: " + startPos.ToString());
            }
            else
            {

                fs = new FileStream(localDir, FileMode.Create);
                startPos = 0;

            }

            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                //if there is an existing file, add range parameters to the request 
                if (startPos > 0)
                {
                    request.AddRange((int)startPos);
                }

                responeStream = request.GetResponse().GetResponseStream();

                byte[] nBytes = new byte[512];
                int nReadSize = 0;
                nReadSize = responeStream.Read(nBytes, 0, 512);
                while (nReadSize > 0)
                {
                    fs.Write(nBytes, 0, nReadSize);
                    nReadSize = responeStream.Read(nBytes, 0, 512);
                    Console.Out.WriteLine(nReadSize.ToString());
                }

                fs.Close();
                responeStream.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                responeStream.Close();
                Console.Out.WriteLine("restart Exception");

                if (reConnectTimer == null)
                    reConnectTimer = new Timer();


                reConnectTimer.Interval = 3000;
                reConnectTimer.Elapsed += reConnectTimer_Elapsed;

                reConnectTimer.Start();
            }

        }

        public void start(string url, string localDir, string fileName)
        {
            Console.Out.WriteLine("start download.");
            this.url = url;
            this.localDir = localDir;
            this.fileName = fileName;


            //If file exists, get the pointer of the stream
            if(File.Exists(localDir))
            {
                fs = File.OpenWrite(localDir);
                startPos = fs.Length;
                fs.Seek(startPos,SeekOrigin.Current);

            }else{

                fs = new FileStream(localDir, FileMode.Create);
                startPos = 0;

            }

            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                //if there is an existing file, add range parameters to the request 
                if (startPos > 0)
                {
                    request.AddRange((int)startPos);
                }

                responeStream = request.GetResponse().GetResponseStream();

                byte[] nBytes = new byte[512];
                int nReadSize = 0;
                nReadSize = responeStream.Read(nBytes, 0, 512);
                while (nReadSize > 0)
                {
                    fs.Write(nBytes, 0, nReadSize);
                    nReadSize = responeStream.Read(nBytes, 0, 512);
                    Console.Out.WriteLine(nReadSize.ToString());
                }

                fs.Close();
                responeStream.Close();
                DownloadCompleteEventArgs e = new DownloadCompleteEventArgs(fileName);
                onComplete(e);
            }
            catch (Exception ex)
            {
                fs.Close();
                if(responeStream!=null)
                    responeStream.Close();
                Console.Out.WriteLine("Exception");

                if (reConnectTimer == null)
                    reConnectTimer = new Timer();


                reConnectTimer.Interval = 3000;
                reConnectTimer.Elapsed += reConnectTimer_Elapsed;
                reConnectTimer.Start();
            }
        }


        private void reConnectTimer_Elapsed(object sender, EventArgs e)
        {
            Console.Out.WriteLine("Timer Elapsed");
            reConnectTimer.Stop();
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                this.restart();
            }
            else
            {
                reConnectTimer.Start();
            }
        }

        protected void onComplete(DownloadCompleteEventArgs e)
        {
            DownloadCompleteEventHandler handler = DownloadEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
