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

        public List<Konyv> Konyvek { get => konyvek; set => konyvek = value; }

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

        public bool Delete(int id)
        {
            OpenConnection();

            string sql = "DELETE FROM books WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);

            int affectedRows = command.ExecuteNonQuery();

            CloseConnection();

            return affectedRows == 1;
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

        public int LongerThan500Page()
        {
            int count = 0;
            foreach (Konyv item in konyvek)
            {
                if (item.Page_count > 500)
                {
                    count++;
                }
            }

            return count;
        }

        public bool OlderThan1950()
        {
            foreach (Konyv item in konyvek)
            {
                if (item.Publish_year < 1950)
                {
                    return true;
                }
            }

            return false;
        }

        public Konyv LongestBook()
        {
            Konyv longest = konyvek[0];
            foreach (Konyv item in konyvek)
            {
                if (item.Page_count > longest.Page_count)
                {
                    longest = item;
                }
            }

            return longest;
        }

        public string MostPopularAuthor()
        {
            Dictionary<string, int> authors = new Dictionary<string, int>();
            foreach (Konyv item in konyvek)
            {
                if (authors.ContainsKey(item.Author))
                {
                    authors[item.Author]++;
                }
                else
                {
                    authors.Add(item.Author, 1);
                }
            }

            string mostPopular = "";
            int max = 0;
            foreach (KeyValuePair<string, int> item in authors)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    mostPopular = item.Key;
                }
            }

            return mostPopular;
        }
    }
}
