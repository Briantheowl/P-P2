using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System.Data;
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
        //List to be populated with database info
        List<Restaurant> restaurantList = new List<Restaurant>();


        //Declaring connection class
        DBConn _connected;



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
            Console.Clear();

            Console.WriteLine("Start\n");

            //instantiating Connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query("SELECT * FROM RestaurantProfiles");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                //tryparcing numerical values to be able to convert to json format
                decimal.TryParse(TempTable.Rows[i]["Price"].ToString(), out decimal price);
                double.TryParse(TempTable.Rows[i]["FoodRating"].ToString(), out double foodrating);
                double.TryParse(TempTable.Rows[i]["ServiceRating"].ToString(), out double servicerating);
                double.TryParse(TempTable.Rows[i]["AmbienceRating"].ToString(), out double ambiencerating);
                double.TryParse(TempTable.Rows[i]["ValueRating"].ToString(), out double valuerating);
                double.TryParse(TempTable.Rows[i]["OverallRating"].ToString(), out double overallrating);
                double.TryParse(TempTable.Rows[i]["OverallPossibleRating"].ToString(), out double overallpossiblerating);

                //Creating Restaurant object structure to store all database entries
                Restaurant r = new Restaurant(TempTable.Rows[i]["id"].ToString(), TempTable.Rows[i]["RestaurantName"].ToString(), TempTable.Rows[i]["Address"].ToString(),
                    TempTable.Rows[i]["Phone"].ToString(), TempTable.Rows[i]["HoursOfOperation"].ToString(), price, TempTable.Rows[i]["USACityLocation"].ToString(),
                    TempTable.Rows[i]["Cuisine"].ToString(), foodrating, servicerating, ambiencerating, valuerating, overallrating, overallpossiblerating);

                //Adding all restaurants created to the restaurant list
                restaurantList.Add(r);
            }



            using (StreamWriter sw = new StreamWriter(_directory + "info.json"))
            {
                //open Bracket for entire JSON file  
                sw.WriteLine("{");
                //open square bracket for table 
                sw.WriteLine("\"ResaurantProfiles\" " + ":[");
                //open bracket for each row
                sw.WriteLine("{");


                for (int i = 0; i < restaurantList.Count; i++)
                {
                    sw.Write($"\"id\": \"{restaurantList[i].Id}\",");
                    sw.Write($"\"Restaurant\": \"{restaurantList[i].RestaurantName}\",");
                    sw.Write($"\"Address\": \"{restaurantList[i].Address}\",");
                    sw.Write($"\"Phone\": \"{restaurantList[i].Phone}\",");
                    sw.Write($"\"HoursOfOpereation\": \"{restaurantList[i].HoursOfOperation}\",");
                    sw.Write($"\"Price\": \"{restaurantList[i].Price}\",");
                    sw.Write($"\"USACityLocation\": \"{restaurantList[i].USACityLocation}\",");
                    sw.Write($"\"Cuisine\": \"{restaurantList[i].Cuisine}\",");
                    sw.Write($"\"FoodRating\": \"{restaurantList[i].FoodRating}\",");
                    sw.Write($"\"ServiceRating\": \"{restaurantList[i].ServiceRating}\",");
                    sw.Write($"\"AmbienceRating\": \"{restaurantList[i].AmbienceRating}\",");
                    sw.Write($"\"ValueRating\": \"{restaurantList[i].ValueRating}\",");
                    sw.Write($"\"OverallRating\": \"{restaurantList[i].OverallRating}\",");
                    sw.Write($"\"OverallPossibleRating\": \"{restaurantList[i].OverallPossibleRating}\"");

                    //check for whether last brace in data set will have a comma or not
                    if (i == restaurantList.Count - 1)
                    {
                        sw.WriteLine("}");
                    }
                    else
                    {
                        sw.WriteLine("},");
                    }

                    if (i != restaurantList.Count - 1)
                    {
                        sw.WriteLine("{");
                    }
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
                    Alphabetical();
                    break;

                case 2:
                    //Running method for reverse alphabetical order
                    ReverseAlphabetical();
                    break;

                case 3:
                    //Running method for best to worst based on star rating
                    BestToWorst();
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
                    //ShowBest();
                    break;

                case 2:
                    //show 4 and above 
                    //ShowFourAndUp();
                    break;

                case 3:
                    //show 3 and above
                    //ShowThreeAndUp();
                    break;

                case 4:
                    //show the worst(1 star)
                    //ShowWorst();
                    break;

                case 5:
                    //show unrated(not rated)
                    //ShowUnrated();
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

        private void Alphabetical()
        {
            Console.Clear();

            Console.WriteLine("Start\n");

            //instantiating Connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query(" SELECT RestaurantName,OverallRating FROM RestaurantProfiles " +
                " ORDER BY RestaurantName ASC ");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                //Tryparsing the number value to be displayed as a string in console
                double.TryParse(TempTable.Rows[i]["OverallRating"].ToString(), out double overallrating);

                //Creating Restaurant object structure to store all database entries
                Restaurant r = new Restaurant(TempTable.Rows[i]["RestaurantName"].ToString(), Math.Round(overallrating, 0));

                //Adding all restaurants created to the restaurant list
                restaurantList.Add(r);

                //writing out to the console all the restaurants with overall rating and star ratings
                //Console.WriteLine($"{restaurantList[i].RestaurantName, -5} {restaurantList[i].OverallRating, -8} {Stars.DrawStars(overallrating), -3}");

                Console.Write($"{restaurantList[i].RestaurantName,-15} {"     "}{restaurantList[i].OverallRating,-12} ");

                Stars.DrawStars(restaurantList[i].OverallRating);

            }

        }

        private void ReverseAlphabetical()
        {
            Console.Clear();

            Console.WriteLine("Start\n");

            //instantiating Connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query("SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                " ORDER BY RestaurantName " +
                "DESC ");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                //Tryparsing the number value to be displayed as a string in console
                double.TryParse(TempTable.Rows[i]["OverallRating"].ToString(), out double overallrating);

                //Creating Restaurant object structure to store all database entries
                Restaurant r = new Restaurant(TempTable.Rows[i]["RestaurantName"].ToString(), Math.Round(overallrating, 0));

                //Adding all restaurants created to the restaurant list
                restaurantList.Add(r);

                //writing out to the console all the restaurants with overall rating and star ratings
                //Console.WriteLine($"{restaurantList[i].RestaurantName, -5} {restaurantList[i].OverallRating, -8} {Stars.DrawStars(overallrating), -3}");

                Console.Write($"{restaurantList[i].RestaurantName,-15} {"     "}{restaurantList[i].OverallRating,-12} ");

                Stars.DrawStars(restaurantList[i].OverallRating);

            }
        }

        private void BestToWorst()
        {
            Console.Clear();

            Console.WriteLine("Start\n");

            //instantiating Connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query("SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                " ORDER BY OverallRating DESC ");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                //Tryparsing the number value to be displayed as a string in console
                double.TryParse(TempTable.Rows[i]["OverallRating"].ToString(), out double overallrating);

                //Creating Restaurant object structure to store all database entries
                Restaurant r = new Restaurant(TempTable.Rows[i]["RestaurantName"].ToString(), Math.Round(overallrating, 0));

                //Adding all restaurants created to the restaurant list
                restaurantList.Add(r);

                //writing out to the console all the restaurants with overall rating and star ratings
                //Console.WriteLine($"{restaurantList[i].RestaurantName, -5} {restaurantList[i].OverallRating, -8} {Stars.DrawStars(overallrating), -3}");

                Console.Write($"{restaurantList[i].RestaurantName,-15} {"     "}{restaurantList[i].OverallRating,-12} ");

                Stars.DrawStars(restaurantList[i].OverallRating);
            }


        }

        private void WorstToBest()
        {
            Console.Clear();

            Console.WriteLine("Start\n");

            //instantiating Connection class
            _connected = new DBConn();

            //Running select statement through class object fields
            _connected.Query("SELECT RestaurantName,OverallRating FROM RestaurantProfiles" +
                " ORDER BY OverallRating ASC ");

            //Variable used to conatin all information of table
            DataTable TempTable = _connected.QueryEx();

            for (int i = 0; i < TempTable.Rows.Count; i++)
            {
                //Tryparsing the number value to be displayed as a string in console
                double.TryParse(TempTable.Rows[i]["OverallRating"].ToString(), out double overallrating);

                //Creating Restaurant object structure to store all database entries
                Restaurant r = new Restaurant(TempTable.Rows[i]["RestaurantName"].ToString(), Math.Round(overallrating, 0));

                //Adding all restaurants created to the restaurant list
                restaurantList.Add(r);

                //writing out to the console all the restaurants with overall rating and star ratings
                //Console.WriteLine($"{restaurantList[i].RestaurantName, -5} {restaurantList[i].OverallRating, -8} {Stars.DrawStars(overallrating), -3}");

                Console.Write($"{restaurantList[i].RestaurantName,-15} {"     "}{restaurantList[i].OverallRating,-12} ");

                Stars.DrawStars(restaurantList[i].OverallRating);
            }
        }
    }
}







