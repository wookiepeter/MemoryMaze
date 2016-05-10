using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject2D
{
    // enum goes global
    enum cellContent
    {
        Empty,
        Item,
        Wall
    };

    class Cell
    {
        cellContent content;

        public Cell(cellContent _content)
        {
            content = _content;
        }

        public cellContent getContent()
        {
            return content;
        }

        public void setContent(cellContent _content)
        {
            content = _content;
        }
    }
}
