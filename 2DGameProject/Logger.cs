using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject2D
{
    public class Logger
    {
        private static Logger instance;
        private TimeSpan curGameTime;
        private String messageString;

        private Boolean writeToFile;
        private Boolean writeToConsole;

        System.IO.StreamWriter file;

        level lvl;
        String[] levelMessage = { "ERROR ", "WARN  ", "INFO  ", "DEBUG " };

        enum level
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
    }
}
