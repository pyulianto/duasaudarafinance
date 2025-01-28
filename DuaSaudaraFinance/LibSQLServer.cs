using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DuaSaudaraFinance
{
    class LibSQLServer
    {
        public static void CreateSqlConnection(ref SqlConnection internalConn, string internalString)
        {
            try
            {
                internalConn.ConnectionString = internalString;
                internalConn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CloseSqlConnection(ref SqlConnection internalConn)
        {
            try
            {
                internalConn.Close();
                internalConn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ReadSqlData(ref SqlConnection internalConn, ref SqlDataReader myReader, string sqlSelect)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(sqlSelect, internalConn);
                myCommand.CommandTimeout = 0;
                myReader = myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CloseReadSqlData(ref SqlDataReader myReader)
        {
            try
            {
                myReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ExecuteSqlCommand(ref SqlConnection internalConn, string sqlSelect)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(sqlSelect, internalConn);
                myCommand.CommandTimeout = 0;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static int ExecuteSqlCommandReturnId(ref SqlConnection internalConn, string sqlInsert)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(sqlInsert + "; SELECT SCOPE_IDENTITY();", internalConn);
                myCommand.CommandTimeout = 0;

                // Execute the command and retrieve the newly assigned ID
                object result = myCommand.ExecuteScalar();

                // Check if result is not null and convert it to int
                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    return newId;
                }
                else
                {
                    throw new Exception("Failed to retrieve the newly assigned ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1; // Return a sentinel value indicating failure
            }
        }



    }
}
