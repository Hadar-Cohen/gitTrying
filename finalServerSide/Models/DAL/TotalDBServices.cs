using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Ex2.Models.DAL
{
    public class TotalDBServices
    {

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
        public int Insert(Total obj)
        {
            SqlConnection con;
            SqlCommand cmd;
            int numEffected;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);// write to log
            }

            String cStr = BuildInsertCommand(obj);
            cmd = CreateCommand(cStr, con);
            
            try
            {
                numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException ex)
            {
                // write to log
                if (!(ex.Number == 2627))
                    throw (ex);
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Close();                    // close the db connection

            }
        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Total prefer)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values({0}, {1} , {2})", prefer.UserId, prefer.Episode.EpisodeId, prefer.Episode.SeriesId);
            String prefix = "INSERT INTO Preferences_2021 ([userId], [episodeId], [seriesId])";
            command = prefix + sb.ToString();
            return command;
        }

        //---------------------------------------------------------------------------------
        // Read from the DB into a list all the series names that the user prefered- dataReader
        //---------------------------------------------------------------------------------
        public List<string> GetSeries(int userId)
        {
            SqlConnection con = null;
            List<string> seriesNames = new List<string>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "Select distinct S.name From Preferences_2021 as P inner join User_2021 as U on U.id=P.userId ";
                selectSTR += "inner join Series_2021 as S on P.seriesId= S.id WHERE U.id = " + userId.ToString();
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    string s= (string)dr["name"];
                    seriesNames.Add(s);
                }

                return seriesNames;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
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