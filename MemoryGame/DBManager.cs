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
        public List<Leaderboard> displayLeaderboards()
        {
            List<Leaderboard> leaderboards = new List<Leaderboard>();
       
            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"select * from Games", conn);
                SqlDataReader reader = select.ExecuteReader();
                
                
                while (reader.Read())
                {
                    
                }
           
            }
            catch (SqlException sql)
            {
                Console.WriteLine($"{sql.Message}");
                Console.ReadLine();
            }
            finally
            {
                conn.Close();
            }
            return leaderboards;
            
        }


        public void addToLeaderboards(Leaderboard leaderboard)
        {
        
            try
            {
                conn.Open();
                SqlCommand insert = new SqlCommand($"INSERT INTO leaderboards_table VALUES ({leaderboard.Name}, {leaderboard.Difficulty}," +
                    $"{leaderboard.GridSize}, {leaderboard.Score}, {leaderboard.Wave})", conn);
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

        public Settings checkSettings()
        {
            Settings settings = null;

            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand($"SELECT * FROM settings_table");
                SqlDataReader reader = select.ExecuteReader();
                

                while (reader.Read())
                {
                    settings = new Settings(reader["name"].ToString(), reader["difficulty"].ToString(),
                Convert.ToInt32(reader["gridsize"].ToString()), Convert.ToBoolean(reader["multicolour"].ToString()),
                Convert.ToBoolean(reader["leaderboard"].ToString()));
                }

              
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

        public void addSettings(Settings settings)
        {
            try
            {
                conn.Open();
                SqlCommand add = new SqlCommand($"INSERT INTO settings_table VALUES ({settings.Name}, {settings.Difficulty}, " +
                    $"{settings.GridSize}, {settings.MultiColour.ToString()}, {settings.LeaderBoards.ToString()})");
            }
            catch (SqlException sql)
            {

            }
            finally
            {
            }
        }

        public void updateSettings(Settings settings)
        {

        }
    }
}
