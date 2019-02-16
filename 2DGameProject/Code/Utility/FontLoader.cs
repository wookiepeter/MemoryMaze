using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace MemoryMaze
{
    public class FontLoader
    {
        private static FontLoader instance;
        private static Dictionary<String, Font> fontDic;


        private FontLoader()
        {
            fontDic = new Dictionary<string, Font>();
        }

        public static FontLoader Instance
        {
            get
            {
                if (instance == null)
                    instance = new FontLoader();
                return instance;
            }
        }

        public Font LoadFont(String path)
        {
            if (fontDic.ContainsKey(path) == false)
            {
                fontDic.Add(path, new Font(path));
            }

            return fontDic[path];
        }
    }
}
