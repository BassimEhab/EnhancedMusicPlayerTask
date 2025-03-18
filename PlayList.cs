using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnhancedMusicPlayerTask
{
    public class PlayList
    {
        private readonly Music _Allmusic;
        public Dictionary<string, List<string>> Lists { get; set; } = new Dictionary<string, List<string>>();
        private Dictionary<string, Dictionary<string, int>> playCount = new Dictionary<string, Dictionary<string, int>>();
        private const int PlayLimit = 2;
        private Music _AllMusic;


        public PlayList(Music allMusic)
        {
            _Allmusic = allMusic;
        }

        public void CreateNewPlaylist(string listName)
        {
            if (!Lists.ContainsKey(listName))
            {
                Lists[listName] = new List<string>();
                Console.WriteLine($"Playlist '{listName}' created successfully!");
            }
            else
            {
                Console.WriteLine("Playlist already exists!");
            }
        }

        public void AddSongToExistingPlayList(string playListName, string songName)
        {
            if (Lists.ContainsKey(playListName))
            {
                if (_Allmusic.music.ContainsKey(songName) && !Lists[playListName].Contains(songName))
                {
                    Lists[playListName].Add(songName);
                    Console.WriteLine("Song added successfully!");
                }
                else
                {
                    Console.WriteLine("Cannot add this song. Either it doesn't exist or it's already in the playlist.");
                }
            }
            else
            {
                Console.WriteLine("Playlist does not exist!");
            }
        }

        public void RemoveSongFromPlayList(string playListName, string songName)
        {
            if (Lists.ContainsKey(playListName))
            {
                if (Lists[playListName].Contains(songName))
                {
                    Lists[playListName].Remove(songName);
                    Console.WriteLine("Song removed successfully!");
                }
                else
                {
                    Console.WriteLine("Cannot remove this song. It is not in the playlist.");
                }
            }
            else
            {
                Console.WriteLine("Playlist does not exist!");
            }
        }

        public void ViewAllSongsInPlayList(string playListName)
        {
            if (Lists.ContainsKey(playListName))
            {
                Console.WriteLine($"Playlist '{playListName}':");
                foreach (var song in Lists[playListName])
                {
                    Console.WriteLine($"- {song}");
                }
            }
            else
            {
                Console.WriteLine("Playlist does not exist!");
            }
        }

        public void Search(string songName)
        {
            bool isFound = false;
            foreach (var list in Lists)
            {
                if (list.Value.Contains(songName))
                {
                    isFound = true;
                    Console.WriteLine($"Song '{songName}' found in playlist: {list.Key}");
                }
            }

            if (!isFound)
            {
                Console.WriteLine("Song not found!");
            }
        }

        public async Task PlayPlaylist(string playListName, Song songPlayer, bool shuffle = false)
        {
            if (!Lists.ContainsKey(playListName) || Lists[playListName].Count == 0)
            {
                Console.WriteLine("Playlist is empty or does not exist!");
                return;
            }

            List<string> playListSongs = new List<string>(Lists[playListName]);

            if (shuffle)
            {
                Random random = new Random();
                playListSongs = playListSongs.OrderBy(x => random.Next()).ToList();
            }

            Console.WriteLine($"Now Playing Playlist: {playListName}");
            foreach (var song in playListSongs)
            {
                await songPlayer.ControlSong("start");
                Console.WriteLine($"Playing: {song}");
            }
        }
        public void PlaylistLoop(string playListName, Song songPlayer)
        {
            if (Lists.ContainsKey(playListName))
            {
                songPlayer.Loop();
            }
            else
            {
                Console.WriteLine("Playlist does not exist.");
            }
        }
        public void IncPlayCount(string playListName, string songName)
        {
            if (!playCount.ContainsKey(playListName))
                playCount[playListName] = new Dictionary<string, int>();

            if (!playCount[playListName].ContainsKey(songName))
                playCount[playListName][songName] = 0;

            playCount[playListName][songName]++;
            Console.WriteLine($"{songName} has been played {playCount[playListName][songName]} times.");

            RemoveOverplayedSongs(playListName);
        }

        private void RemoveOverplayedSongs(string playListName)
        {
            if (playCount.ContainsKey(playListName))
            {
                var overplayedSongs = playCount[playListName]
                    .Where(s => s.Value > PlayLimit)
                    .Select(s => s.Key)
                    .ToList();

                foreach (var song in overplayedSongs)
                {
                    Lists[playListName].Remove(song);
                    playCount[playListName].Remove(song);
                    Console.WriteLine($"❌ {song} was removed from {playListName} due to exceeding play limit.");
                }
            }
        }

        public void GetMostPlayedSong(string playListName)
        {
            if (playCount.ContainsKey(playListName) && playCount[playListName].Count > 0)
            {
                var mostPlayed = playCount[playListName].OrderByDescending(s => s.Value).First();
                Console.WriteLine($"Most played song in {playListName}: {mostPlayed.Key} ({mostPlayed.Value} plays)");
            }
            else
            {
                Console.WriteLine("No song has been played yet in this playlist.");
            }
        }





    }
}
