using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MemoryGame
{
    class DBManager
    {
        SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyGame;MultipleActiveResultSets=true;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyGame;MultipleActiveResultSets=true;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public List<Leaderboard> displayLeaderboards()
        {
            List<Leaderboard> leaderboards = new List<Leaderboard>();
       
            try
            {
                // This will open a connection to the DB
                conn.Open();
                SqlCommand select = new SqlCommand($"select * from leaderboards_table", conn);
                SqlDataReader reader = select.ExecuteReader();
                
                // This will read the data from the DB and create a new object with those properties.
                while (reader.Read())
                {
                    leaderboards.Add(new Leaderboard(reader["name"].ToString(), Convert.ToInt32(reader["score"]), Convert.ToInt32(reader["wave"]),
                        reader["difficulty"].ToString(), Convert.ToInt32(reader["gridsize"])));
                }
           
            }
            catch (SqlException sql)
            {
                // THis will send a message to the console if the SQL connection failed
                Console.WriteLine($"{sql.Message}");
                Console.ReadLine();
            }
            finally
            {
                // At the end of the tast, the connection will be terminated. This is to save memory. 
                conn.Close();
            }
            return leaderboards;
            
        }


        // This Method is responsible for adding new entries to the leaderboards DB
        public void addToLeaderboards(Leaderboard leaderboard)
        {
        
            try
            {
                conn.Open();
                SqlCommand insert = new SqlCommand($"INSERT INTO leaderboards_table VALUES ('{leaderboard.Name.ToString()}', '{leaderboard.Score.ToString()}', '{leaderboard.Wave.ToString()}', '{leaderboard.Difficulty.ToString()}','{leaderboard.GridSize.ToString()}')", conn);
                insert.ExecuteReader();
            }
            catch (SqlException sql)
            {
                
            }
            finally
            {
                conn.Close();
            }
        }
     
        // THis method will check to see if the settings already exist within the DB.
        public Settings checkSettings()
        {
            Settings settings = null;

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"SELECT * FROM settings_table", conn);
                SqlDataReader reader = select.ExecuteReader();


                while (reader.Read())
                {
                    settings = new Settings(reader["name"].ToString(), reader["difficulty"].ToString(),
                Convert.ToInt32(reader["gridsize"].ToString()), Convert.ToBoolean(reader["multicolour"].ToString()),
                Convert.ToBoolean(reader["leaderboard"].ToString()));
                }


            }
            catch (InvalidOperationException noSql)
            {
                settings = null;
            }
            catch (SqlException sql)
            {

            }
            finally
            {
                conn.Close();
            }

            return settings;
        }

        // This Method will add entries to the settings DB
        public void addSettings(Settings settings)
        {
            try
            {
                conn.Open();
                SqlCommand add = new SqlCommand($"INSERT INTO settings_table VALUES ('{settings.Name}', '{settings.Difficulty}', '{settings.GridSize}', '{settings.MultiColour.ToString()}', '{settings.LeaderBoards.ToString()}')", conn);
                add.ExecuteReader();
            }
            catch (SqlException sql)
            {

            }
            finally
            {
            }
        }

        // Finally, this Method will update the settings in the settings DB
        public void updateSettings(Settings settings)
        {
            conn.Open();
            SqlCommand update = new SqlCommand($"UPDATE settings_table SET name = '{settings.Name}', difficulty = '{settings.Difficulty}', gridsize = '{settings.GridSize}', multicolour = '{settings.MultiColour}', leaderboard = '{settings.LeaderBoards}'", conn);
            update.ExecuteReader();
        }
    }
}
