using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
    public class Statisztika
    {
        private MySqlConnection connection;
        private List<Konyv> konyvek;

        public Statisztika()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "library";
            connection = new MySqlConnection(builder.ConnectionString);
        }

        public void Fill()
        {
            konyvek = new List<Konyv>();
            OpenConnection();
            string sql = "SELECT * FROM books";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string title = reader.GetString("title");
                    string author = reader.GetString("author");
                    int publish_year = reader.GetInt32("publish_year");
                    int page_count = reader.GetInt32("page_count");
                    Konyv konyv = new Konyv(id, title, author, publish_year, page_count);
                    konyvek.Add(konyv);
                }
            }
            CloseConnection(); 
        }

        private void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            } catch (MySqlException e)
            {
                throw new Exception("Hiba a kapcsolódás során: " + e.Message);
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            } catch (MySqlException e)
            {
                throw new Exception("Hiba a kapcsolódás során: " + e.Message);
            }
        }   
    }
}
