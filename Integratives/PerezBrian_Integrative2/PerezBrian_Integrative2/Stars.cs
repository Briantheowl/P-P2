using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerezBrian_Integrative2
{
    class Stars
    {

        public static void DrawStars(double overallrating)
        {
            if (overallrating >= 4.50)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*****");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (overallrating >= 3.50 && overallrating < 4.40)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("****");
                Console.ForegroundColor = ConsoleColor.White;
                //Console.Write("*");
            }
            else if (overallrating >= 2.50 && overallrating < 3.40)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***");  
                Console.ForegroundColor = ConsoleColor.White;
                //Console.Write("**");
            }
            else if (overallrating >= 1.50 && overallrating < 2.40)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("**");  
                Console.ForegroundColor = ConsoleColor.White;
                //Console.Write("***");
            }
            else if (overallrating >= 0.50 && overallrating < 1.40)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*");  
                Console.ForegroundColor = ConsoleColor.White;
                //Console.Write("****");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unrated", -15); 
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
       
    }
}
