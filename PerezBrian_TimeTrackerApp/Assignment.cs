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

        //After logging in, ask user what activity they want to do today
        //submenus input stored to variables
        //open connection
        //insert stored data in variables into the database via INSERT statement
        //close connection
        //DONE
        
        //variables used to store all user inputs 
        int submenuSelec1;
        int submenuSelec2;
        int submenuSelec3;
        int submenuSelec4;
        
        Menu _myMenu = new Menu("Enter Activity","View Tracked Data","Run Calculations","Exit");


        //Instantiating connection class for use throughout project
        DBConn _connected = new DBConn();

        //variables to hold the user's name and password
        //string _userName;
        //string _password;

        //declaring DataTable class for use through out project
        DataTable tempTable;

        

        public Assignment()
        {
            Console.WriteLine("Press any key to access login to your Time Tracker App");
            Console.ReadKey();

            //_userName = Validation.ValidateString("What is your First name?");

            //_password = Validation.ValidateString("What is your password?");

            ////Running select statement through class object fields
            //_connected.Query(" SELECT user_firstname, user_password FROM time_tracker_users_05 " +
            //$" WHERE user_firstname = {_userName} AND user_password = {_password} ");

            ////Variable used to conatin all information of table
            //tempTable = _connected.QueryEx();

            ////conditional checking if the login information exists in the database
            //if (tempTable.Rows.Contains(_password) && tempTable.Rows.Contains(_userName))
            //{
            //    ItAllBeginsHere();
            //}
            //else
            //{
            //    Console.WriteLine("Invalid login info, press any key to try again...");
            //    Console.ReadKey();
            //}

            ItAllBeginsHere();
        }

        //method containing the activation of entire project
        public void ItAllBeginsHere()
        {
            Console.Clear();

            _myMenu.Title = "Time Tracker App";
            _myMenu.Display();
            Selection();
        }

        private void Selection()
        {
            int selection = Validation.ValidateInt($"Hello Brian," +
                " What would you like to do today?");

            switch (selection)
            {
                case 1:
                    ActivityCategories();
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    //exiting the program
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("That is not a valid menu option," +
                        " Press any key to return to the main menu and try again.");

                    Console.ReadKey();

                    Console.Clear();

                    _myMenu.Display();
                    Selection();
                    break;
            }
        }

        private void ActivityCategories()
        {
            Console.Clear();

            //formatting for neatness
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Activity Categories");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================");

            _connected.Query("SELECT * FROM activity_categories_05");

            //Variable used to conatin all information of table
            tempTable = _connected.QueryEx();

            //setting name of the table to correct one
            tempTable.TableName = "activity_categories_05";

            Console.WriteLine(tempTable.TableName);
            Console.WriteLine("=================================");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["category_description"].ToString()}");
            }
            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");
           

            submenuSelec1 = Validation.ValidateInt("choose an activity from the listed data...");

            if (submenuSelec1 > 0  && submenuSelec1 < tempTable.Rows.Count)
            {
                ActivityDescriptions();
            }
            else if (submenuSelec1 == tempTable.Rows.Count + 1)
            {
                Console.Clear();
                ItAllBeginsHere();
            }
            else
            {
                Console.WriteLine("that is not a valid option," +
                    " press any key to re-choose an activity.");

                Console.ReadKey();

                //submenu 2 method
                ActivityCategories();
            }

        }

        private void ActivityDescriptions()
        {
            //closing previous cononection
            _connected._conn.Close();

            Console.Clear();

            //formatting for neatness
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Activity Descriptions");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================");

            _connected.Query("SELECT * FROM activity_descriptions_05");

            //Variable used to conatin all information of table
            tempTable = _connected.QueryEx();
            //setting name of the table to match what data is being displayed
            tempTable.TableName = "activity_descriptions_05";

            Console.WriteLine(tempTable.TableName);
            Console.WriteLine("=================================");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["activity_description"].ToString()}");
            }
            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            submenuSelec2 = Validation.ValidateInt("choose an activity from the listed data...");

            if (submenuSelec2 > 0 && submenuSelec2 < tempTable.Rows.Count)
            {

                //closing previous connection
                _connected._conn.Close();

                Console.Clear();

                //submenu 3 method
                DatePerformed();
            }
            else if (submenuSelec2 == tempTable.Rows.Count + 1)
            {
                ActivityCategories();
            }
            else
            {
                Console.WriteLine("that is not a valid option," +
                    " press any key to re-choose an activity.");

                Console.ReadKey();
                ActivityDescriptions();
            }
        }

        private void DatePerformed()
        {
            //closing previous cononection
            _connected._conn.Close();

            Console.Clear();

            //formatting for neatness
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Date Activities Were Performed");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================");

            _connected.Query("SELECT * FROM tracked_calendar_dates_05");

            //Variable used to conatin all information of table
            tempTable = _connected.QueryEx();
            //setting name of the table to match what data is being displayed
            tempTable.TableName = "tracked_calendar_dates_05";

            Console.WriteLine(tempTable.TableName);
            Console.WriteLine("=================================");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["calendar_date"].ToString()}");
            }
            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            submenuSelec3 = Validation.ValidateInt("choose a date from the listed data...");

            if (submenuSelec3 > 0 && submenuSelec3 < tempTable.Rows.Count)
            {

                //closing previous connection
                _connected._conn.Close();

                Console.Clear();

                //submenu 4 method
                TimeSpent();
            }
            else if (submenuSelec3 == tempTable.Rows.Count + 1)
            {
                ActivityDescriptions();
            }
            else
            {
                Console.WriteLine("that is not a valid option," +
                    " press any key to re-choose an activity.");

                Console.ReadKey();
                ActivityDescriptions();
            }
        }

        private void TimeSpent()
        {
            //closing previous cononection
            _connected._conn.Close();

            Console.Clear();

            //formatting for neatness
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Time Spent Doing Activity");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================");

            _connected.Query("SELECT * FROM activity_times_05");

            //Variable used to conatin all information of table
            tempTable = _connected.QueryEx();
            //setting name of the table to match what data is being displayed
            tempTable.TableName = "activity_times_05";

            Console.WriteLine(tempTable.TableName);
            Console.WriteLine("=================================");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["time_spent_on_activity"].ToString()}");
            }
            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            submenuSelec4 = Validation.ValidateInt("choose a time from the listed data...");

            if (submenuSelec4 > 0 && submenuSelec4 < tempTable.Rows.Count)
            {

                //closing previous connection
                _connected._conn.Close();

                Console.Clear();

                FeedbackAndAdding();
            }
            else if (submenuSelec4 == tempTable.Rows.Count + 1)
            {
                DatePerformed();
            }
            else
            {
                Console.WriteLine("that is not a valid option," +
                    " press any key to re-choose a time.");

                Console.ReadKey();
                TimeSpent();
            }
        }

        private void FeedbackAndAdding()
        {
            //closing previous cononection
            _connected._conn.Close();

            //opening connection for last time 
            _connected._conn.Open();

            //select statement used to insert the data gathered into database
            _connected.Query("INSERT INTO activity_log_05 (calendar_date, category_description, activity_description, time_spent_on_activity) " +
                $" VALUES ({submenuSelec3}, {submenuSelec1}, {submenuSelec2}, {submenuSelec4}) ");

            _connected.NonQueryEx();

            _connected._conn.Close();
            
            Console.Clear();
            
            //menu at the end of program for either entering another activity or going to main menu
            Menu myMenu2 = new Menu("Enter another Activity", "Back to main menu");

            //formatting for neatness
            Console.ForegroundColor = ConsoleColor.Cyan;
            myMenu2.Title = "Activity has been added to log";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=================================");
            myMenu2.Display();

            int selection2 = Validation.ValidateInt("What would you like to do next?");

            switch (selection2)
            {
                case 1:
                    ActivityCategories();
                    break;

                case 2:
                    ItAllBeginsHere();
                    break;


                default:
                    Console.WriteLine("That is not a valid menu option," +
                        " Press any key to try again.");

                    Console.ReadKey();

                    Console.Clear();

                    FeedbackAndAdding();
                    break;
            }


        }
    }
}
