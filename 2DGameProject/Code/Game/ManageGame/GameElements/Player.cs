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

        public bool redbot, bluebot, greenbot, isAlive;                         //Existiert ein Bot?
        public int scoreCounter = 0;                                            //Punkte des Spielers
        public List<Bot> botList;                                               //aktuelle BotListe
        public int redItemCounter, blueItemCounter, greenItemCounter = 0;       //
        public int keyCounter { get; private set; } = 0;                        //
        public Vector2i mapPosition { get; set; }
        public int controllid { get; set; }                                     //Welchen Bot möchte ich steuern?

        Text playerdetected;
        int id;
        public bool ghostaktiv, iserstellt;                                  //Ghostaktiv (momentan existiert ein Ghost) |  iserstellt(Ghost wurde erstellt)
        public GhostPlayer ghostPlayer;
        RectangleShape sprite;
        List<Bot> deleteList;
        Vector2f currentFocus;

        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }


        // GUI Stuff
        Sprite playerStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Player)));
        Sprite EnergybarGhostPlayer = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.YellowEnergybar)));
        Sprite playerHUDBox = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.HUDYellowBox)));
        //
        Sprite redBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.RedBot)));
        Sprite EnergybarRed = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.RedEnergybar)));
        Sprite redBotHUDBox = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.HUDRedBox)));
        Sprite redBotReserveBot = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.RedItem)));
        Sprite blueBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.BlueBot)));
        Sprite EnergybarBlue = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.BlueEnergybar)));
        Sprite blueBotHUDBox = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.HUDBlueBox)));
        Sprite blueBotReserveBot = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.BlueItem)));
        Sprite greenBotStatus = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.GreenBot)));
        Sprite EnergybarGreen = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.GreenEnergybar)));
        Sprite greenBotHUDBox = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.HUDGreenBox)));
        Sprite greenBotReserveBot = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.GreenItem)));

        Font calibri = new Font("Assets/Fonts/fixedsys.ttf");
        Text guiGhostCounter, guiRedCounter, guiBlueCounter, guiGreenCounter;
        Text guiPlayerItemCounter, guiRedItemCounter, guiBlueItemCounter, guiGreenItemCounter;

        Sprite teleSprite;
        Vector2 teleSpritePos;
        float teleSpriteSpeed;
        bool teleporting;
        bool blueBotPorting;
        List<Vector2> teleporterWaypoints;

        int sizePerCell;

        // all variables initialized here need to be initialized in the copyconstructor too
        public Player(Vector2i position, Map map)
        {
            playerdetected = new Text("Virus entdeckt!", calibri);

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

            sizePerCell = map.GetSizePerCell();

            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(sizePerCell, sizePerCell);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Player);

            teleSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.TeleportBot));
            teleSprite.Origin = new Vector2f(teleSprite.Texture.Size.X * 0.5f, teleSprite.Texture.Size.Y * 0.5f);
            teleSpriteSpeed = 1500f;

            this.mapPosition = position;
            UpdateSpritePosition(map);

            InitializeGUI();
        }

        // Constructor for the Copy function
        Player(Vector2i position, RectangleShape _sprite, Sprite _teleSprite, float _teleSpriteSpeed,int _sizePerCell)
        {
            playerdetected = new Text("Virus detected!", calibri);
            id = 0;
            controllid = 0;
            deleteList = new List<Bot>();
            botList = new List<Bot>();
            isAlive = true;
            redbot = false;
            bluebot = false;
            greenbot = false;
            ghostaktiv = false;
            iserstellt = false;
            teleporting = false;

            sprite = _sprite;
            mapPosition = position;
            teleSprite = _teleSprite;
            teleSpriteSpeed = _teleSpriteSpeed;
            sizePerCell = _sizePerCell;

            InitializeGUI();
        }

        void InitializeGUI()
        {
            playerStatus.Position = new Vector2f(25, 25);
            EnergybarGhostPlayer.Position = playerStatus.Position + new Vector2f(50, 0);
            playerHUDBox.Position = playerStatus.Position - new Vector2f(3, 3);
            //playerStatus.Scale = new Vector2f(1, 1);
            redBotStatus.Position = new Vector2f(25, 125);
            EnergybarRed.Position = redBotStatus.Position + new Vector2f(50, 0);
            redBotHUDBox.Position = redBotStatus.Position - new Vector2f(3, 3);
            redBotReserveBot.Scale = new Vector2f(0.5F, 0.5F);
            redBotReserveBot.Position = redBotStatus.Position + new Vector2f(30, 58);
            //redBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            greenBotStatus.Position = new Vector2f(25, 225);
            EnergybarGreen.Position = greenBotStatus.Position + new Vector2f(50, 0);
            greenBotHUDBox.Position = greenBotStatus.Position - new Vector2f(3, 3);
            greenBotReserveBot.Scale = new Vector2f(0.5F, 0.5F);
            greenBotReserveBot.Position = greenBotStatus.Position + new Vector2f(30, 58);
            //greenBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);
            blueBotStatus.Position = new Vector2f(25, 325);
            EnergybarBlue.Position = blueBotStatus.Position + new Vector2f(50, 0);
            blueBotHUDBox.Position = blueBotStatus.Position - new Vector2f(3, 3);
            blueBotReserveBot.Scale = new Vector2f(0.5F, 0.5F);
            blueBotReserveBot.Position = blueBotStatus.Position + new Vector2f(30, 58);
            //blueBotStatus.Scale = new Vector2f(25f / 64f, 25f / 64f);


            // initialize text for stepcounter
            guiGhostCounter = new Text("0", calibri, 20);
            guiRedCounter = new Text("0", calibri, 20);
            guiGreenCounter = new Text("0", calibri, 20);
            guiBlueCounter = new Text("0", calibri, 20);

            // initialize text for itemcounter
            guiPlayerItemCounter = new Text("" + keyCounter, calibri, 20);
            guiRedItemCounter = new Text("" + redItemCounter , calibri, 20);
            guiGreenItemCounter = new Text("" + greenItemCounter, calibri, 20);
            guiBlueItemCounter = new Text("" + blueItemCounter, calibri, 20);

            guiGhostCounter.Position = new Vector2f(playerStatus.Position.X, playerStatus.Position.Y + (float)playerStatus.TextureRect.Height*playerStatus.Scale.Y);
            guiRedCounter.Position = new Vector2f(redBotStatus.Position.X, redBotStatus.Position.Y + (float)redBotStatus.TextureRect.Height*redBotStatus.Scale.Y);
            guiGreenCounter.Position = new Vector2f(greenBotStatus.Position.X, greenBotStatus.Position.Y + (float)greenBotStatus.TextureRect.Height*greenBotStatus.Scale.Y);
            guiBlueCounter.Position = new Vector2f(blueBotStatus.Position.X, blueBotStatus.Position.Y + (float)blueBotStatus.TextureRect.Height * blueBotStatus.Scale.Y);

            guiPlayerItemCounter.Position = new Vector2f(playerStatus.Position.X + (float)playerStatus.TextureRect.Width* playerStatus.Scale.X * 0.85f, playerStatus.Position.Y + (float)playerStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiRedItemCounter.Position = new Vector2f(redBotStatus.Position.X + (float)redBotStatus.TextureRect.Width * redBotStatus.Scale.X * 0.85f, redBotStatus.Position.Y + (float)redBotStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiGreenItemCounter.Position = new Vector2f(greenBotStatus.Position.X + (float)greenBotStatus.TextureRect.Width * greenBotStatus.Scale.X * 0.85f, greenBotStatus.Position.Y + (float)greenBotStatus.TextureRect.Height * playerStatus.Scale.Y);
            guiBlueItemCounter.Position = new Vector2f(blueBotStatus.Position.X + (float)blueBotStatus.TextureRect.Width * blueBotStatus.Scale.X * 0.85f, blueBotStatus.Position.Y + (float)blueBotStatus.TextureRect.Height * playerStatus.Scale.Y);
        }

        public Player Copy()
        {
            return new Player(mapPosition, sprite, teleSprite, teleSpriteSpeed, sizePerCell);
        }
        
        public void Update(float deltaTime, Map map)
        {
            if(teleporting)
            {
                UpdateBots(deltaTime, map, getListOfBotPositions());
                UpdateSpritePosition(map);
                Teleporting(deltaTime);
                currentFocus = teleSpritePos;
            }
            else if (isAlive == true)
            {
                if (controllid != 0)
                    SwitchToGhostPlayer();
                UpdateBots(deltaTime, map, getListOfBotPositions());

                if (KeyboardInputManager.Downward(Keyboard.Key.Space))
                    ghostaktiv = true;

                if (iserstellt)
                {
                    ghostPlayer.Update(deltaTime, map, this);
                    currentFocus = new Vector2(ghostPlayer.mapPosition.X * sizePerCell + sizePerCell * 0.5f, ghostPlayer.mapPosition.Y * sizePerCell + sizePerCell * 0.5f);
                    Console.WriteLine("after setting: " + currentFocus);
                    UpdateSpritePosition(map);
                }
                else
                {
                    if (id == controllid)
                    {
                        Vector2i move = GetMove();
                        if (map.CellIsWalkable(mapPosition + move))
                        {
                            mapPosition = mapPosition + move;
                            if(move.X != 0 || move.Y != 0) //ProSchritt wird der Counter um 1 erhöht
                                scoreCounter++;
                        }
                        else if (map.MoveIsPossible(mapPosition, move, this.getListOfBotPositions()))
                        {
                            map.MoveBlock(mapPosition, move);
                            mapPosition = mapPosition + move;
                            scoreCounter++;
                        }
                        else
                        {
                            MusicManager.PlaySound(AssetManager.SoundName.Wall);
                        }
                        UpdateSpritePosition(map);
                        currentFocus = sprite.Position + new Vector2f(sprite.Size.X / 2f, sprite.Size.Y / 2f);
                    }
                }
                //Create GhostPlayer
                if (ghostaktiv && (!iserstellt) && controllid == 0)
                {
                    ghostPlayer = new GhostPlayer(mapPosition, map);
                    iserstellt = true;
                }
                if (!ghostaktiv)
                {
                    ghostPlayer = null;
                    iserstellt = false;
                }
                //Destroy GhostPlayer
                else if (KeyboardInputManager.Upward(Keyboard.Key.Space) || iserstellt && ghostPlayer.GetCount() == 0)
                {
                    ghostPlayer = null;
                    ghostaktiv = false;
                    iserstellt = false;
                }
        
                //Target controll manager
                SwitchTarget();
                UpdateSpritePosition(map);
            }
            else
            {//SPieler ist tod
                MusicManager.PlaySound(AssetManager.SoundName.VirusDetected);
                playerdetected.Position = new Vector2f(300, 200);
                playerdetected.Scale = new Vector2f(4, 4);
                playerdetected.Color = Color.Red;
                playerdetected.Style = Text.Styles.Bold;
            }
            
        }

        public bool Teleporting(float deltaTime)
        {
            if(teleporterWaypoints.Count > 0)
            {
                Vector2 moveVec = teleporterWaypoints[0] - teleSpritePos;
                if(moveVec.length != 0)
                    moveVec = moveVec.normalize() * teleSpriteSpeed * deltaTime;
                if (moveVec.length < (teleporterWaypoints[0] - teleSpritePos).length)
                    teleSpritePos += moveVec;
                else
                {
                    teleSpritePos = teleporterWaypoints[0];
                    teleporterWaypoints.RemoveAt(0);
                }
                return true;
            }
            else
            {
                if (blueBotPorting)
                    GraphicHelper.SetAlpha(255, botList.Find(b => b.id == 2).sprite);
                else
                    GraphicHelper.SetAlpha(255, sprite);
                teleporting = false;
                return false;
            }
        }

        public void InitializeTeleport(Transporter porter, bool _blueBotPorting, Vector2i target)
        {
            teleporterWaypoints = new List<Vector2>();
            teleporting = true;
            blueBotPorting = _blueBotPorting;
            Vector2i startPos = (blueBotPorting) ? botList.Find(b => b.id == 2).mapPosition : mapPosition;
            teleSpritePos = new Vector2(startPos.X * sizePerCell + 0.5f * sizePerCell, startPos.Y * sizePerCell + sizePerCell * 0.5f);
            if (blueBotPorting)
                GraphicHelper.SetAlpha(0, botList.Find(b => b.id == 2).sprite);
            else
                GraphicHelper.SetAlpha(0, sprite);
            if (Math.Abs(target.X - startPos.X) > Math.Abs(target.Y - startPos.Y))
                teleporterWaypoints.Add(new Vector2(target.X * sizePerCell + sizePerCell * 0.5f, startPos.Y * sizePerCell + sizePerCell * 0.5f));
            else
                teleporterWaypoints.Add(new Vector2(startPos.X * sizePerCell + sizePerCell * 0.5f, target.Y * sizePerCell + sizePerCell * 0.5f));
            teleporterWaypoints.Add(new Vector2(target.X * sizePerCell + sizePerCell * 0.5f, target.Y * sizePerCell + sizePerCell * 0.5f));
            MusicManager.PlaySound(AssetManager.SoundName.Teleport);
        }
 
        public void Draw(RenderTexture win, View view, Vector2f relViewDis, float deltaTime)
        {
            view.Center = Vector2.lerp(view.Center, currentFocus, deltaTime*3);
            
            if (teleporting)
            {
                teleSprite.Position = teleSpritePos + (Vector2)relViewDis;
                win.Draw(teleSprite);
            }

            sprite.Position += relViewDis;
            if (isAlive)
                win.Draw(sprite);
            if (iserstellt)
                ghostPlayer.Draw(win, view, relViewDis);

            foreach (Bot it in botList)
            {
                if(it != null)
                    it.Render(win, view, relViewDis);
            }


        }

        private void SwitchTarget()
        {
            if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num2)) && redbot)
                controllid = 1;

            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num4)) && bluebot)
                controllid = 2;

            else if (!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num3)) && greenbot)
                controllid = 3;

            else
            {
                if ((!(KeyboardInputManager.IsPressed(Keyboard.Key.Space)) && (KeyboardInputManager.Downward(Keyboard.Key.Num1))))
                    controllid = 0;
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            if (!isAlive)
                gui.Draw(playerdetected);

            Color low = new Color(255, 255, 255, 127);
            Color high = new Color(255, 255, 255, 255);

            playerStatus.Color = low;
            redBotStatus.Color = low;
            blueBotStatus.Color = low;
            greenBotStatus.Color = low;

            // highlighting active player
            switch (controllid)
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

            // HUD Boxes
            playerHUDBox.Color = playerStatus.Color;
            redBotHUDBox.Color = redBotStatus.Color;
            greenBotHUDBox.Color = greenBotStatus.Color;
            blueBotHUDBox.Color = blueBotStatus.Color;
            gui.Draw(playerHUDBox);
            gui.Draw(redBotHUDBox);
            gui.Draw(greenBotHUDBox);
            gui.Draw(blueBotHUDBox);

            updateTexts();
            // EnergyBars (how many steps are left)
            for (int i = 0; i <= 3; i++)
            {
                drawEnergybars(i, gui);
            }

            // Reserve Bots
            DrawReserveBots(gui, redItemCounter, redBotReserveBot);
            DrawReserveBots(gui, greenItemCounter, greenBotReserveBot);
            DrawReserveBots(gui, blueItemCounter, blueBotReserveBot);
            /*
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
            */
            gui.Draw(playerStatus);
            gui.Draw(redBotStatus);
            gui.Draw(blueBotStatus);
            gui.Draw(greenBotStatus);

        }

        private static void DrawReserveBots(GUI gui, int itemCounter, Sprite reserveBot)
        {
            Vector2 tmpPosition = reserveBot.Position;
            for (int i = 0; i < itemCounter; i++)
            {
                gui.Draw(reserveBot);
                reserveBot.Position = reserveBot.Position + new Vector2f(reserveBot.Scale.X * reserveBot.TextureRect.Width, 0);
            }
            reserveBot.Position = tmpPosition;
        }

        private void drawEnergybars(int id, GUI gui)
        {
            Sprite barSprite;
            int numBars;
            switch (id)
            {
                case 0:
                    barSprite = EnergybarGhostPlayer;
                    barSprite.Color = playerStatus.Color;
                    numBars = ghostaktiv ? ghostPlayer.counter : 0;
                    break;
                case 1:
                    barSprite = EnergybarRed;
                    barSprite.Color = redBotStatus.Color;
                    numBars = redbot ? botList.Find(b => b.id == 1).counter : 0;
                    break;
                case 2:
                    barSprite = EnergybarBlue;
                    barSprite.Color = blueBotStatus.Color;
                    numBars = bluebot ? botList.Find(b => b.id == 2).counter : 0;
                    break;
                case 3:
                    barSprite = EnergybarGreen;
                    barSprite.Color = greenBotStatus.Color;
                    numBars = greenbot ? botList.Find(b => b.id == 3).counter : 0;
                    break;
                default:
                    throw new Exception("this should not have happened");
            }
            Vector2 tmpPosition = barSprite.Position;
            for(int i = 0; i < numBars; i++)
            {
                if (i % 3 == 0)
                {
                    barSprite.Scale = new Vector2f(1, 1.1F);
                    gui.Draw(barSprite);
                    barSprite.Scale = new Vector2f(1, 1);
                }
                gui.Draw(barSprite);
                barSprite.Position += new Vector2f(7,0);
            }
            barSprite.Position = tmpPosition;
        }


        private void updateTexts()
        {
            // updating Text
            if (iserstellt)
                guiGhostCounter.DisplayedString = "" + ghostPlayer.counter;
            else
                guiGhostCounter.DisplayedString = "" + 0;

            if (redbot)
                guiRedCounter.DisplayedString = "" + botList.Find(b => b.id == 1).counter;

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
            if (KeyboardInputManager.Downward(Keyboard.Key.W) || KeyboardInputManager.Downward(Keyboard.Key.Up))
                move.Y = -1;


            else if (KeyboardInputManager.Downward(Keyboard.Key.S) || KeyboardInputManager.Downward(Keyboard.Key.Down))
                move.Y = 1;

            else  if (KeyboardInputManager.Downward(Keyboard.Key.A) || KeyboardInputManager.Downward(Keyboard.Key.Left))
                move.X = -1;

            else if (KeyboardInputManager.Downward(Keyboard.Key.D) || KeyboardInputManager.Downward(Keyboard.Key.Right))
                move.X = 1;

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
                        deleteList.Add(it); //zerstoere Bot
                        Console.WriteLine("Some Bot is getting destroyed");
                        MusicManager.PlaySound(AssetManager.SoundName.DeleteBot);
                        if (it.id == controllid)
                            controllid = 0;
                    }
                    else
                    {
                        if (controllid == it.id)                                                   //Setzt den Focus auf aktuellen Bot
                            currentFocus = it.sprite.Position + new Vector2f(it.sprite.Size.X / 2f, it.sprite.Size.Y / 2f);
                    }
                    Check(it); //Überprüft ob Red,Blue, Green in der Liste ist
                    DestroyBotForPoints(it);
                }
                botList.RemoveAll(deleteList.Contains);
                deleteList = new List<Bot>();
        }
            

        void DestroyBotForPoints(Bot it)
        {
            if (KeyboardInputManager.IsPressed(Keyboard.Key.R) && (controllid == 1) && it == botList.Find(b => b.id == 1))
            {
                scoreCounter += it.counter * (-1);
                it.isAlive = false;
            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.R) && (controllid == 2) && it == botList.Find(b => b.id == 2))
            {
                scoreCounter += it.counter * (-1);
                it.isAlive = false;
            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.R) && (controllid == 3) && it == botList.Find(b => b.id == 3))
            {
                scoreCounter += it.counter * (-1);
                it.isAlive = false;
            }
        }
        void SwitchToGhostPlayer()
        {
            if (KeyboardInputManager.Downward(Keyboard.Key.Space)){
                controllid = 0;
            }
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
            {
                keyCounter++;
                MusicManager.PlaySound(AssetManager.SoundName.Key);
            }
                
            if (item is RedItem)
            {
                redItemCounter++;
                MusicManager.PlaySound(AssetManager.SoundName.ItemPick);
            }

            if (item is BlueItem)
            {
                blueItemCounter++;
                MusicManager.PlaySound(AssetManager.SoundName.ItemPick);

            }

            if (item is GreenItem)
            {
                greenItemCounter++;
                MusicManager.PlaySound(AssetManager.SoundName.ItemPick);
            }

            if (item is ScoreItem)
                scoreCounter= scoreCounter -5;
                 
        }

        public List<Vector2i> getListOfBotPositions()
        {
            List<Vector2i> result = new List<Vector2i>();
            result.Add(mapPosition);
            foreach (Bot bot in botList)
                result.Add(bot.mapPosition);
            return result;
        }

        public List<Vector2i> getListWithPlayerAndBlueBot()
        {
            List<Vector2i> result = new List<Vector2i>();
            result.Add(mapPosition);
            foreach(Bot bot in botList)
            {
                if (bot.id == 2)
                    result.Add(bot.mapPosition);
            }
            return result;
        }

        public void setBlueBotPosition(Vector2i newPosition)
        {
            if(bluebot)
            {
                botList.Find(b => b.id == 2).mapPosition = newPosition;
            }
            else
            {
                Logger.Instance.Write("Bluebot does not exist at the moment", Logger.level.Error);
            }

        }

        public int addScoreFromBots()
        {
            int result = 0;
            foreach (Bot b in botList)
            {
                if (b != null)
                {
                    result -= b.counter;
                }
            }
            return result;
        }
    }
}
