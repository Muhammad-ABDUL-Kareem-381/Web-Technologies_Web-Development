using SEF23_Web_HW_02_FM;
using SEF23_Web_HW_02_MDB;
using SEF23_Web_HW_02_S;
using SEF23_Web_HW_02_PLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Data.SqlClient;

namespace SEF23_Web_HW_02_F
{
    public class Functions
    {
        // Constructors
        public Functions() // Default constructor 
        {

        }

        // Referances to other classes
        PlaylistManager pm = new PlaylistManager();
        FileManager fm = new FileManager();
        MusicDatabase db = new MusicDatabase();
        List<Song> songs = new List<Song>();

        // Private Methods
        private Song CreateSong() // Method to create a new song by taking user input
        {
            Song song = new Song();
            Console.Write("Wnter SongId: ");
            string? input;
            input = Console.ReadLine();
            while (!int.TryParse(input, out int id))
            {
                Console.Write("Invalid input. Please enter a valid integer for SongId: ");
                input = Console.ReadLine();
            }
            song.SongId = int.Parse(input);
            Console.Write("Enter Title: ");
            input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Invalid input. Please enter a valid title: ");
                input = Console.ReadLine();
            }
            song.Title = input;
            Console.Write("Enter Artist: ");
            input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Invalid input. Please enter a valid artist name: ");
                input = Console.ReadLine();
            }
            song.Artist = input;
            Console.Write("Enter Duration (in minutes): ");
            input = Console.ReadLine();
            while (!double.TryParse(input, out double dur) || dur <= 0)
            {
                Console.Write("Invalid input. Please enter a valid positive number for duration: ");
                input = Console.ReadLine();
            }
            song.Duration = double.Parse(input);
            Console.Write("Enter Genre: ");
            input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Invalid input. Please enter a valid genre: ");
                input = Console.ReadLine();
            }
            song.Genre = input;
            Console.Write("Is the song liked? (yes/no): ");
            string? likedInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(likedInput))
            {
                Console.Write("Invalid input. Please enter 'yes' or 'no': ");
                likedInput = Console.ReadLine();
            }
            if (likedInput.ToLower() == "yes")
            {
                song.IsLiked = true;
            }
            else
            {
                song.IsLiked = false;
            }
            song.PlayCount = 0; // Initialize play count to 0
            return song;
        }

        private void AddSongsIntoPlaylist() // Method to add songs from the songs list into the playlist
        {
            if (songs == null || songs.Count == 0)
            {
                Console.WriteLine("No songs available to add to the playlist. Please create or load songs first.\n");
                return;
            }
            else
            {
                foreach (var song in songs)
                {
                    if (pm.AllSongs == null)
                    {
                        Console.WriteLine("\nPlaylist not initialized. Please create a playlist first.");
                    }
                    else if (pm.AllSongs.Count < pm.MaxSongs)
                    {
                        pm.AddMultipleSongs(song);
                    }
                    else
                    {
                        Console.WriteLine("\nPlaylist is full. Cannot add more songs.");
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        private void UpdatePlayCount(int songId, int newPlayCount) // Method to update the play count of a song in the database
        {
            db.OpenConnection();
            string? updateQurey = "UPDATE Songs SET PlayCount = @PlayCount WHERE SongId = @Songid";
            SqlCommand cmd = new SqlCommand(updateQurey, db.Connection);
            cmd.Parameters.AddWithValue("@PlayCount", newPlayCount);
            cmd.Parameters.AddWithValue("@SongId", songId);
            cmd.ExecuteNonQuery();
            db.CloseConnection();
        }

        // Public Methods
        public void CreateSongs() // Method to create multiple songs and add them to the songs list
        {
            int count = 0;
            Console.Write("Enter the amount of songs that you want to create: ");
            int.TryParse(Console.ReadLine(), out count);
            while (count < 5)
            {
                Console.WriteLine("You must create at least 5 songs.");
                Console.Write("Enter the amount of songs that you want to create: ");
                int.TryParse(Console.ReadLine(), out count);
            }
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n\t\t\tCreating song {i + 1}:");
                songs.Add(CreateSong());
            }
            Console.WriteLine($"\n{count} song(s) created successfully.\n");
        }

        public void DisplayAllSongs(List<Song>? list = null) // Method to display all songs in the playlist
        {
            if (list != null)
            {
                if (list.Count == 0)
                {
                    Console.WriteLine("No songs to display.\n");
                    return;
                }
                foreach (var song in list)
                {
                    Console.WriteLine($"ID: {song.SongId}\nTitle: {song.Title}\nArtist: {song.Artist}\nDuration: {song.Duration} mins\nGenre: {song.Genre}\nLiked: {song.IsLiked}\nPlay Count: {song.PlayCount}\n");
                }
            }
            else
            {
                if (songs == null || songs.Count == 0)
                {
                    Console.WriteLine("No songs to display.\n");
                    return;
                }
                foreach (var song in songs)
                {
                    Console.WriteLine($"ID: {song.SongId}\nTitle: {song.Title}\nArtist: {song.Artist}\nDuration: {song.Duration} mins\nGenre: {song.Genre}\nLiked: {song.IsLiked}\nPlay Count: {song.PlayCount}\n");
                }
            }
        }

        public void SaveSongsToTextFile() // Method to save songs to a text file
        {
            Console.Write("\nDo you want to save songs to a desegnated text file? (yes/no): ");
            string? saveChoice = Console.ReadLine();
            if (saveChoice != null && saveChoice.ToLower() == "yes")
            {
                Console.Write("Enter the filename to save songs (FileName.txt): ");
                string? filename = Console.ReadLine();
                if (!string.IsNullOrEmpty(filename))
                {
                    fm.SaveSongsToText(songs, filename);
                }
                else
                {
                    Console.WriteLine("Invalid filename.");
                }
            }
            else
            {
                fm.SaveSongsToText(songs, "songs.txt");
            }
        }

        public void LoadSongsFromTextFiles() // Method to load songs from a text file
        {
            Console.Write("\nDo you want to load songs from a designated text file? (yes/no): ");
            string? loadChoice = Console.ReadLine();
            if (loadChoice != null && loadChoice.ToLower() == "yes")
            {
                Console.Write("Enter the filename to load songs from (FileName.txt): ");
                string? filename = Console.ReadLine();
                if (!string.IsNullOrEmpty(filename))
                {
                    songs = fm.LoadSongsFromText(filename); ;
                    Console.WriteLine($"\nSongs loaded from text file {filename} successfully.\n");
                }
                else
                {
                    Console.WriteLine("Invalid filename.");
                }
            }
            else
            {
                songs = fm.LoadSongsFromText("songs.txt");
                Console.WriteLine("\nSongs loaded from default text file songs.txt successfully.\n");
            }
        }

        public void InsertSongsIntoDatabase() // Method to insert songs into the database
        {
            if (songs == null || songs.Count == 0)
            {
                Console.WriteLine("\nNo songs available to insert into the database. Please create or load songs first.\n");
                return;
            }
            else
            {
                foreach (var song in songs)
                {
                    db.InsertSong(song);
                }
                Console.WriteLine("\n");
            }
        }

        public void DisplayAllSongsFromDatabase() // Method to display all songs from the database
        {
            Console.WriteLine("\n\t\t\tAll Songs in the Database:");
            songs = db.GetAllSongs();
            DisplayAllSongs();
        }

        public void GetSongById() // Method to retrieve and display a song by its ID from the database
        {
            Console.Write("Enter the Song ID to retrieve: ");
            string? idInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int songId) || int.Parse(idInput) <= 0)
            {
                Console.Write("Invalid input. Please enter a valid Song ID: ");
                idInput = Console.ReadLine();
            }
            int id = int.Parse(idInput);
            Song? song = db.GetSongById(id);
            if (song == null)
            {
                return;
            }
            else
            {
                Console.WriteLine($"\nSong details for ID {id}:\n");
                song.DisplaySongInfo();
                Console.WriteLine();
            }
        }

        public void GetTotalSongCount() // Method to get and display the total number of songs in the database
        {
            int count = db.GetSongCount();
            Console.WriteLine($"\nTotal number of songs in the database: {count}\n");
        }

        public void CreatepLaylist() // Method to create a playlist and add songs to it
        {
            if (pm.AllSongs != null)
            {
                Console.WriteLine("\nA playlist already exists. Cannot create a new one.\n");
            }
            else
            {
                Console.Write("Enter the name of the playlist: ");
                string? name = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Invalid input. Please enter a valid playlist name: ");
                    name = Console.ReadLine();
                }
                Console.Write("Enter the maximum number of songs for the playlist: ");
                string? maxInput = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(maxInput) || !int.TryParse(maxInput, out int maxSongs) || int.Parse(maxInput) <= 0)
                {
                    Console.Write("Invalid input. Please enter a valid positive number for maximum songs: ");
                    maxInput = Console.ReadLine();
                }
                pm.CreatePlaylist(name, int.Parse(maxInput));
            }
            Console.Write("Insert songs into the playlist from database or List: ");
            if (Console.ReadLine()!.ToLower() == "database")
            {
                Console.WriteLine();
                songs = db.GetAllSongs();
            }
            Console.WriteLine() ;
            AddSongsIntoPlaylist();
        }

        public void DisplayPlaylistStatistics() // Method to display statistics of the playlist
        {
            pm.GetPlaylistStatistics();
        }

        public void FindSongsByGenre() // Method to find and display songs by genre from the playlist
        {
            Console.Write("Enter the genre to search for: ");
            string? genre = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(genre))
            {
                Console.Write("\nInvalid input. Please enter a valid genre: ");
                genre = Console.ReadLine();
            }
            List<Song>? genreSongs = pm.FindSongsByGenre(genre);
            if (genreSongs == null || genreSongs.Count == 0)
            {
                Console.WriteLine($"\nNo songs found in the genre '{genre}'.\n");
            }
            else
            {
                Console.WriteLine($"\nSongs in the genre '{genre}':");
                DisplayAllSongs(genreSongs);
            }
        }

        public void PlaySongById() // Method to play a song by its ID from the database
        {
            if (db.CS == null )
            {
                Console.WriteLine("\nDatabase does not exist.\n");
                return;
            }
            Console.Write("Enter the Song ID to play: ");
            string? idInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int songId) || int.Parse(idInput) == 0)
            {
                Console.Write("Invalid input. Please enter a valid Song ID: ");
                idInput = Console.ReadLine();
            }
            int id = int.Parse(idInput);
            Song? song = db.GetSongById(id);
            if (song == null)
            {
                Console.WriteLine($"Song with ID {id} cannot be played because it does not exist in the database.\n");
                return;
            }
            else
            {
                int playCount = song.PlayCount + 1;
                UpdatePlayCount(id, playCount);
                Console.WriteLine($"\nSong with Id = {id} in the database, played succeccfully\n");
            }
        }
    }
}
