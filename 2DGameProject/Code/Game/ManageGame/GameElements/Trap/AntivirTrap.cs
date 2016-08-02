using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
/*
    Aufgabe der Klasse: Die Falle soll den Spieler, RedBot, BlueBot "ausschalten". nur der Grüne Bot kann über die Fallen laufen und sie entschaerfen!
    !!!!Wichtig! Die Falle wird "nicht" geloescht nur nicht mehr gezeichnet!!!!
*/
namespace MemoryMaze
{
    class AntivirTrap
    {
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.TrapTile));
        public Vector2i position;
        public Boolean isAlive;                                 //Ist meine Falle scharf?


        public AntivirTrap() { }
        public AntivirTrap(Vector2i _position, Map map)
        {
            isAlive = true;
            position = _position;
            sprite.Position = new Vector2f(position.X * map.sizePerCell, position.Y * map.sizePerCell );
            sprite.Scale = new Vector2f((float)map.sizePerCell  / (float)sprite.Texture.Size.X, (float)map.sizePerCell  / (float)sprite.Texture.Size.Y);


        }
        public AntivirTrap(AntivirTrap trap)                    //CopyKonstruktor
        {
            isAlive = true;
            position = trap.position;
            sprite.Position = trap.sprite.Position;

        }
        public AntivirTrap Copy()
        {
            return new AntivirTrap(this);
        }
        public void Update(Map map, float deltaTime)
        {

        }

        public void Draw(RenderWindow win, View view)
        {
            if(isAlive)                                 //Nur zeichnen wenn sie aktiv ist
                win.Draw(sprite);
        }
    }
}
