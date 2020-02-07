using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerezBrian_Integrative2
{
    class Restaurant
    {
        string _id;
        string _RestaurantName;
        string _Address;
        string _Phone;
        string _HoursOfOperation;
        decimal _Price;
        string _USACityLocation;
        string _Cuisine;
        double _FoodRating;
        double _ServiceRating;
        double _AmbienceRating;
        double _ValueRating;
        double _OverallRating;
        double _OverallPossibleRating = 5.00;




        public Restaurant(string id, string RestaurantName, string Address, string Phone, string HoursOfOperation, decimal Price, string USACityLocation, string Cuisine, double FoodRating, double ServiceRating, double AmbienceRating, double ValueRating, double OverallRating, double OverallPossibleRating)
        {
            _id = id;
            _RestaurantName = RestaurantName;
            _Address = Address;
            _Phone = Phone;
            _HoursOfOperation = HoursOfOperation;
            _Price = Price;
            _USACityLocation = USACityLocation;
            _Cuisine = Cuisine;
            _FoodRating = FoodRating;
            _ServiceRating = ServiceRating;
            _AmbienceRating = AmbienceRating;
            _ValueRating = ValueRating;
            _OverallRating = OverallRating;
            _OverallPossibleRating = OverallPossibleRating;

        }

        public Restaurant(string RestaurantName, double OverallRating)
        {
            _RestaurantName = RestaurantName;
            _OverallRating = OverallRating;
        }

        public string Id { get => _id; }
        public string RestaurantName { get => _RestaurantName; }
        public string Address { get => _Address; }
        public string Phone { get => _Phone;}
        public string HoursOfOperation { get => _HoursOfOperation; }
        public decimal Price { get => _Price; }
        public string USACityLocation { get => _USACityLocation; }
        public string Cuisine { get => _Cuisine; }
        public double FoodRating { get => _FoodRating; }
        public double ServiceRating { get => _ServiceRating; }
        public double AmbienceRating { get => _AmbienceRating;  }
        public double ValueRating { get => _ValueRating; }
        public double OverallRating { get => _OverallRating; }
        public double OverallPossibleRating { get => _OverallPossibleRating; }
    }
}
