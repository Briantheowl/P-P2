using System.Data;
using MySql.Data.MySqlClient;



namespace PerezBrian_CustomAppCode
{
    class DBConn
    {
        
        //variable to hold connection string
        public MySqlConnection _conn;
        //variable used to activate the command line
        public MySqlCommand _cmd;
        //variable to fill and update datasets
        private MySqlDataAdapter _da;
        //variable to hold central object of database as a whole
        private DataTable _dt;

        public DBConn()
        {
            _conn = new MySqlConnection(@"server=192.168.0.19;username=brianPerez;password=root;database=PerezBrian_CustomEER;port=8889");
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
