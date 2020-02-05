using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerezBrian_Integrative2
{
    class Menu
    {
        public string Title { get; set; }
        private List<string> _items;
        public Menu()
        {
            Title = "Application";
            _items = new List<string>();
        }
        public Menu(params string[] items)
        {
            Title = "Integrative Collection";
            _items = items.ToList();
        }
        public void AddMenuItem(string item)
        {
            _items.Add(item);
        }
        public void Display()
        {
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Title);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("==========================================================");
            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {_items[i]}");
            }
        }
    }
}
