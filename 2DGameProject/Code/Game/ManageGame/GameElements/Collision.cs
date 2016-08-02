using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    interface Collider
    {
        Vector2f getPosition();
        Vector2u getSize();
    }

    class Collision<T, U>
        where T : Collider
        where U : Collider
    {

        public Collision()
        {

        }

        public static bool CheckColission(T obj1, U obj2)
        {
            Vector2f pos1 = obj1.getPosition();
            Vector2u size1 = 2 * obj1.getSize() / 3; 
            Vector2f pos2 = obj2.getPosition();
            Vector2u size2 = 2 * obj2.getSize() / 3;

            return CheckCollision(pos1, size1, pos2, size2);

        }

        public static bool CheckCollision(T obj1, Vector2f move1, U obj2, Vector2f move2)
        {
            Vector2f pos1 = obj1.getPosition() + move1;
            Vector2u size1 = obj1.getSize();
            Vector2f pos2 = obj2.getPosition() + move2;
            Vector2u size2 = obj2.getSize();

            return CheckCollision(pos1, size1, pos2, size2);

        }

        public static bool CheckCollision(Vector2f pos1, Vector2u size1, Vector2f pos2, Vector2u size2)
        {

            if (pos1.X < pos2.X + size2.X
                && pos1.X + size1.X > pos2.X
                && pos1.Y < pos2.Y + size2.Y
                && pos1.Y + size1.Y > pos2.Y)
                     return true;
            else
                return false;
        }
    }
}
