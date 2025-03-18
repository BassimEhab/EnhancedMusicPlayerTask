using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EnhancedMusicPlayerTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Music music = new Music();
            PlayList playList = new PlayList(music);
            Song songPlayer = new Song(music,playList);
            string playlistName = "", songName = "";

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("[1] Create new playlist");
                Console.WriteLine("[2] Add song to existing playlist");
                Console.WriteLine("[3] Remove song from playlist");
                Console.WriteLine("[4] View songs in a playlist");
                Console.WriteLine("[5] Search for a song");
                Console.WriteLine("[6] Play a playlist");
                Console.WriteLine("[7] Play song");
                Console.WriteLine("[8] Pause");
                Console.WriteLine("[9] Resume");
                Console.WriteLine("[10] Restart");
                Console.WriteLine("[11] backward");
                Console.WriteLine("[12] Fast forward");
                Console.WriteLine("[13] Play previous song");
                Console.WriteLine("[14] Play next song");
                Console.WriteLine("[15] Shuffle playlist");
                Console.WriteLine("[16] Loop playlist");
                Console.WriteLine("[17] Show most played song in playlist");


                Console.Write("=> ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter new playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            playList.CreateNewPlaylist(playlistName);
                            break;
                        case 2:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            Console.Write("Enter song name: ");
                            songName = Console.ReadLine().ToLower();
                            playList.AddSongToExistingPlayList(playlistName, songName);
                            break;
                        case 3:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            Console.Write("Enter song name: ");
                            songName = Console.ReadLine().ToLower();
                            playList.RemoveSongFromPlayList(playlistName, songName);
                            break;
                        case 4:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            playList.ViewAllSongsInPlayList(playlistName);
                            break;
                        case 5:
                            Console.Write("Enter song name: ");
                            songName = Console.ReadLine().ToLower();
                            playList.Search(songName);
                            break;
                        case 6:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            Console.Write("Shuffle? (yes/no): ");
                            bool shuffle = Console.ReadLine().ToLower() == "yes";
                            await playList.PlayPlaylist(playlistName, songPlayer, shuffle);
                            break;
                        case 7:
                            Console.Write("choose playlist?(y/n) => ");
                            string ch= Console.ReadLine().ToLower();
                            if (ch == "y")
                            {
                                Console.Write("enter play list name: ");
                                string name = Console.ReadLine().ToLower();
                                await songPlayer.ControlSong("start",name);
                            }
                            else
                                await songPlayer.ControlSong("start");

                            break;
                        case 8:
                            await songPlayer.ControlSong("pause");
                            break;
                        case 9:
                            await songPlayer.ControlSong("play");
                            break;
                        case 10:
                            await songPlayer.ControlSong("restart");
                            break;
                        case 11:
                            songPlayer.MoveInSong("b");
                            break;
                        case 12:
                            songPlayer.MoveInSong("f");
                            break;
                        case 13:
                            await songPlayer.ControlSong("prev");
                            break;
                        case 14:
                            await songPlayer.ControlSong("next");
                            break;
                        case 15:
                            await songPlayer.ControlSong("shuff");
                            break;
                        case 16:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            playList.PlaylistLoop(playlistName, songPlayer);
                            break;
                        case 17:
                            Console.Write("Enter playlist name: ");
                            playlistName = Console.ReadLine().ToLower();
                            playList.GetMostPlayedSong(playlistName);
                            break;


                        default:
                            Console.WriteLine("Invalid choice! Please choose a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
            }
        }
    }
}
