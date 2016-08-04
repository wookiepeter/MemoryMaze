using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public class MapManipulation
    {
        Vector2i position;
        cellContent newContent;

        public MapManipulation(Vector2i _position, cellContent _newContent)
        {
            position = _position;
            newContent = _newContent;
        }

        private MapManipulation(MapManipulation _mapMani)
        {
            position = _mapMani.position;
            newContent = _mapMani.newContent;
        }

        public MapManipulation Copy()
        {
            return new MapManipulation(this);
        }
    }
}
