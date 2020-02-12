using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PerezBrian_TimeTrackerApp
{
    class Assignment
    {
        //Declaring database connection class
        DBConn _connected;

        //Variable used to tell user what they signed in as
        string _user = ""; 

        public Assignment()
        {
            //instantiating Database connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query("SELECT * FROM time_tracker_users_05");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            //loop to go through rows of database and write it out to console.
            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                TempTable.Rows[i]["user_id"].ToString();
                TempTable.Rows[i]["password"].ToString();
                TempTable.Rows[i]["user_firstname"].ToString();
                TempTable.Rows[i]["user_lastname"].ToString();
            }
            


        }

    }
}
