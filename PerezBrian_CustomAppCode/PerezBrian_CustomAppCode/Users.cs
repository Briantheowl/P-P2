using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerezBrian_CustomAppCode
{
    class Users
    {
        int _userid;
        string _firstName;
        string _lastName;
        string _userName;
        string _password;

        public Users(int user_id, string first_name, string last_name, string user_name, string password)
        {
            _userid = user_id;
            _firstName = first_name;
            _lastName = last_name;
            _userName = user_name;
            _password = password;
        }

        public int  User_id { get => _userid; }
        public string First_name { get => _firstName; }
        public string Last_name { get => _lastName; }
        public string User_name { get => _userName; }
        public string Password { get => _password; }

    }
}
