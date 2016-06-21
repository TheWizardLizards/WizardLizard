using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Reflection;

namespace WizardLizard.Db
{
    public class Connection : IDisposable
    {
        SQLiteConnection con = new SQLiteConnection("Data source = data.db");

        public DataTable GetData(SQLiteCommand mySqLiteCommand)
        {
            DataSet objDataSet = new DataSet();
            SQLiteConnection dbConnection = new SQLiteConnection("Data source=" + "data.db");
            SQLiteDataAdapter objDataAdapter = new SQLiteDataAdapter();
            mySqLiteCommand.Connection = dbConnection;
            objDataAdapter.SelectCommand = mySqLiteCommand;

            dbConnection.Open();
            objDataAdapter.Fill(objDataSet);
            dbConnection.Close();

            return objDataSet.Tables[0];
        }
        public void OpenCon()
        {
            con.Open();
        }
        public void ModifyData(SQLiteCommand myLiteCommand)
        {
            SQLiteConnection dbConnection = new SQLiteConnection("Data source=" + "data.db");
            myLiteCommand.Connection = dbConnection;
            dbConnection.Open();
            myLiteCommand.ExecuteNonQuery();
            dbConnection.Close();
        }
        public List<T> GetAllRows<T>() where T : TableRow, new()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + typeof(T).Name, con);

            List<T> result = new List<T>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    T item = new T();
                    FillPropertiesFromRow(item, reader);

                    result.Add(item);
                }
            }
            return result;
        }

        public T GetRow<T>(int ID) where T : TableRow, new()
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + typeof(T).Name + " WHERE ID = " + ID, con))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = new T();
                        FillPropertiesFromRow(result, reader);
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<T> FindRowsWhere<T>(string column, object value) where T : TableRow, new()
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + typeof(T).Name + " WHERE " + column + " = @Val;", con))
            {
                cmd.Parameters.AddWithValue("@Val", value);

                List<T> result = new List<T>();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();
                        FillPropertiesFromRow(item, reader);
                        result.Add(item);
                    }
                }
                return result;
            }
        }

        public int InsertRow<T>(T obj) where T : TableRow, new()
        {
            //Get all properties of the assigned type (Except for ID)
            var propertiesWithoutID = typeof(T).GetProperties().Where(a => a.Name != "ID");

            StringBuilder cmdText = new StringBuilder();

            //Create base of INSERT statement
            cmdText.Append("INSERT INTO ").Append(typeof(T).Name);

            if (propertiesWithoutID.Count() > 0)
            {
                //Append list of column names to insert
                cmdText.Append(" (").Append(string.Join(",", propertiesWithoutID.Select(a => a.Name))).Append(")");

                //Append list of values to insert there
                cmdText.Append(" VALUES (").Append(string.Join(",", propertiesWithoutID.Select(a => "@" + a.Name))).Append(");");
            }
            else
            {
                //No values to insert...
                cmdText.Append(" DEFAULT VALUES;");
            }

            using (var cmd = new SQLiteCommand(cmdText.ToString(), con))
            {
                //Insert values as command parameters
                foreach (var p in propertiesWithoutID)
                {
                    object val = p.GetValue(obj, null);
                    if (val == null)
                        val = DBNull.Value;

                    cmd.Parameters.AddWithValue("@" + p.Name, val);
                }
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SQLiteCommand("SELECT last_insert_rowid()", con))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void UpdateRow<T>(T obj) where T : TableRow, new()
        {
            var properties = typeof(T).GetProperties();

            StringBuilder cmdText = new StringBuilder();

            //Create base of INSERT statement
            cmdText.Append("UPDATE ").Append(typeof(T).Name).Append(" SET ");

            var propertiesWithoutID = properties.Where(a => a.Name != "ID");

            //Append list of column names to insert
            cmdText.Append(string.Join(",", propertiesWithoutID.Select(a => a.Name + "=@" + a.Name)));

            //Append list of values to insert there
            cmdText.Append(" WHERE ID = ").Append(obj.ID).Append(";");

            using (var cmd = new SQLiteCommand(cmdText.ToString(), con))
            {
                //Insert values as command parameters
                foreach (var p in propertiesWithoutID)
                {
                    cmd.Parameters.AddWithValue("@" + p.Name, p.GetValue(obj, null));
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertOrUpdateRow<T>(T obj) where T : TableRow, new()
        {
            var alreadyExistingRow = GetRow<T>(obj.ID);

            if (alreadyExistingRow != null)
            {
                UpdateRow(obj);
            }
            else
            {
                InsertRow(obj);
            }
        }

        public void DeleteRow<T>(int id) where T : TableRow, new()
        {
            using (var cmd = new SQLiteCommand("DELETE FROM " + typeof(T).Name + " WHERE ID = " + id, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAllRows<T>() where T : TableRow, new()
        {
            using (var cmd = new SQLiteCommand("DELETE FROM " + typeof(T).Name, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void FillPropertiesFromRow<T>(T target, SQLiteDataReader reader)
        {
            PropertyInfo[] propertiesToFill = typeof(T).GetProperties();
            foreach (PropertyInfo p in propertiesToFill)
            {
                object value = reader[p.Name];

                if (value != DBNull.Value)
                {
                    value = Convert.ChangeType(value, p.PropertyType);
                    p.SetValue(target, value, null);
                }
            }
        }

        public void Dispose()
        {
            con.Dispose();
        }
    }
}
