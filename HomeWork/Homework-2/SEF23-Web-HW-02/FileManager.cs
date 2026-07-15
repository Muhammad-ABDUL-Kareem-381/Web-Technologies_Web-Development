using System;
using System.IO;
using SEF23_Web_HW_02_S;

namespace SEF23_Web_HW_02_FM
{
    public class FileManager
    {
        //Constructors
        public FileManager() // Default constructor 
        {

        }

        //Methods
        public void SaveSongsToText(List<Song> songs, string fileName)
        {
            if (songs == null || songs.Count == 0)
            {
                Console.WriteLine("\nNo songs to save.\n");
                return;
            }
            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var song in songs)
            {
                sw.WriteLine(song.ToString());
            }
            sw.Close();
            fs.Close();
            Console.WriteLine($"\nSongs saved to text file {fileName} successfully.\n");
        }
        public List<Song> LoadSongsFromText(string filename)
        {
            List<Song> songs = new List<Song>();
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return songs;
            }
            FileStream fs = new FileStream(filename, FileMode.Open);
            if (fs.Length == 0)
            {
                Console.WriteLine("File is empty.");
                return songs;
            }
            StreamReader sw = new StreamReader(fs);
            string? temp;
            while ((temp = sw.ReadLine()) != null)
            {
                var parts = temp.Split(',');
                Song song = new Song(int.Parse(parts[0].Trim()), (parts[1].Trim()), (parts[2].Trim()), double.Parse(parts[3].Trim()), (parts[4].Trim()), bool.Parse(parts[5].Trim()), int.Parse(parts[6].Trim()));
                songs.Add(song);
            }
            sw.Close();
            fs.Close();
            return songs;
        }
    }
}
