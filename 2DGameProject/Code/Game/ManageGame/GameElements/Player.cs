using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public class Player
    {
        bool ghostaktiv;
        bool iserstellt;
        int controllid;
        int id;
        bool redbot;
        bool bluebot;
        bool greenbot;
        GhostPlayer ghostPlayer;
        RectangleShape sprite;
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }

        public List<Bot> botList;
        List<Bot> deleteList;

        // GUI Stuff
        Sprite playerStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Player)));
        Sprite redBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.RedBot)));
        Sprite blueBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.BlueBot)));
        Sprite greenBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.GreenBot)));
        Font calibri = new Font("Assets/Fonts/calibri.ttf");
        Text ghostCounter;
        Text redCounter;
        Text blueCounter;
        Text greenCounter;

        // all variables initialized here need to be initialized in the copyconstructor too
        public Player(Vector2i position, Map map)
        {
            id = 0;
            controllid = 0;
            deleteList = new List<Bot>();
            botList = new List<Bot>();

            redbot = false;
            bluebot = false;
            greenbot = false;
            ghostaktiv = false;
            iserstellt = false;

            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell() * 0.8F, map.GetSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Player);

            this.mapPosition = position;
            UpdateSpritePosition(map);

            InitializeGUI();
        }

        // Constructor for the Copy function
        Player(Vector2i position, RectangleShape _sprite)
        {
            deleteList = new List<Bot>();
            botList = new List<Bot>();

            redbot = false;
            bluebot = false;
            greenbot = false;
            ghostaktiv = false;
            iserstellt = false;

            sprite = _sprite;
            mapPosition = position;

            InitializeGUI();
        }

        void InitializeGUI()
        {
            playerStatus.Position = new Vector2f(25, 25);
            playerStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            redBotStatus.Position = new Vector2f(125, 25);
            redBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            blueBotStatus.Position = new Vector2f(225, 25);
            blueBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            greenBotStatus.Position = new Vector2f(325, 25);
            greenBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);

            ghostCounter = new Text("0", calibri, 20);
            redCounter = new Text("0", calibri, 20);
            blueCounter = new Text("0", calibri, 20);
            greenCounter = new Text("0", calibri, 20);

            ghostCounter.Position = new Vector2f(playerStatus.Position.X, playerStatus.Position.Y + (float)playerStatus.TextureRect.Height*playerStatus.Scale.Y);
            redCounter.Position = new Vector2f(redBotStatus.Position.X, redBotStatus.Position.Y + (float)redBotStatus.TextureRect.Height*redBotStatus.Scale.Y);
            blueCounter.Position = new Vector2f(blueBotStatus.Position.X, blueBotStatus.Position.Y + (float)blueBotStatus.TextureRect.Height*blueBotStatus.Scale.Y);
            greenCounter.Position = new Vector2f(greenBotStatus.Position.X, greenBotStatus.Position.Y + (float)greenBotStatus.TextureRect.Height*greenBotStatus.Scale.Y);
        }

        public Player Copy()
        {
            return new Player(mapPosition, sprite);
        }
        
        public void Update(float deltaTime, Map map)
        {
            UpdateBots(deltaTime, map);
           
            if (KeyboardInputManager.Downward(Keyboard.Key.LControl)) //ToDo: Bedingungen um GhostPlayer zu aktivieren
            {
                ghostaktiv = true;
               
            }
            if (iserstellt)
                ghostPlayer.Update(deltaTime, map, botList, redbot, bluebot, greenbot);

            else {
                if(id == controllid)
                {

                    Vector2i move = GetMove();
                    if (map.CellIsWalkable(mapPosition + move))
                    {
                        mapPosition = mapPosition + move;
                        //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                        UpdateSpritePosition(map);
                    }
                    else if (map.MoveIsPossible(mapPosition, move))
                    {
                        //Logger.Instance.Write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                        map.MoveBlock(mapPosition, move);
                        mapPosition = mapPosition + move;
                    }
                }
            }
            //Create GhostPlayer
            if (ghostaktiv && (!iserstellt) && controllid == 0)
            {
                ghostPlayer = new GhostPlayer(mapPosition, map);
                iserstellt = true;
            }
            //Destroy GhostPlayer
            if (KeyboardInputManager.Upward(Keyboard.Key.LControl) || iserstellt && ghostPlayer.GetCount() == 0)
            {
                ghostPlayer = null;
                ghostaktiv = false;
                iserstellt = false;
            }
            //Target controll manager
            SwitchTarget();
        }
 
        public void Draw(RenderWindow win, View view)
        {
            view.Center = Vector2.lerp(view.Center, sprite.Position, 0.01F);
            win.Draw(sprite);
            if (iserstellt)
                ghostPlayer.Draw(win, view);

            foreach (Bot it in botList)
            {
                if(it != null)
                    it.Render(win);
            }
        }


        private void SwitchTarget()
        {
            if (!(KeyboardInputManager.IsPressed(Keyboard.Key.LControl)) && (KeyboardInputManager.Downward(Keyboard.Key.Num1)) && redbot)
            {
                controllid = 1;
            }
            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.LControl)) && (KeyboardInputManager.Downward(Keyboard.Key.Num2)) && bluebot)
            {
                controllid = 2;
            }
            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.LControl)) && (KeyboardInputManager.Downward(Keyboard.Key.Num3)) && greenbot)
            {
                controllid = 3;
            }
            else
            {

                if ((!(KeyboardInputManager.IsPressed(Keyboard.Key.LControl)) && (KeyboardInputManager.Downward(Keyboard.Key.Space))))
                    controllid = 0;
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            Color low = new Color(255, 255, 255, 127);
            Color high = new Color(255, 255, 255, 255);

            playerStatus.Color = low;
            redBotStatus.Color = low;
            blueBotStatus.Color = low;
            greenBotStatus.Color = low;

            // highlighting active player
            switch(controllid)
            {
                case 0:
                    playerStatus.Color = high; break;
                case 1:
                    redBotStatus.Color = high; break;
                case 2:
                    blueBotStatus.Color = high; break;
                case 3:
                    greenBotStatus.Color = high; break;
                default: break;
            }

            // updating Text
            if (iserstellt) ghostCounter.DisplayedString = "" + ghostPlayer.counter;
            else ghostCounter.DisplayedString = "" + 0;
            if (redbot) redCounter.DisplayedString = "" + botList.Find(b => b.id == 1).counter;
            else redCounter.DisplayedString = "" + 0;
            if (bluebot) blueCounter.DisplayedString = "" + botList.Find(b => b.id == 2).counter;
            else blueCounter.DisplayedString = "" + 0;
            if (greenbot) greenCounter.DisplayedString = "" + botList.Find(b => b.id == 3).counter;
            else greenCounter.DisplayedString = "" + 0;

            // printing current steps;
            gui.Draw(ghostCounter);
            gui.Draw(redCounter);
            gui.Draw(blueCounter);
            gui.Draw(greenCounter);

            gui.Draw(playerStatus);
            gui.Draw(redBotStatus);
            gui.Draw(blueBotStatus);
            gui.Draw(greenBotStatus);

        }

        Vector2i GetMove()
        {
            Vector2i move = new Vector2i(0, 0);
            if (KeyboardInputManager.Downward(Keyboard.Key.Up))
            {
                move.Y = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Down))
            {
                move.Y = 1;
            }
            else  if (KeyboardInputManager.Downward(Keyboard.Key.Left))
            {
                move.X = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Right))
            {
                move.X = 1;
            }
            return move;
        }

        Vector2i MakeNextPos(Vector2i nextPos, Map map)
        {
            nextPos.X = (nextPos.X > map.mapSizeX) ? map.mapSizeX : nextPos.X;
            nextPos.Y = (nextPos.Y > map.mapSizeY) ? map.mapSizeY : nextPos.Y;

            nextPos.X = (nextPos.X < 0) ? 0 : nextPos.X;
            nextPos.Y = (nextPos.Y < 0) ? 0 : nextPos.Y;
            return nextPos;
        }

        void UpdateSpritePosition(Map map)
        {
            this.sprite.Position = new Vector2f(mapPosition.X * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F, mapPosition.Y * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F);
        }

        Vector2f GetSpritePosition(Map map)
        {
            return new Vector2f(mapPosition.X * map.GetSizePerCell() + map.GetSizePerCell()*0.1F, mapPosition.Y * map.GetSizePerCell() + map.GetSizePerCell()*0.1F);
        }

        private void UpdateBots(float deltaTime, Map map)
        {
            foreach (Bot it in botList)
            {
                //Logger.Instance.Write("conntrollid: " + controllid, Logger.level.Info);
                it.Update(deltaTime, map, controllid);
                if (!it.isAlive)
                {
                    deleteList.Add(it); //´zerstoere Player
                    if (it.id == controllid)
                        controllid = 0;
                }
                Check(it); //Überprüft ob Red,Blue, Green in der Liste ist
            }
            botList.RemoveAll(deleteList.Contains);
            deleteList = new List<Bot>();
        }

        private void Check(Bot bot)
        {
            if(bot.id == 1)
            {
                if (bot.isAlive)
                    redbot = true;
                else                    
                    redbot = false;
            }
            else if(bot.id == 2)
            {
                if (bot.isAlive)
                    bluebot = true;
                else
                    bluebot = false;
            }

            else if(bot.id == 3)
            {
                if (bot.isAlive)
                    greenbot = true;
                else
                    greenbot = false;
            }
        }
    }
}
