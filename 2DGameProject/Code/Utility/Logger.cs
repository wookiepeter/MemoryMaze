﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaze
{
    public class Logger
    {
        private static Logger instance;
        private static TimeSpan curGameTime;
        private static String messageString;

        private static Boolean writeToFile;
        private static Boolean writeToConsole;
        private static Boolean disabled;

        static System.IO.StreamWriter file;

        static level lvl;
        static String[] levelMessage = { "ERROR ", "WARN  ", "INFO  ", "DEBUG " };

        public enum level
        {
            Error,
            Warning,
            Info,
            Debug
        }

        private Logger()
        {
            lvl = level.Error;
            writeToFile = false;
            writeToConsole = true;
            file = new System.IO.StreamWriter("../logfile.txt");
        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        public void UpdateTime(TimeSpan newTime)
        {
            curGameTime = newTime;
        }

        public void SetLevel(int newLevel)
        {
            if(newLevel >= (int)level.Error && newLevel <= (int)level.Debug)
            {
                lvl = (level)newLevel;
            }
        }

        public void SetWriteMode(Boolean _writeToConsole, Boolean _writeToFile)
        {
            writeToConsole = _writeToConsole;
            writeToFile = _writeToConsole;
            disabled = (writeToFile == false && writeToConsole == false);
        }

        public void Write(String msg, int _lvl)
        {
            if (disabled == false && _lvl >= (int)level.Error && _lvl <= (int)lvl)
            {
                messageString = levelMessage[_lvl] + "[" + curGameTime.TotalSeconds.ToString().PadLeft(15, ' ') + "] " + msg;
                if(writeToConsole)
                    Console.WriteLine(messageString);
                if (writeToFile)
                    file.WriteLine(messageString);
            }
        }

        // write function with enum instead of int #justforCord #easierReadible
        public void Write(String msg, level newlvl)
        {
            Write(msg, (int)newlvl);
        }
    }
}
