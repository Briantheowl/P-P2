using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System.Data;

namespace PerezBrian_CustomAppCode
{
    class Assignment
    {
        //variables used to create a new user
        private string firstName;
        private string lastName;
        private string userName;
        private string password;

        //instantiating database connection
        DBConn _connected = new DBConn(); 

        //menu used for user to get into application/create an acount for access
        Menu _myMenu = new Menu("Sign up","Login");
        //Menu user for user to do things in the app itself
        Menu _myMenu2 = new Menu("View all items", "View NPCs", "View Conversion rates", "Exit");
        //variable used to hold all data from database table
        DataTable tempTable = new DataTable();
        

        public Assignment()
        {
            _myMenu.Display();
            Selection();
        }

        private void Selection()
        {
            int selection = Validation.ValidateInt("Welcome to the guidebook," +
                " please either sign up or login to an existing account.");

            switch (selection)
            {

                case 1:
                    SignUp();
                    break;

                case 2:
                    Login();
                    break;


                default:
                    Console.WriteLine("That is no a valid option" +
                        " Press any key to return to the main menu");
                    Console.ReadKey();
                    Console.Clear();
                    _myMenu.Display();
                    Selection();
                    break;
            }
        }

        private void SignUp()
        {
            //Asking user for necessary informtation to create their user
            firstName = Validation.ValidateString("What is your First name?");
            lastName = Validation.ValidateString("What is your Last name?");
            userName = Validation.ValidateString($"What would you like your username to be {firstName}?");
            password = Validation.ValidateString("What about your password?");

            //Adding user info to database to become a known user account
            _connected.Query("INSERT INTO Users(first_name, last_name, user_name, password)" +
                $" VALUES(\"{firstName}\",\"{lastName}\",\"{userName}\",\"{password}\")");

            //executing insert statement
            _connected._cmd.ExecuteNonQuery();

            //closing previous connection
            _connected._conn.Close();

            Console.WriteLine("Account creation CONFIRMED, " +
                "Press any key to return to the main menu and login to your account.");
            Console.ReadKey();

            Console.Clear();

            _myMenu.Display();
            Selection();
        }

        private void Login()
        {

            Console.Clear();

            //user inputed information to login
            userName = Validation.ValidateString("Username:");
            password = Validation.ValidateString("Password:");
            
            _connected.Query("SELECT * FROM Users " +
                $" WHERE user_name = \"{userName}\" AND password = \"{password}\"");

            DataTable tempTable = new DataTable();

            tempTable = _connected.QueryEx();

            if (tempTable.Rows.Count == 1)
            {
                AfterLogin();
            }
            else
            {
                Console.WriteLine("Username/Password is incorrect, press any key to re-enter your data correctly.");
                Console.ReadKey();
                Login();
            }
            //Closing connnection after login 
            _connected._conn.Close();
        }

        private void AfterLogin()
        {
            _myMenu2.Display();
            Selection2();

            ////Variable used to conatin all information of table
            //tempTable = _connected.QueryEx();
            ////setting name of the table to match what data is being displayed
            //tempTable.TableName = "activity_times_05";

            //Console.WriteLine(tempTable.TableName);
            //Console.WriteLine("=================================");

            //for (int i = 0; i < tempTable.Rows.Count; i++)
            //{
            //    Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["time_spent_on_activity"].ToString()}");
            //}
            //Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            //submenuSelec4 = Validation.ValidateInt("choose a time from the listed data...");

            //if (submenuSelec4 > 0 && submenuSelec4 < tempTable.Rows.Count)
            //{

            //    //closing previous connection
            //    _connected._conn.Close();

            //    Console.Clear();

            //    FeedbackAndAdding();
            //}
            //else if (submenuSelec4 == tempTable.Rows.Count + 1)
            //{
            //    DatePerformed();
            //}
            //else
            //{
            //    Console.WriteLine("that is not a valid option," +
            //        " press any key to re-choose a time.");

            //    Console.ReadKey();
            //    TimeSpent();
            //}

        }

        private void Selection2()
        {
            int selection2 = Validation.ValidateInt("Please feel free to navigate through the options...");

            switch (selection2)
            {
                case 1:
                    AllItems();
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
                    Console.WriteLine("That is not a valid option" +
                        "Press any key to return to the menu");
                    _myMenu2.Display();
                    Selection2();
                    break;
            }
        }

        private void AllItems()
        {
            Console.Clear();

            _connected.Query("Select item_id, itemName FROM Items");

            tempTable = _connected.QueryEx();

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["itemName"].ToString()}");
            }

            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            int menuInput1 = Validation.ValidateInt("Choose an item number to read it's description...");

            if (menuInput1 > 0 && menuInput1 < tempTable.Rows.Count)
            {
                Console.Clear();

                _connected.Query("SELECT itemDescription FROM Items" +
                    $" WHERE item_id = \"{menuInput1}\"");

                tempTable = _connected.QueryEx();

                Console.WriteLine(tempTable.Rows[menuInput1]["itemDescription"]);

                Console.WriteLine("Press any key to return to the item list...");
                Console.ReadKey();

                Console.Clear();
                AllItems();
               
            }
            else if (menuInput1 == tempTable.Rows.Count + 1)
            {
                Console.Clear();
                AfterLogin();
            }
            else
            {
                Console.WriteLine("that is not a valid option," +
                    " press any key to re-choose an item.");

                Console.ReadKey();
                Console.Clear();
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}]{tempTable.Rows[i]["itemName"].ToString()}");
                }
                Selection2();
            }
        }
    }
}
