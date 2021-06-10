using System;
using System.Collections.Generic;
using System.Data;
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
    public class UserDataServices
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
        public int Insert(User user)
        {        
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
                //checking if the email already exists
                String selectSTR = "SELECT * FROM User_2021 WHERE email = '" + user.Email + "'";
                cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    if (dr.HasRows)
                        return -1;
                    
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            con = connect("DBConnectionString"); // create the connection
            String cStr = BuildInsertCommand(user);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
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
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(User user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}')", user.FirstName, user.LastName, user.Email, user.Password, user.PhoneNum, user.Gender, user.YearOfBirth, user.Genre, user.Address);
            String prefix = "INSERT INTO User_2021 " + "([firstName], [lastName], [email],[password],[phoneNum],[gender],[yearOfBirth],[genre],[address]) ";
            command = prefix + sb.ToString();

            return command;
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

        // TODO : Add a DeleteFlight method
        // TODO : Add a BuildFlightDeleteCommand method


        public User checkLogIn(string email, string pass)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
                //checking if the email already exists
                String selectSTR = "SELECT * FROM User_2021 WHERE email = '" + email + "'AND password= '" + pass +"'";
                cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                User u = null;
                if (dr.Read())
                {
                    u = new User();
                    u.Id = Convert.ToInt32(dr["id"]);
                    u.FirstName = (string)dr["firstName"];
                    u.LastName = (string)dr["lastName"];
                    //u.Email= (string)dr["email"]; we dont need more fields
                    //u.Password = (string)dr["password"];
                    //u.PhoneNum = (string)dr["phoneNum"];
                }
                return u;
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
                    // close the db connection
                    con.Close();
                }
            }
        }
       
    }
}