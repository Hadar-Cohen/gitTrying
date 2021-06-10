using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using Ex2.Models;


namespace Ex2.Models.DAL
{
    public class SeriesDBServices
    {
      
        public SeriesDBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //--------------------------------------------------------------------------------------------------
        // This method inserts a car to the cars table 
        //--------------------------------------------------------------------------------------------------
        public int Insert(Series series)
        {
            SqlConnection con;
            //SqlCommand cmd;
            int numEffected;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception exe)
            {
               throw (exe);
            }

            //String cStr = BuildInsertCommand(obj);
            //cmd = CreateCommand(cStr, con);

            String seriesQuery = "INSERT INTO Series_2021 (id,first_air_date,name,origin_country,original_language,overview,popularity,poster_path) VALUES (@id,@first_air_date,@name,@origin_country,@original_language,@overview,@popularity,@poster_path)";

            using (SqlCommand command = new SqlCommand(seriesQuery, con))
            {
                command.Parameters.AddWithValue("@id", series.Id);
                command.Parameters.AddWithValue("@first_air_date", series.First_air_date);
                command.Parameters.AddWithValue("@name", series.Name);
                command.Parameters.AddWithValue("@origin_country", series.Origin_country);
                command.Parameters.AddWithValue("@original_language", series.Original_language);
                command.Parameters.AddWithValue("@overview", series.Overview);
                command.Parameters.AddWithValue("@popularity", series.Popularity);
                command.Parameters.AddWithValue("@poster_path", series.Poster_path);

                try
                {
                    numEffected = command.ExecuteNonQuery(); // execute the command
                    return numEffected;
                }
                catch (SqlException exe)
                {
                    if (!(exe.Number == 2627))  //if the row doesnt exists so what is your problem? (throw it)
                        throw (exe); //so throw what was the problem
                    return -1; //the row exists allready
                }
                finally
                {
                    if (con != null)
                    {
                        // close the db connection
                        con.Close();
                    }
                }
            }
        }
  
        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

      }
}