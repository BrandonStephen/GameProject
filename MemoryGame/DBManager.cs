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
                conn.Open();
                SqlCommand select = new SqlCommand($"select TOP 10 * from leaderboards_table ORDER BY score ACS", conn);
                SqlDataReader reader = select.ExecuteReader();
                
                
                while (reader.Read())
                {
                    leaderboards.Add(new Leaderboard(reader["name"].ToString(), Convert.ToInt32(reader["score"]), Convert.ToInt32(reader["wave"]),
                        reader["difficulty"].ToString(), Convert.ToInt32(reader["gridsize"])));
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

        public void updateSettings(Settings settings)
        {
            conn.Open();
            SqlCommand update = new SqlCommand($"UPDATE settings_table SET name = '{settings.Name}', difficulty = '{settings.Difficulty}', gridsize = '{settings.GridSize}', multicolour = '{settings.MultiColour}', leaderboard = '{settings.LeaderBoards}'", conn);
            update.ExecuteReader();
        }
    }
}
