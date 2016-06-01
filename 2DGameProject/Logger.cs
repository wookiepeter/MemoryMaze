using System;
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

        public void updateTime(TimeSpan newTime)
        {
            curGameTime = newTime;
        }

        public void setLevel(int newLevel)
        {
            if(newLevel >= (int)level.Error && newLevel <= (int)level.Debug)
            {
                lvl = (level)newLevel;
            }
        }

        public void setWriteMode(Boolean _writeToConsole, Boolean _writeToFile)
        {
            writeToConsole = _writeToConsole;
            writeToFile = _writeToConsole;
        }

        public void write(String msg, int lvl)
        {
            if (lvl >= (int)level.Error && lvl <= (int)level.Debug)
            {
                messageString = levelMessage[lvl] + "[" + curGameTime.TotalSeconds.ToString().PadLeft(15, ' ') + "] " + msg;
                if(writeToConsole)
                    Console.WriteLine(messageString);
                if (writeToFile)
                    file.WriteLine(messageString);
            }
        }

        // write function with enum instead of int #justforCord #easierReadible
        public void write(String msg, level newlvl)
        {
            if (lvl >= level.Error && lvl <= level.Debug)
            {
                messageString = levelMessage[(int)lvl] + "[" + curGameTime.TotalSeconds.ToString().PadLeft(15, ' ') + "] " + msg;
                if (writeToConsole)
                    Console.WriteLine(messageString);
                if (writeToFile)
                    file.WriteLine(messageString);
            }
        }
    }
}
