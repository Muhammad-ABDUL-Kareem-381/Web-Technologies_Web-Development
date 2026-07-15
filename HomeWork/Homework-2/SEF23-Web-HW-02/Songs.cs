using System;

namespace SEF23_Web_HW_02_S
{
    public class Song
    {
        // Fields
        private int songId; // Unique identifier for the song
        private string? title; // Title of the song
        private string? artist; // Artist or band name
        private double duration; // in minutes
        private string? genre; // Genre of the song
        private bool isLiked; // Indicates if the song is liked by the user
        private int playCount; // Number of times the song has been played

        // Constructors
        public Song() // Default constructor
        {
            songId = 0;
            title = null;
            artist = null;
            duration = 0.0;
            genre = null;
            isLiked = false;
            playCount = 0;
        }
        public Song(int id, string title, string artist, double duration, string genre, bool isLiked = false, int playCount = 0) // Parameterized constructor
        {
            this.songId = id;
            this.title = title;
            this.artist = artist;
            this.duration = duration;
            this.genre = genre;
            this.isLiked = isLiked;
            this.playCount = playCount;
        }

        // Properties
        public int SongId // Unique identifier for the song
        {
            get { return songId; }
            set { songId = value; }
        }
        public string? Title // Title of the song
        {
            get { return title; }
            set { title = value; }
        }
        public string? Artist // Artist or band name
        {
            get { return artist; }
            set { artist = value; }
        }
        public double Duration // Duration of the song in minutes
        {
            get { return duration; }
            set { duration = value; }
        }
        public string? Genre // Genre of the song
        {
            get { return genre; }
            set { genre = value; }
        }
        public bool IsLiked // Indicates if the song is liked by the user
        {
            get { return isLiked; }
            set { isLiked = value; }
        }
        public int PlayCount // Number of times the song has been played
        {
            get { return playCount; }
            set { playCount = value; }
        }

        // Methods
        public void PlaySong() // Simulates playing the song
        {
            playCount++;
        }
        public void DisplaySongInfo() // Displays song information
        {
            Console.WriteLine($"ID: {songId}\nTitle: {title}\nArtist: {artist}\nDuration: {duration} mins\nGenre: {genre}\nLiked: {isLiked}\nPlay Count: {playCount}");
        }

        // Override Methods
        public override string ToString() // Overrides the ToString method to provide a string representation of the song
        {
            return $"{songId}, {title}, {artist}, {duration}, {genre}, {isLiked}, {playCount}";
        }
    }
}
