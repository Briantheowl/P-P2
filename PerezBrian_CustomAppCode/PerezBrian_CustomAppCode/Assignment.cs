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
        Menu _myMenu2 = new Menu("View all items", "View NPCs", "View Conversion rates", "Logout", "Exit");
        //variable used to hold all data from database table
        DataTable tempTable = new DataTable();
        //list to store users created and to be looped through for validation
        List<Users> userList = new List<Users>();
        

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
                    Console.WriteLine("That is not a valid option" +
                        " Press any key to return to try again");
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

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                int.TryParse(tempTable.Rows[i]["user_id"].ToString(), out int uId);

                //Creating User object structure to store all database entries
                 Users u = new Users(uId, tempTable.Rows[i]["first_name"].ToString(), tempTable.Rows[i]["last_name"].ToString(), tempTable.Rows[i]["user_name"].ToString(), tempTable.Rows[i]["password"].ToString());

                //adding all users to the list
                userList.Add(u);
                
            }

            for (int i = 0; i < userList.Count(); i++)
            {
                

                if (userList[i].User_name == userName)
                {
                    
                    if (userList[i].Password == password)
                    {
                        Console.Clear();
                        AfterLogin();
                    }
                    
                    else
                    {
                        Console.WriteLine("Username/Password is incorrect, press any key to re-enter your data correctly.");
                        Console.ReadKey();
                        Login();
                    }
                    
                }
                else if (userList[i].User_name != userName && userList[i].Password != password)
                {
                    Console.WriteLine("Username and Password are incorrect, press any key to re-enter your data correctly.");
                    Console.ReadKey();
                    Login();
                }
                else
                {
                    Console.WriteLine("Username/Password is incorrect, press any key to re-enter your data correctly.");
                    Console.ReadKey();
                    Login();
                    
                    
                }
            }

            


            //Closing connnection after login 
            _connected._conn.Close();
        }

        private void AfterLogin()
        {
            
            _myMenu2.Display();
            Selection2();
        }

        private void LogOut()
        {
            Console.Clear();

            string outConfirmation = Validation.ValidateString("Are you sure you want to log out?");
            Console.Write("yes" + "   " + "no");
            if (outConfirmation.ToLower() == "yes")
            {
                Login();
            }
            else if (outConfirmation.ToLower() == "no")
            {
                _myMenu2.Display();
                Selection2();
            }
            else
            {
                Console.WriteLine("Please only type yes or no to confirm your choice. /r/n" +
                    "press any key to re-confirm your choice");
                LogOut();
            }
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
                    LogOut();
                    break;

                case 5:
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

            int itemInput = Validation.ValidateInt("Choose an item number to read it's description...");

            if (itemInput > 0 && itemInput < tempTable.Rows.Count)
            {
                Console.Clear();

                _connected.Query("SELECT itemDescription FROM Items" +
                    $" WHERE item_id = \"{itemInput}\"");

                tempTable = _connected.QueryEx();

                Console.WriteLine(tempTable.Rows[itemInput]["itemDescription"]);

                Console.WriteLine("Press any key to return to the item list...");
                Console.ReadKey();

                Console.Clear();
                AllItems();
               
            }
            else if (itemInput == tempTable.Rows.Count + 1)
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

        private void AllNPCs()
        {
            Console.Clear();

            _connected.Query("Select * FROM NPCS");

            tempTable = _connected.QueryEx();

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]{tempTable.Rows[i][""].ToString()}");
            }

            Console.WriteLine($"[{tempTable.Rows.Count + 1}]Back");

            int itemInput = Validation.ValidateInt("Choose an item number to read it's description...");

            if (itemInput > 0 && itemInput < tempTable.Rows.Count)
            {
                Console.Clear();

                _connected.Query("SELECT itemDescription FROM Items" +
                    $" WHERE item_id = \"{itemInput}\"");

                tempTable = _connected.QueryEx();

                Console.WriteLine(tempTable.Rows[itemInput]["itemDescription"]);

                Console.WriteLine("Press any key to return to the item list...");
                Console.ReadKey();

                Console.Clear();
                AllItems();

            }
            else if (itemInput == tempTable.Rows.Count + 1)
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
