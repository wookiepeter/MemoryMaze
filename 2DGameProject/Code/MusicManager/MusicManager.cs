using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;

namespace MemoryMaze
{
    public class MusicManager
    {

       static Music music;
        static Sound sound;
        


        public MusicManager()
        {

        }
        public static void PlayMusic(AssetManager.MusicName musicName)
        {
            
            music = AssetManager.GetMusic(musicName);
            music.Volume = 18;
            music.Play();
            music.Loop = true;;

        }
        public static void StopMusic()
        {
            music.Stop();
            music.Loop = false;
        }
        public static void PlaySound(AssetManager.SoundName soundName)
        {
            

            if(AssetManager.SoundName.CreateBot == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 25;
            }
            if (AssetManager.SoundName.Key == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 30;
            }
            if (AssetManager.SoundName.LeverNormal == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 20;
            }
            if (AssetManager.SoundName.BlueLever == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 10;
            }
            if (AssetManager.SoundName.MenueClick == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 15;
            }
            if (AssetManager.SoundName.Teleport == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 35;
            }
            if (AssetManager.SoundName.VirusDetected == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 2f;
            }
            if (AssetManager.SoundName.DeleteBot == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 50;
            }
            if (AssetManager.SoundName.Wall == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 3;
            }
            if (AssetManager.SoundName.ItemPick == soundName)
            {
                sound = new Sound(AssetManager.GetSound(soundName));
                sound.Volume = 20;
            }
            sound.Play();
        }
        public static void StopSound()
        {
            if (sound.Status != 0)
                sound.Stop();
        }

        

    }
}
