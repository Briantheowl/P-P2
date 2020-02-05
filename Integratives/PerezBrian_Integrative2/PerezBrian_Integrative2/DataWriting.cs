using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace PerezBrian_Integrative2
{
    class DataWriting
    {

        //FOR INSTRUCTOR
        //Database Location
        //string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";

        //Output Location
        //string _directory = @"../../output/";﻿﻿


        //Main menu 
        private Menu _myMenu = new Menu("Convert The Restaurant Reviews Database From SQL To JSON", "Showcase Our 5 Star Rating System", "Showcase Our Animated Bar Graph Review System", "Play A Card Game", "Exit");
        //second menu for showcasing the rating option of menu 1
        private Menu _myMenu2 = new Menu("List Restaurants Alphabetically", "List Restaurants in Reverse Alphabetical", "Sort Restaurants From Best/Most Stars to Worst", "Sort Restaurants From Worst/Least Stars to Best", "Show only X and up", "Exit");
        //third menu for showcasing the "show x an up" option of menu 2
        private Menu _myMenu3 = new Menu("Show the best(5 star ratings)", "Show 4 stars and up", "Show 3 stars and up", "Show the worst(1 star ratings)", "Show unrated(not rated)", "Back");
        private string _directory = @"..\..\output\";
        private string _file = @"info.json";

        //dictionary to hold values of id and other values in database
        Dictionary<string, List<string>> DbInfo = new Dictionary<string, List<string>>();
        //dictionary to hold values of names and ratings of resaurants
        Dictionary<string, double> Rratings = new Dictionary<string, double>();


        //Variable to be overwritten during conditionals loop
        string starRating = "";

        //Constructor for class
        public DataWriting()
        {
            Directory.CreateDirectory(_directory);

            if (!File.Exists(_directory + _file))
            {
                File.Create(_directory + _file).Dispose();
            }
            else
            {
                Console.WriteLine("File already exists");
            }

            Console.Clear();

            _myMenu.Display();

            Selection();
        }

        //method for easier implementation of switch statement use 
        private void Selection()
        {
            int selection = Validation.ValidateInt("Hello Admin, What would you like to do today?");

            switch (selection)
            {
                case 1:
                    JsonConversion();
                    break;

                case 2:
                    Selection2();
                    break;

                case 3:

                    break;

                case 4:

                    break;

                case 5:

                    break;

                case 6:
                    _myMenu.Display();
                    Selection();
                    break;

                default:
                    Console.WriteLine("Please only choose an option on the menu...");

                    Console.ReadKey();

                    Console.Clear();

                    _myMenu.Display();

                    Selection();
                    break;
            }
        }

        private void JsonConversion()
        {
            //Console.Clear();

            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server= 10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;

            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;

            DbInfo.Add("id", new List<string>());
            DbInfo.Add("RestaurantName", new List<string>());
            DbInfo.Add("Address", new List<string>());
            DbInfo.Add("Phone", new List<string>());
            DbInfo.Add("HoursOfOperation", new List<string>());
            DbInfo.Add("Price", new List<string>());
            DbInfo.Add("USACityLocation", new List<string>());
            DbInfo.Add("Cuisine", new List<string>());
            DbInfo.Add("FoodRating", new List<string>());
            DbInfo.Add("ServiceRating", new List<string>());
            DbInfo.Add("AmbienceRating", new List<string>());
            DbInfo.Add("ValueRating", new List<string>());
            DbInfo.Add("OverallRating", new List<string>());
            DbInfo.Add("OverallPossibleRating", new List<string>());

            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT * FROM RestaurantProfiles";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();
                Console.WriteLine("Executed Reader");
                while (rdr.Read())
                {
                    DbInfo["id"].Add(rdr["id"].ToString());
                    DbInfo["RestaurantName"].Add(rdr["RestaurantName"].ToString());
                    DbInfo["Address"].Add(rdr["Address"].ToString());
                    DbInfo["Phone"].Add(rdr["Phone"].ToString());
                    DbInfo["HoursOfOperation"].Add(rdr["HoursOfOperation"].ToString());
                    DbInfo["Price"].Add(rdr["Price"].ToString());
                    DbInfo["USACityLocation"].Add(rdr["USACityLocation"].ToString());
                    DbInfo["Cuisine"].Add(rdr["Cuisine"].ToString());
                    DbInfo["FoodRating"].Add(rdr["FoodRating"].ToString());
                    DbInfo["ServiceRating"].Add(rdr["ServiceRating"].ToString());
                    DbInfo["AmbienceRating"].Add(rdr["AmbienceRating"].ToString());
                    DbInfo["ValueRating"].Add(rdr["ValueRating"].ToString());
                    DbInfo["OverallRating"].Add(rdr["OverallRating"].ToString());
                    DbInfo["OverallPossibleRating"].Add(rdr["OverallPossibleRating"].ToString());

                }
                rdr.Close();
                //Console.WriteLine("Finished Populating Dictionary");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }


            using (StreamWriter sw = new StreamWriter(_directory + "info.json"))
            {
                //open Bracket for entire JSON file  
                sw.WriteLine("{");
                //open square bracket for table 
                sw.WriteLine("\"ResaurantProfiles\" " + ":[");
                //open bracket for each row
                sw.WriteLine("{");

                //counter for  list index 
                int counter = 0;

                foreach (string item in DbInfo["id"])
                {
                    //sw.WriteLine($"\"name\": \"{employee.Name}\",");
                    sw.WriteLine($"\"id\": \"{DbInfo["id"][counter].ToString()}\",");
                    sw.WriteLine($"\"RestaurantName\": \"{DbInfo["RestaurantName"][counter].ToString()}\",");
                    sw.WriteLine($"\"Address\": \"{DbInfo["Address"][counter].ToString()}\",");
                    sw.WriteLine($"\"Phone\": \"{DbInfo["Phone"][counter].ToString()}\",");
                    sw.WriteLine($"\"HoursOfOperation\": \"{DbInfo["HoursOfOperation"][counter].ToString()}\",");
                    sw.WriteLine($"\"Price\": \"{DbInfo["Price"][counter].ToString()}\",");
                    sw.WriteLine($"\"USACityLocation\": \"{DbInfo["USACityLocation"][counter].ToString()}\",");
                    sw.WriteLine($"\"Cuisine\": \"{DbInfo["Cuisine"][counter].ToString()}\",");
                    sw.WriteLine($"\"FoodRating\": \"{DbInfo["FoodRating"][counter].ToString()}\",");
                    sw.WriteLine($"\"ServiceRating\": \"{DbInfo["ServiceRating"][counter].ToString()}\",");
                    sw.WriteLine($"\"AmbienceRating\": \"{DbInfo["AmbienceRating"][counter].ToString()}\",");
                    sw.WriteLine($"\"ValueRating\": \"{DbInfo["ValueRating"][counter].ToString()}\",");
                    sw.WriteLine($"\"OverallRating\": \"{DbInfo["OverallRating"][counter].ToString()}\",");
                    sw.WriteLine($"\"OverallPossibleRating\": \"{DbInfo["OverallPossibleRating"][counter].ToString()}\"");

                    //check for whether last brace in data set will have a comma or not
                    if (DbInfo["id"].Count == counter + 1)
                    {
                        sw.WriteLine("}");
                    }
                    else
                    {
                        sw.WriteLine("},");
                    }

                    if (DbInfo["id"].Count == counter + 1)
                    {
                        //sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine("{");
                    }


                    counter++;
                }

                //add the last bracket in data set
                sw.WriteLine("]");
                //close brace for entire JSON file
                sw.WriteLine("}");

                sw.Close();
            }
        }
        //method for easier implementation of switch statement use
        private void Selection2()
        {
            Console.Clear();

            _myMenu2.Display();

            int selection2 = Validation.ValidateInt("Hello Admin, how would you like to sort the data?");

            switch (selection2)
            {
                case 1:
                    //Running method for alphabetical order
                    RestaurantRatings();
                    break;

                case 2:
                    //Running method for reverse alphabetical order
                    ReverseRestaurantRatings();
                    break;

                case 3:
                    //Running method for best to worst based on star rating
                    BestToWorse();
                    break;

                case 4:
                    //Running method for worst to best based on star rating
                    WorstToBest();
                    break;

                case 5:
                    //Running sub-menu method
                    Selection3();
                    break;

                case 6:
                    //exiting the program
                    break;

                default:
                    Console.WriteLine("Please only choose an option on the menu...");

                    Console.ReadKey();

                    //Console.Clear();

                    _myMenu2.Display();

                    Selection2();
                    break;
            }
        }

        private void RestaurantRatings()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;


            string Rname;
            double Orating;



            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " ORDER BY RestaurantName ASC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (Rratings.ContainsKey("id"))
                    {

                    }
                    else
                    {
                        Rname = rdr["RestaurantName"].ToString();
                        double.TryParse(rdr["OverallRating"].ToString(), out Orating);
                        Rratings.Add(Rname, Orating);
                    }
                }

                //Foreach loop used to check for rating value
                //to add the appropriate star value

                foreach (var item in Rratings)
                {
                    //Conditionals checking for OverallRating
                    if (item.Value > 4.60)
                    {
                        starRating = "*****";
                    }
                    else if (item.Value > 3.60)
                    {
                        starRating = "****";
                    }
                    else if (item.Value > 2.60)
                    {
                        starRating = "***";
                    }
                    else if (item.Value > 1.60)
                    {
                        starRating = "**";
                    }
                    else if (item.Value > 0.60)
                    {
                        starRating = "*";
                    }
                    else
                    {
                        starRating = "";
                    }

                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
                //Selection2();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            _myMenu2.Display();
            Selection2();
        }

        private void ReverseRestaurantRatings()
        {


            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;


            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = " SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " ORDER BY RestaurantName DESC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                   
                }

                //Foreach loop used to check for rating value
                //to add the appropriate star value

                foreach (var item in Rratings)
                {
                    //Conditionals checking for OverallRating
                    if (item.Value > 4.60)
                    {
                        starRating = "*****";
                    }
                    else if (item.Value > 3.60)
                    {
                        starRating = "****";
                    }
                    else if (item.Value > 2.60)
                    {
                        starRating = "***";
                    }
                    else if (item.Value > 1.60)
                    {
                        starRating = "**";
                    }
                    else if (item.Value > 0.60)
                    {
                        starRating = "*";
                    }
                    else
                    {
                        starRating = "";
                    }

                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            _myMenu2.Display();
            Selection2();





            foreach (KeyValuePair<string, double> item in Rratings)
            {
                //Writing out all three values to console
                Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
            }

        }

        private void BestToWorse()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server= 10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;
        
            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " ORDER BY OverallRating DESC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    //Foreach loop used to check for rating value
                    //to add the appropriate star value
                    foreach (var item in Rratings)
                    {
                        //Conditionals checking for OverallRating
                        if (item.Value > 4.60)
                        {
                            starRating = "*****";
                        }
                        else if (item.Value > 3.60)
                        {
                            starRating = "****";
                        }
                        else if (item.Value > 2.60)
                        {
                            starRating = "***";
                        }
                        else if (item.Value > 1.60)
                        {
                            starRating = "**";
                        }
                        else if (item.Value > 0.60)
                        {
                            starRating = "*";
                        }
                        else
                        {
                            starRating = "";
                        }

                        //Writing out all three values to console
                        Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                    }
                    rdr.Close();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();
        }

        private void WorstToBest()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " ORDER BY OverallRating ASC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                }

                //Foreach loop used to check for rating value
                //to add the appropriate star value

                foreach (var item in Rratings)
                {
                    //Conditionals checking for OverallRating
                    if (item.Value > 4.60)
                    {
                        starRating = "*****";
                    }
                    else if (item.Value > 3.60)
                    {
                        starRating = "****";
                    }
                    else if (item.Value > 2.60)
                    {
                        starRating = "***";
                    }
                    else if (item.Value > 1.60)
                    {
                        starRating = "**";
                    }
                    else if (item.Value > 0.60)
                    {
                        starRating = "*";
                    }
                    else
                    {
                        starRating = "";
                    }

                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
                //Selection2();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();

        }
        //method for easier implementation of switch statement use
        private void Selection3()
        {
            Console.Clear();

            _myMenu3.Display();

            int selection3 = Validation.ValidateInt("Please choose how you want the data to be shown");

            switch (selection3)
            {
                case 1:
                    //show the best (5 stars)
                    ShowBest();
                    break;

                case 2:
                    //show 4 and above 
                    ShowFourAndUp();
                    break;

                case 3:
                    //show 3 and above
                    ShowThreeAndUp();
                    break;

                case 4:
                    //show the worst(1 star)
                    ShowWorst();
                    break;

                case 5:
                    //show unrated(not rated)
                    ShowUnrated();
                    break;

                case 6:
                    //back to previous menu
                    Console.Clear();
                    _myMenu2.Display();
                    Selection2();
                    break;

                default:

                    Console.WriteLine("Please only choose an option on the menu...");

                    break;
            }
        }

        private void ShowBest()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " WHERE OverallRating = 5.00 ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                }

                //Foreach loop used to check for rating value to add the appropriate star value
                foreach (var item in Rratings)
                {
                    //setting star value to 5 by defualt since we are 
                    //only showing the best 
                    starRating = "*****";

                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();

        }

        private void ShowFourAndUp()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;


            string Rname;
            double Orating;



            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " WHERE OverallRating > 3.60 " +
                    " ORDER BY OverallRating ASC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Conditional for duplicate key entries error
                    if (Rratings.ContainsKey("RestaurantName"))
                    {

                    }
                    else
                    {
                        Rname = rdr["RestaurantName"].ToString();
                        double.TryParse(rdr["OverallRating"].ToString(), out Orating);
                        Rratings.Add(Rname, Orating);
                    }


                }



                foreach (var item in Rratings)
                {
                    //Conditional used to write out star ratings in console
                    if (item.Value > 3.60 || item.Value < 4.40)
                    {
                        starRating = "****";
                    }
                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
                //Selection2();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();
        }

        private void ShowThreeAndUp()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;


            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;


            string Rname;
            double Orating;



            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " WHERE OverallRating > 2.60 " +
                    " ORDER BY OverallRating ASC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Conditional for duplicate key entries error
                    if (!Rratings.ContainsKey("RestaurantName"))
                    {

                    }
                    else
                    {
                        Rname = rdr["RestaurantName"].ToString();
                        double.TryParse(rdr["OverallRating"].ToString(), out Orating);
                        Rratings.Add(Rname, Orating);
                    }
                }
                //Foreach loop used to check for rating value
                //to add the appropriate star value

                foreach (var item in Rratings)
                {
                    //Conditional used to write out star ratings in console
                    if (item.Value > 2.60 || item.Value <= 3.40)
                    {
                        starRating = "***";
                    }

                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
                //Selection2();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();
        }

        private void ShowWorst()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;
            
            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " ORDER BY OverallRating ASC ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                   
                }



                foreach (var item in Rratings)
                {
                    //Conditional used to write out star ratings in console
                    if (item.Value > 0.60 || item.Value < 1.50)
                    {
                        starRating = "*";
                    }
                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
                //Selection2();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();
        }

        private void ShowUnrated()
        {
            Console.WriteLine("Start\n");

            // MySQL Database Connection String
            string cs = @"server=  10.63.41.76;username=brianPerez;password=root;database=SampleRestaurant;port=8889";

            // Declare a MySQL Connection
            MySqlConnection conn = null;
            
            string stm;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            
            try
            {
                // Open a connection to MySQL
                conn = new MySqlConnection(cs);
                conn.Open();

                // Form SQL Statement
                stm = "SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                    " WHERE OverallRating IS NULL ";

                // Prepare SQL Statement
                cmd = new MySqlCommand(stm, conn);

                // Execute SQL statement and place the returned data into rdr
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                   
                }

                //Foreach loop used to check for rating value to add the appropriate star value
                foreach (var item in Rratings)
                {
                    //Writing out all three values to console
                    Console.WriteLine($"{ item.Key,-2} {Math.Round(item.Value, 0)}/5 {starRating,-2}");
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            Console.WriteLine("Press any key to return to the Data Sorting menu...");

            Console.ReadKey();

            Console.Clear();

            _myMenu2.Display();
            Selection2();
        }
    }
}  
    


         