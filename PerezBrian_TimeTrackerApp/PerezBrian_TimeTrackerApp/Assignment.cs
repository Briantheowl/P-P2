using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Sql;

namespace PerezBrian_TimeTrackerApp
{
    class Assignment
    {
        //Instantiating connection class
        DBConn _connected = new DBConn();

        //variables to hold the user's name and password
        string _userName;
        string _password;

        public Assignment()
        {
            Console.WriteLine("Press any key to access login to your Time Tracker App");
            Console.ReadKey();
            
            _userName = Validation.ValidateString("What is your First name?");

            _password = Validation.ValidateString("What is your password?");
            
            //Running select statement through class object fields
            _connected.Query(" SELECT user_firstname, user_password FROM time_tracker_users_05 " +
            $" WHERE user_firstname = {_userName} AND user_password = {_password} ");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            if (TempTable.Rows.Count != 0)
            {
                ItAllBeginsHere();
            }
            else
            {
                Console.WriteLine("Invalid login info, press any key to try again...");
                Console.ReadKey();
            }
             

        }

        public void ItAllBeginsHere()
        {
            Console.WriteLine($"Hello {_userName}" + 
                " What woud you like to do today?");
        }

    }
}
