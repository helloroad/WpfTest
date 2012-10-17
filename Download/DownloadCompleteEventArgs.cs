using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Download
{
    /** Class contains data for Download Event.
     *  Derives from System.EventArgs
     * */
    class DownloadCompleteEventArgs : EventArgs
    {
        private string fileName;

        //Constructor
        public DownloadCompleteEventArgs(string fileName)
        {
            this.fileName = fileName;

        }

        public string FileName { get { return fileName; } }
    }
}
