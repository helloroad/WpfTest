using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SQLLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LocalDBConnector ldc = LocalDBConnector.Instance;

            SQLiteConnection conn = ldc.getConn();
        }
    }
}
