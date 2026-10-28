using cdn_api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuwanieZal
{
    static class HarDos
    {


        public static string DeleteFrom(string sql, string connection_string)       //Użycie delete i inserta nie jest optymalne, ale w niektórych przypadkach pozycje nie miały atrybutu. Po testach zmienić na update
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    adapter.DeleteCommand = new SqlCommand(sql, connection);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    //MessageBox.Show("Row deleted!! ");
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return sql;
        }

        public static List<int> SelectListRowColumn(string query, string srcTableName, string connection_string)
        {
            //List<string> str = new List<string>();
            List<int> myList = new List<int>();
            string sttr = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connection_string))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(query, connection);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, srcTableName);
                    foreach (DataRow row in (InternalDataCollectionBase)dataSet.Tables[0].Rows)
                    {
                        foreach (DataColumn column in (InternalDataCollectionBase)dataSet.Tables[0].Columns)
                        {
                            myList.Add(Convert.ToInt32(row[column].ToString()));

                            //MessageBox.Show(column + ": " + row[column].ToString());
                        }
                        

                    }
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(sttr = sttr + "Bład SQL:" + ex.ToString() + "\n");
            }
            return myList;
        }
    }
}
