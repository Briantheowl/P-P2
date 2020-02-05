using System.Data;
using MySql.Data.MySqlClient;



namespace PerezBrian_Integrative2
{
    class DBConn
    {
        
        //variable to hold connection string
        private MySqlConnection _conn;
        //variable used to activate the command line
        public MySqlCommand _cmd;
        //variable to fill and update datsets
        private MySqlDataAdapter _da;
        //variable to hold central object
        private DataTable _dt;

        public DBConn()
        {
            _conn = new MySqlConnection(@"server=10.63.12.88;username=brianPerez;password=root;database=SampleRestaurant;port=8889");
            _conn.Open();
        } 

        public void Query(string queryText)
        {
            _cmd = new MySqlCommand(queryText, _conn);
        }

        public DataTable QueryEx()
        {
            _da = new MySqlDataAdapter(_cmd);
            _dt = new DataTable();
            _da.Fill(_dt);
            return _dt;
        }

        public void NonQueryEx()
        {
            _cmd.ExecuteNonQuery();
        }
       
    }
}
