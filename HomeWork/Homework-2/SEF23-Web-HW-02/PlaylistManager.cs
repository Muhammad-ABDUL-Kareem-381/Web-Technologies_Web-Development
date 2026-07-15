using System;
using SEF23_Web_HW_02_FM;
using SEF23_Web_HW_02_S;

namespace SEF23_Web_HW_02_PLM
{
    public class PlaylistManager
    {
        // Properties
        private string? playlistName; // Name of the playlist
        private int maxSongs; // Maximum number of songs allowed in the playlist
        private List<Song>? allSongs; // List to store songs in the playlist

        // Constructors
        public PlaylistManager() // Default constructor
        {
            playlistName = "";
            maxSongs = 0;
            allSongs = null;
        }

        public PlaylistManager(string pLName, int maximunCapacity) // Parameterized constructor
        {
            this.playlistName = pLName;
            this.maxSongs = maximunCapacity;
            this.allSongs = new List<Song>();
        }

        // Properties
        public string? PlaylistName // Name of the playlist
        {
            get { return playlistName; }
            set { playlistName = value; }
        }

        public int MaxSongs // Maximum number of songs allowed in the playlist
        {
            get { return maxSongs; }
            set { maxSongs = value; }
        }

        public List<Song>? AllSongs // List to store songs in the playlist
        {
            get { return allSongs; }
            set { allSongs = value; }
        }

        // Private Methods
        private bool IsSongExists(Song song) // Method to check if a song already exists in the playlist
        {
            if (allSongs != null)
            {
                foreach (var s in allSongs)
                {
                    if (s.Title == song.Title && s.Artist == song.Artist && s.Duration == song.Duration && s.Genre == song.Genre)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Public Methods
        public void AddMultipleSongs(params Song[] songs) // Method to add multiple songs to the playlist
        {
            int totalCount = 0;
            if (songs.Length == 0)
            {
                Console.WriteLine("\nNo songs provided to add.");
            }
            else if (allSongs == null)
            {
                Console.WriteLine("\nPlaylist not initialized. Please create a playlist first.");
            }
            else if (allSongs.Count >= maxSongs)
            {
                Console.WriteLine("\nPlaylist is already full. Cannot add more songs.");
            }
            else
            {
                foreach (var song in songs)
                {
                    if (allSongs.Count < maxSongs && !IsSongExists(song))
                    {
                        allSongs.Add(song);
                        Console.WriteLine($"Song '{song.Title}' added to the playlist.");
                        totalCount++;
                    }
                    else if (allSongs.Count >= maxSongs)
                    {
                        Console.WriteLine("\nPlaylist has reached its maximum capacity. Cannot add more songs.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Song '{song.Title}' already exists in the playlist. Skipping duplicate.");
                    }
                }
                //if (totalCount == songs.Count())
                //{
                //    Console.WriteLine("\nAll songs added successfully.");
                //}
                //else
                //{
                //    Console.WriteLine($"Only {totalCount} out of {songs.Length} songs were added to the playlist due to playlist capacity.");
                //}
            }
        }

        public void CreatePlaylist(string name, int maxSongs = 10) // Method to create a new playlist
        {
            this.playlistName = name;
            this.maxSongs = maxSongs;
            this.allSongs = new List<Song>();
            Console.WriteLine($"\nPlaylist '{name}' created with a maximum capacity of {maxSongs} songs.\n");
        }

        public List<Song>? FindSongsByGenre(string genre) // Method to find songs by genre
        {
            int totalCount = 0;
            List<Song> temp = new List<Song>();
            if (allSongs == null)
            {
                Console.WriteLine("Playlist not initialized. Please create a playlist first.");
            }
            else if (allSongs.Count == 0)
            {
                Console.WriteLine("Playlist is empty.");
            }
            else
            {
                foreach (var song in allSongs)
                {
                    if (song.Genre != null && song.Genre.Equals(genre))
                    {
                        temp.Add(song);
                        totalCount++;
                    }
                }
                if (totalCount == 0)
                {
                    Console.WriteLine($"No songs found in the genre '{genre}'.");
                }
                else
                {
                    Console.WriteLine($"\n{totalCount} song(s) found in the genre '{genre}'.");
                }
            }
            return temp;
        }

        public void GetPlaylistStatistics() // Method to get statistics about the playlist
        {
            int count = 0;
            double durationCount = 0;
            int songsCount = 0;
            while (allSongs != null && count < allSongs.Count)
            {
                durationCount += allSongs[count].Duration;
                count++;
            }
            count = 0;
            while (allSongs != null && count < allSongs.Count)
            {
                if (allSongs[count].IsLiked)
                {
                    songsCount++;
                }
                count++;
            }
            Console.WriteLine($"\nTotal number of songs in the playlist: {allSongs?.Count}");
            Console.WriteLine($"Total duration of all songs in the playlist: {durationCount} mins");
            Console.WriteLine($"Number of liked songs in the playlist: {songsCount}\n");
        }
    }
}
