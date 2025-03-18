using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnhancedMusicPlayerTask
{
    public class Song
    {
        private readonly Music _Allmusic;
        private WaveOutEvent _outputDevice = new WaveOutEvent();
        private AudioFileReader _audioFile;
        private List<KeyValuePair<string, string>> songList;
        private List<int> shuffleOrder;
        private int currentIndex = 0;
        private bool isShuffleMode = false;
        private bool IsLooping = false;
        private Random random = new Random();

        public Dictionary<string, string> Songs { get; set; } = new Dictionary<string, string>();

        private PlayList _playlist;
        public Song(Music allMusic, PlayList playlist)
        {
            _Allmusic = allMusic;
            _playlist = playlist;
            InitializeSongList();
            GenerateShuffleOrder();
        }


        private void InitializeSongList()
        {
            songList = _Allmusic.music.ToList();
        }

        private void GenerateShuffleOrder()
        {
            shuffleOrder = Enumerable.Range(0, songList.Count).OrderBy(x => random.Next()).ToList();
        }

        public async Task ControlSong(string choose,string Lname=null)
        {
            bool isPlaylist=false;
            if (Lname != null)
            {
                isPlaylist=true;
            }
            switch (choose)
            {
                case "start":
                    if(isPlaylist)
                        PlaySongAtIndex(currentIndex,false,Lname);
                    else
                        PlaySongAtIndex(currentIndex);
                    break;
                case "pause":
                    _outputDevice.Pause();
                    break;
                case "play":
                    _outputDevice.Play();
                    break;
                case "restart":
                    _outputDevice.Stop();
                    _audioFile.Position = 0;
                    _outputDevice.Play();
                    break;
                case "prev":
                    _outputDevice.Stop();
                    PlayPreviousSong();
                    break;
                case "next":
                    _outputDevice.Stop();
                    PlayNextSong();
                    break;
                case "shuff":
                    isShuffleMode = true;
                    GenerateShuffleOrder();
                    currentIndex = 0;
                    PlaySongAtIndex(shuffleOrder[currentIndex]);
                    break;
            }
        }


        public void Loop()
        {
            IsLooping = !IsLooping;
            Console.WriteLine(IsLooping ? "Loop mode activated." : "Loop mode deactivated.");
        }

        private void PlaySongAtIndex(int index, bool shuff = false, string Name=null)
        {
            if (index < 0 || index >= songList.Count) return;

            _outputDevice.Stop();
            _audioFile?.Dispose();
            _audioFile = new AudioFileReader(songList[index].Value);
            _outputDevice.Init(_audioFile);

            _outputDevice.PlaybackStopped += (s, e) =>
            {
                if (IsLooping && currentIndex == songList.Count - 1)
                {
                    PlaySongAtIndex(0);
                }
                else
                {
                    PlayNextSong();
                }
            };

            _outputDevice.Play();
            currentIndex = index;
            if (Name != null)
            {
                _playlist.IncPlayCount(Name, songList[index].Key);

            }

        }


        private void PlayNextSong()
        {
            if (isShuffleMode)
            {
                if (currentIndex + 1 < shuffleOrder.Count)
                {
                    PlaySongAtIndex(shuffleOrder[++currentIndex]);
                }
                else
                {
                    GenerateShuffleOrder();
                    currentIndex = 0;
                    PlaySongAtIndex(shuffleOrder[currentIndex]);
                }
            }
            else
            {
                if (currentIndex + 1 < songList.Count)
                {
                    PlaySongAtIndex(++currentIndex);
                }
            }
        }

        private void PlayPreviousSong()
        {
            if (isShuffleMode)
            {
                if (currentIndex - 1 >= 0)
                {
                    PlaySongAtIndex(shuffleOrder[--currentIndex]);
                }
            }
            else
            {
                if (currentIndex - 1 >= 0)
                {
                    PlaySongAtIndex(--currentIndex);
                }
            }
        }
        public void MoveInSong(string Dir)
        {
            if (_audioFile != null)
            {
                long newPosition;
                switch (Dir)
                {
                    case "f":
                        newPosition = _audioFile.Position + (_audioFile.WaveFormat.AverageBytesPerSecond * 10);
                        _audioFile.Position = Math.Min(newPosition, _audioFile.Length);
                        break;
                    case "b":
                        newPosition = _audioFile.Position - (_audioFile.WaveFormat.AverageBytesPerSecond * 5);
                        _audioFile.Position = Math.Max(newPosition, 0);
                        break;
                }
            }
            else
                Console.WriteLine("There is no song is playing");
        }

    }
}
