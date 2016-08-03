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
        public int controllid {get; private set; }
        int id;
        public bool redbot;
        public bool bluebot;
        public bool greenbot;
        public bool isAlive;
        GhostPlayer ghostPlayer;
        RectangleShape sprite;
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        Vector2f currentFocus;

        public List<Bot> botList;
        List<Bot> deleteList;

        public int scoreCounter = 0;
        public int keyCounter { get; private set; } = 0;
        public int redItemCounter = 0;
        public int blueItemCounter = 0;
        public int greenItemCounter = 0;

        // GUI Stuff
        Sprite playerStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Player)));
        Sprite redBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.RedBot)));
        Sprite blueBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.BlueBot)));
        Sprite greenBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.GreenBot)));
        Font calibri = new Font("Assets/Fonts/calibri.ttf");
        Text guiGhostCounter;
        Text guiRedCounter;
        Text guiBlueCounter;
        Text guiGreenCounter;

        Text guiPlayerItemCounter;
        Text guiRedItemCounter;
        Text guiBlueItemCounter;
        Text guiGreenItemCounter;


        // all variables initialized here need to be initialized in the copyconstructor too
        public Player(Vector2i position, Map map)
        {
            id = 0;
            isAlive = true;
            controllid = 0;
            deleteList = new List<Bot>();
            botList = new List<Bot>();

            redbot = false;
            bluebot = false;
            greenbot = false;
            ghostaktiv = false;
            iserstellt = false;

            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell(), map.GetSizePerCell());
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
            isAlive = true;
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
            //playerStatus.Scale = new Vector2f(1, 1);
            redBotStatus.Position = new Vector2f(125, 25);
            //redBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            blueBotStatus.Position = new Vector2f(225, 25);
            //blueBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            greenBotStatus.Position = new Vector2f(325, 25);
            //greenBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);

            // initialize text for stepcounter
            guiGhostCounter = new Text("0", calibri, 20);
            guiRedCounter = new Text("0", calibri, 20);
            guiBlueCounter = new Text("0", calibri, 20);
            guiGreenCounter = new Text("0", calibri, 20);
            // initialize text for itemcounter
            guiPlayerItemCounter = new Text("" + keyCounter, calibri, 20);
            guiRedItemCounter = new Text("" + redItemCounter , calibri, 20);
            guiBlueItemCounter = new Text("" +blueItemCounter, calibri, 20);
            guiGreenItemCounter = new Text("" + greenItemCounter, calibri, 20);

            guiGhostCounter.Position = new Vector2f(playerStatus.Position.X, playerStatus.Position.Y + (float)playerStatus.TextureRect.Height*playerStatus.Scale.Y);
            guiRedCounter.Position = new Vector2f(redBotStatus.Position.X, redBotStatus.Position.Y + (float)redBotStatus.TextureRect.Height*redBotStatus.Scale.Y);
            guiBlueCounter.Position = new Vector2f(blueBotStatus.Position.X, blueBotStatus.Position.Y + (float)blueBotStatus.TextureRect.Height*blueBotStatus.Scale.Y);
            guiGreenCounter.Position = new Vector2f(greenBotStatus.Position.X, greenBotStatus.Position.Y + (float)greenBotStatus.TextureRect.Height*greenBotStatus.Scale.Y);

            guiPlayerItemCounter.Position = new Vector2f(playerStatus.Position.X + (float)playerStatus.TextureRect.Width* playerStatus.Scale.X * 0.85f, playerStatus.Position.Y + (float)playerStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiRedItemCounter.Position = new Vector2f(redBotStatus.Position.X + (float)redBotStatus.TextureRect.Width * redBotStatus.Scale.X * 0.85f, redBotStatus.Position.Y + (float)redBotStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiBlueItemCounter.Position = new Vector2f(blueBotStatus.Position.X + (float)blueBotStatus.TextureRect.Width * blueBotStatus.Scale.X * 0.85f, blueBotStatus.Position.Y + (float)blueBotStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiGreenItemCounter.Position = new Vector2f(greenBotStatus.Position.X + (float)greenBotStatus.TextureRect.Width * greenBotStatus.Scale.X * 0.85f, greenBotStatus.Position.Y + (float)greenBotStatus.TextureRect.Height * playerStatus.Scale.Y);

        }

        public Player Copy()
        {
            return new Player(mapPosition, sprite);
        }
        
        public void Update(float deltaTime, Map map)
        {
            if (isAlive == true)
            {
                UpdateBots(deltaTime, map, getListOfBotPositions());

                if (KeyboardInputManager.Downward(Keyboard.Key.Space))
                {
                    ghostaktiv = true;

                }
                if (iserstellt)
                {
                    ghostPlayer.Update(deltaTime, map, this);
                    currentFocus = ghostPlayer.sprite.Position + new Vector2f(ghostPlayer.sprite.Size.X / 2f, ghostPlayer.sprite.Size.Y / 2f);
                }
                else
                {
                    if (id == controllid)
                    {

                        Vector2i move = GetMove();
                        if (map.CellIsWalkable(mapPosition + move))
                        {
                            mapPosition = mapPosition + move;
                            //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                            UpdateSpritePosition(map);
                        }
                        else if (map.MoveIsPossible(mapPosition, move, this.getListOfBotPositions()))
                        {
                            //Logger.Instance.Write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                            map.MoveBlock(mapPosition, move);
                            mapPosition = mapPosition + move;
                        }
                        currentFocus = sprite.Position + new Vector2f(sprite.Size.X / 2f, sprite.Size.Y / 2f);
                    }
                }
                //Create GhostPlayer
                if (ghostaktiv && (!iserstellt) && controllid == 0)
                {
                    ghostPlayer = new GhostPlayer(mapPosition, map);
                    iserstellt = true;
                }
                //Destroy GhostPlayer
                if (KeyboardInputManager.Upward(Keyboard.Key.Space) || iserstellt && ghostPlayer.GetCount() == 0)
                {
                    ghostPlayer = null;
                    ghostaktiv = false;
                    iserstellt = false;
                }
                //Target controll manager
                SwitchTarget();
            }
        }
 
        public void Draw(RenderTexture win, View view)
        {
            view.Center = Vector2.lerp(view.Center, currentFocus, 0.025F);
            if(isAlive)
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
            if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num2)) && redbot)
            {
                controllid = 1;
            }
            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num3)) && bluebot)
            {
                controllid = 2;
            }
            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num4)) && greenbot)
            {
                controllid = 3;
            }
            else
            {

                if ((!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num1))))
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

            updateTexts();

            // printing current steps;
            gui.Draw(guiGhostCounter);
            gui.Draw(guiRedCounter);
            gui.Draw(guiBlueCounter);
            gui.Draw(guiGreenCounter);
            // printing current ItemCounter
            gui.Draw(guiPlayerItemCounter);
            gui.Draw(guiRedItemCounter);
            gui.Draw(guiBlueItemCounter);
            gui.Draw(guiGreenItemCounter);

            gui.Draw(playerStatus);
            gui.Draw(redBotStatus);
            gui.Draw(blueBotStatus);
            gui.Draw(greenBotStatus);

        }

        private void updateTexts()
        {
            // updating Text
            if (iserstellt)
                guiGhostCounter.DisplayedString = "" + ghostPlayer.counter;
            else
                guiGhostCounter.DisplayedString = "" + 0;

            if (redbot)
            {
                guiRedCounter.DisplayedString = "" + botList.Find(b => b.id == 1).counter;
            }


            else
                guiRedCounter.DisplayedString = "" + 0;

            if (bluebot)
                guiBlueCounter.DisplayedString = "" + botList.Find(b => b.id == 2).counter;
            else
                guiBlueCounter.DisplayedString = "" + 0;

            if (greenbot)
                guiGreenCounter.DisplayedString = "" + botList.Find(b => b.id == 3).counter;
            else
                guiGreenCounter.DisplayedString = "" + 0;

            guiPlayerItemCounter.DisplayedString = "" + keyCounter;
            guiRedItemCounter.DisplayedString = "" + redItemCounter;
            guiBlueItemCounter.DisplayedString = "" + blueItemCounter;
            guiGreenItemCounter.DisplayedString = "" + greenItemCounter;
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
            this.sprite.Position = new Vector2f(mapPosition.X * map.GetSizePerCell(), mapPosition.Y * map.GetSizePerCell());
        }

        Vector2f GetSpritePosition(Map map)
        {
            return new Vector2f(mapPosition.X * map.GetSizePerCell() + map.GetSizePerCell()*0.1F, mapPosition.Y * map.GetSizePerCell() + map.GetSizePerCell()*0.1F);
        }

        private void UpdateBots(float deltaTime, Map map, List<Vector2i> botPosList)
        {
            foreach (Bot it in botList)
            {
                it.Update(deltaTime, map, controllid, botPosList);
                if (!it.isAlive)
                {
                    deleteList.Add(it); //´zerstoere Player
                    if (it.id == controllid)
                        controllid = 0;
                }
                else
                {
                    if (controllid == it.id)
                        currentFocus = it.sprite.Position + new Vector2f(it.sprite.Size.X/2f, it.sprite.Size.Y/2f);
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

        public void collectItem(Item item)
        {
            if (item is Key)
                keyCounter++;
            if (item is RedItem)
                redItemCounter++;
            if (item is BlueItem)
                blueItemCounter++;
            if (item is GreenItem)
                greenItemCounter++;
            if (item is ScoreItem)
                scoreCounter= scoreCounter +10;
                 
        }

        public List<Vector2i> getListOfBotPositions()
        {
            List<Vector2i> result = new List<Vector2i>();
            result.Add(mapPosition);
            foreach (Bot bot in botList)
                result.Add(bot.mapPosition);
            return result;
        }
    }
}
