using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    internal class DatabaseOperations
    {
        // Method to get a new SQL connection
        protected SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = "data source=ABINAYA_ZAHRAH;database=gatePass;integrated security=True"
            };
            return con;
        }

        // Method to execute a query and return a DataSet
        public DataSet getData(string query, List<SqlParameter> parameters = null)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in getData: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ds;
        }



        // Method to execute a query without returning data
        public void setData(string query, string msg = null, List<SqlParameter> parameters = null)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in setData: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to insert a record and return its ID
        public string executeInsertAndGetId(string query, List<SqlParameter> parameters = null)
        {
            string id = null;
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open(); // Open the connection

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand if they exist
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        // Execute the query and read the inserted ID
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve the ID as an integer and convert to string
                                id = reader.GetInt32(0).ToString();
                            }
                            else
                            {
                                // Log an error if no ID was returned
                                MessageBox.Show("Insert operation failed or no ID was returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and show a message box
                Console.WriteLine("Error in executeInsertAndGetId: " + ex);
                MessageBox.Show("Error in executeInsertAndGetId: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }





        // Method to execute a query with parameters and return the number of rows affected
        public int executeNonQuery(string query, List<SqlParameter> parameters = null)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection con = getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in executeNonQuery: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }

        public string InsertAndGetId(string query, List<SqlParameter> parameters = null)
        {
            string id = null;
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open(); // Open the connection

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand if they exist
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        // Execute the query and read the inserted ID
                        id = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and show a message box
                Console.WriteLine("Error in InsertAndGetId: " + ex);
                MessageBox.Show("Error in InsertAndGetId: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }

    }
}
