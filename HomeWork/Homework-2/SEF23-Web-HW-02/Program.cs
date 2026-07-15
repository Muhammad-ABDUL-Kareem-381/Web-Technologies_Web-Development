using System;
using SEF23_Web_HW_02_S;
using SEF23_Web_HW_02_FM;
using SEF23_Web_HW_02_PLM;
using SEF23_Web_HW_02_MDB;
using SEF23_Web_HW_02_F;


namespace SEF23_Web_HW_02_M
{
    public class MainProgram
    {
        static void Main(string[] args)
        {

            Functions f = new Functions();
            Console.WriteLine("\n\t\t\t\t<--------- Music Library Menu -------->");
            int choise = 0;

            do
            {
                Console.WriteLine("Select 1 for Create Songs");
                Console.WriteLine("Select 2 for Display All Songs");
                Console.WriteLine("Select 3 for Save Songs to Text File");
                Console.WriteLine("Select 4 for Load Songs from Text File");
                Console.WriteLine("Select 5 for Insert Songs into Database");
                Console.WriteLine("Select 6 for Display All Songs from Database");
                Console.WriteLine("Select 7 for Get Song by ID");
                Console.WriteLine("Select 8 for Get Total Song Count");
                Console.WriteLine("Select 9 for Create Playlist");
                Console.WriteLine("Select 10 for Display Playlist Statistics");
                Console.WriteLine("Select 11 for Find Songs by Genre");
                Console.WriteLine("Select 12 for Play a Song");
                Console.WriteLine("Select 13 for Exit");
                Console.Write("Enter your choice: ");
                string? num = Console.ReadLine();
                int.TryParse(num, out choise);

                if (choise == 1) // Create Songs
                {
                    f.CreateSongs();
                }

                else if (choise == 2) // Display All Songs
                {
                    Console.WriteLine("\n\t\t\tAll Songs in the list:");
                    f.DisplayAllSongs();
                }

                else if (choise == 3) // Save Songs to Text File
                {
                    f.SaveSongsToTextFile();
                }

                else if (choise == 4) // Load Songs from Text File
                {
                    f.LoadSongsFromTextFiles();
                }

                else if (choise == 5) // Insert Songs into Database
                {
                    f.InsertSongsIntoDatabase();
                }

                else if (choise == 6) // Display All Songs from Database
                {
                    f.DisplayAllSongsFromDatabase();
                }

                else if (choise == 7) // Get Song by ID
                {
                    f.GetSongById();
                }

                else if ( choise == 8) // Get Total Song Count
                {
                    f.GetTotalSongCount();
                }

                else if ( choise == 9) // Create Playlist
                {
                    f.CreatepLaylist();
                }

                else if (choise == 10) // Display Playlist Statistics
                {
                    f.DisplayPlaylistStatistics();
                }
                else if (choise == 11) // Find Songs by Genre
                {
                    f.FindSongsByGenre();
                }
                else if (choise == 12) // Play a Song
                {
                    f.PlaySongById();
                }
                else if (choise == 13) // Exit
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option from the menu.\n");
                }
            }
            while (choise != 13);
            return;
        }
    }
    
}
