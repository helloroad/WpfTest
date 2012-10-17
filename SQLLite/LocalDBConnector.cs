using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace SQLLite
{

    /**
     *  LocalDBConnector is used to connect SQLite database.
     * 
     * */
    public class LocalDBConnector
    {
        private static readonly LocalDBConnector instance = new LocalDBConnector();
        private SQLiteConnection conn;
        private string CALLISTO_DATASTORE_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CallistoDataStore";
        private string DB_DIR;

        private LocalDBConnector() { }

        public static LocalDBConnector Instance
        {
            get
            {
                return instance;
            }
        }

        /**
         *  Provide a valid connection
         * */
        public SQLiteConnection getConn()
        {
            DB_DIR = "Data Source="+CALLISTO_DATASTORE_DIR + "\\CallistoTVData.ctv;Version=3;Pooling=True;Max Pool Size=1;";
            if(conn==null)
                conn = new SQLiteConnection(DB_DIR);

            if (conn.State == ConnectionState.Closed)
            {

                conn.Open();
            }

            return conn;
        }

        /**
         * Set, Update password (Encrypt database)
         * */
        public void changePassword(string newPassword, string oldPassword = null)
        {
            if (conn == null)
                if(oldPassword==null)
                    conn = new SQLiteConnection(DB_DIR);
                else
                    conn = new SQLiteConnection(DB_DIR+"Password="+oldPassword+";");

            if (conn.State == ConnectionState.Closed)
            {

                conn.Open();
            }

            conn.ChangePassword(newPassword);
        }

    }
}
