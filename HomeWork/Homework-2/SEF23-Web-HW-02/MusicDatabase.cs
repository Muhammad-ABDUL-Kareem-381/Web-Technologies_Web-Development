using Microsoft.Data.SqlClient;
using SEF23_Web_HW_02_S;
using System;

namespace SEF23_Web_HW_02_MDB
{
    public class MusicDatabase
    {
        // Fields
        private string cS = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Songs;Integrated Security=True;"; // Connection string to connect to the database
        private SqlConnection? connection = null; // SQL connection object
        private SqlCommand? cmd = null; // SQL command object
        private bool connected = false; // Indicates if the connection to the database is established

        // Constructors
        public MusicDatabase() // Default constructor 
        {

        }

        // Properties
        public string CS // Property to get the connection string
        {
            get { return cS; }
        }
        public SqlConnection? Connection // Property to get the SQL connection object
        {
            get { return connection; }
        }
        public bool Connected // Property to check if the connection to the database is established
        {
            get { return connected; }
        }
        public SqlCommand? Cmd // Property to get the SQL command object
        {
            get { return cmd; }
        }

        // Private Methods
        private bool IsSongExists(Song song) // Method to check if a song already exists in the database
        {
            string existsQuery = "SELECT COUNT(1) FROM Songs WHERE Title = @Title AND Artist = @Artist AND Duration = @Duration AND Genre = @Genre";
            cmd = new SqlCommand(existsQuery, connection);
            cmd.Parameters.AddWithValue("@Title", song.Title);
            cmd.Parameters.AddWithValue("@Artist", song.Artist);
            cmd.Parameters.AddWithValue("@Duration", song.Duration);
            cmd.Parameters.AddWithValue("@Genre", song.Genre);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        // Methods
        public void OpenConnection() // Method to open the connection to the database
        {
            if (connection == null)
            {
                connection = new SqlConnection(cS);
                connection.Open();
                connected = true;
            }
            else if (connection != null && !connected) // If connection object exists but not connected
            {
                connection.Open();
                connected = true;
            }
            else
            {
                Console.WriteLine("Connection is already open.");
            }
        }
        public void CloseConnection() // Method to close the connection to the database
        {
            if (connection != null && connected)
            {
                connection.Close();
                connected = false;
            }
            else
            {

            }
        }
        public void InsertSong(Song song) // Method to insert a new song into the database
        {
            OpenConnection();
            if (IsSongExists(song)) // Check if the song already exists in the database
            {
                Console.WriteLine($"\nSong '{song.Title}' by '{song.Artist}' already exists in the database.");
            }
            else // Insert the song into the database
            {
                string insertQuery = "INSERT INTO Songs (Title, Artist, Duration, Genre, IsLiked, PlayCount) VALUES ( @Title, @Artist, @Duration, @Genre, @IsLiked, @PlayCount)";
                cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@Title", song.Title);
                cmd.Parameters.AddWithValue("@Artist", song.Artist);
                cmd.Parameters.AddWithValue("@Duration", song.Duration);
                cmd.Parameters.AddWithValue("@Genre", song.Genre);
                cmd.Parameters.AddWithValue("@IsLiked", song.IsLiked);
                cmd.Parameters.AddWithValue("@PlayCount", song.PlayCount);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Song '{song.Title}' by '{song.Artist}' inserted successfully.");
            }
            CloseConnection();
        }
        public List<Song> GetAllSongs() // Method to retrieve all songs from the database
        {
            OpenConnection();
            List<Song> songs = new List<Song>();
            string selectQuery = "SELECT * FROM Songs";
            cmd = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) // Read each record from the database
            {
                Song song = new Song
                {
                    SongId = (int)reader["SongId"],
                    Title = (string)reader["Title"],
                    Artist = (string)reader["Artist"],
                    Duration = Convert.ToDouble(reader["Duration"]),
                    Genre = (string)reader["Genre"],
                    IsLiked = (bool)reader["IsLiked"],
                    PlayCount = (int)reader["PlayCount"]
                };
                songs.Add(song);
            }
            reader.Close();
            CloseConnection();
            return songs;
        }
        public int GetSongCount() // Method to get the total number of songs in the database
        {
            OpenConnection();
            int count = 0;
            string countQuery = "SELECT COUNT(*) FROM Songs";
            cmd = new SqlCommand(countQuery, connection);
            count = (int)cmd.ExecuteScalar();
            CloseConnection();
            return count;
        }
        public Song? GetSongById(int id) // Method to retrieve a song by its ID from the database
        {
            if (id > 0 && id <= GetAllSongs().Count)
            {
                OpenConnection();
                Song s = new Song();
                s.SongId = id;
                string selectQuery = "SELECT * FROM Songs WHERE SongId = @SongId";
                cmd = new SqlCommand(selectQuery, connection);
                cmd.Parameters.AddWithValue("@SongId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    s.Title = (string)reader["Title"];
                    s.Artist = (string)reader["Artist"];
                    s.Duration = Convert.ToDouble(reader["Duration"]);
                    s.Genre = (string)reader["Genre"];
                    s.IsLiked = (bool)reader["IsLiked"];
                    s.PlayCount = (int)reader["PlayCount"];
                }
                reader.Close();
                CloseConnection();
                return s;
            }
            else
            {
                Console.WriteLine($"\nSong with ID {id} does not exist in the database.\n");
                return null;
            }

        }
    }
}


