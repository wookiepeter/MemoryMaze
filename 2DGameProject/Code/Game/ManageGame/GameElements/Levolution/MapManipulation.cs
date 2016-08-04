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

        public void execute(Map map)
        {
            if (active)
                Disengage(map);
            else
                Engage(map);
        }

        private void Engage(Map map)
        {
            oldContent = map.GetContentOfCell(position);
            map.SetContentOfCell(position, newContent);
            active = !active;
        }

        private void Disengage(Map map)
        {
            newContent = map.GetContentOfCell(position);
            map.SetContentOfCell(position, oldContent);
            active = !active;
        }
    }
}
