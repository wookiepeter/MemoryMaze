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
        public Vector2i position { get; private set; }
        cellContent newContent;
        cellContent oldContent;

        bool active = false;

        public MapManipulation(Vector2i _position, cellContent _newContent, cellContent _oldContent)
        {
            position = _position;
            newContent = _newContent;
            oldContent = _oldContent;
        }

        private MapManipulation(MapManipulation _mapMani)
        {
            position = _mapMani.position;
            newContent = _mapMani.newContent;
            oldContent = _mapMani.oldContent;
        }

        public MapManipulation Copy()
        {
            return new MapManipulation(this);
        }

        public void execute(Map map)
        {
            if (active)
                Disengage(map);
            else
                Engage(map);
        }

        private void Engage(Map map)
        {
            cellContent help = oldContent;
            oldContent = newContent;
            map.SetContentOfCell(position, newContent);
            active = !active;
            newContent = oldContent;
        }

        private void Disengage(Map map)
        {
            cellContent help = newContent;
            newContent = oldContent;
            map.SetContentOfCell(position, oldContent);
            active = !active;
            oldContent = help;
        }
    }
}
