﻿using System;
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


        public MusicManager()
        {

        }
        public static void PlayMusic(AssetManager.MusicName musicName)
        {
            
            music = AssetManager.GetMusic(musicName);
            music.Play();
            music.Loop = true;;

        }
        public static void StopMusic()
        {
            music.Stop();
            music.Loop = false;
        }


        

    }
}
