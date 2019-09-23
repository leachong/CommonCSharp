using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class Sqlite3Helper
    {

        public string DatabaseFile { get; set; }
        //SQLiteConnection _db;
        public Sqlite3Helper(string file)
        {
            DatabaseFile = file;
        }
        SQLiteConnection OpenDatabase()
        {
            string dbPath = "Data Source =" + DatabaseFile;
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.DefaultTimeout = 10;
            db.Open();
            return db;
        }
        void CloseDatabase(SQLiteConnection db)
        {
            if (db != null)
            {
                if (db.State != System.Data.ConnectionState.Closed)
                {
                    db.Close();
                }
                db.Dispose();
                db = null;
            }
        }

        public bool TableExists(string name)
        {
            var sql = "SELECT COUNT(*) FROM sqlite_master where type ='table' and name ='" + name + "'";
            int tableCount = 0;
            if (ExecuteSql(sql, ref tableCount))
            {
                return tableCount > 0;
            }
            return false;
        }

        public int ExecuteSqlInsert(string sql, ref int resultId)
        {
            SQLiteConnection db = null;
            try
            {
                if ((db = OpenDatabase()).State != System.Data.ConnectionState.Open)
                    return -1;

                using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                {
                    int iret = cmd.ExecuteNonQuery();
                    if (iret > 0)
                    {
                        resultId = (int)db.LastInsertRowId;
                    }
                    return iret;
                }
            }
            finally
            {
                CloseDatabase(db);
            }
        }
        public int ExecuteSql(string sql)
        {
            SQLiteConnection db = null;
            try
            {
                if ((db = OpenDatabase()).State != System.Data.ConnectionState.Open)
                    return -1;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                {
                    int iret = cmd.ExecuteNonQuery(); ;
                    return iret;
                }
            }
            finally
            {
                CloseDatabase(db);
            }
        }
        public bool ExecuteSql(string sql, ref int result)
        {
            var tmpret = result;
            var bret = ExecuteSql(sql, (reader) =>
            {
                if (reader.Read())
                {
                    tmpret = reader.GetInt32(0);
                }
            });
            result = tmpret;
            return bret;
        }
        public bool ExecuteSql(string sql, ref string result)
        {
            var tmpret = result;
            var bret = ExecuteSql(sql, (reader) =>
            {
                if (reader.Read())
                {
                    tmpret = reader.GetString(0);
                }
            });
            result = tmpret;
            return bret;
        }
        public bool ExecuteSql(string sql, Action<SQLiteDataReader> callback)
        {
            SQLiteConnection db = null;
            try
            {
                if ((db = OpenDatabase()).State != System.Data.ConnectionState.Open)
                    return false;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            callback?.Invoke(reader);
                        }
                        return reader != null;
                    }
                }

            }
            finally
            {
                CloseDatabase(db);
            }
        }
    }
}
