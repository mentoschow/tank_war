using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Tank_War.Properties;

namespace Tank_War
{
    enum SoundState
    {
        START,
        ADD,
        FIRE,
        HIT,
        BLAST
    }
    class SoundManager
    {
        private static List<SoundPlayer> soundPlayer = new List<SoundPlayer>();
        private static SoundState soundState = SoundState.START;

        public static void InitSoundPlayer()
        {
            for(int i = 0; i < 5; i++)
            {
                SoundPlayer _soundPlayer = new SoundPlayer();
                soundPlayer.Add(_soundPlayer);
            }
            soundPlayer[0].Stream = Resources.start;
            soundPlayer[1].Stream = Resources.add;
            soundPlayer[2].Stream = Resources.fire;
            soundPlayer[3].Stream = Resources.hit;
            soundPlayer[4].Stream = Resources.blast;
        }

        public static void Play(SoundState _soundState)
        {
            SoundUpdate(_soundState);
            soundPlayer[(int)soundState].Play();
        }

        private static void SoundUpdate(SoundState _soundState)
        {
            soundState = _soundState;
        }
    }
}
