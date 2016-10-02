using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace MemoryMaze
{
    class LoadLevelState : IGameState
    {
        Font font;
        Text playOn, levelSelect, control, back;
        Sprite background;

        List<Text> textlist;
        List<IntRect> rectList;
        Stopwatch stopwatch;

        RectangleShape debugRect;
        RectangleShape debugButtonRect;

        RectangleShape mainMap;
        RectangleShape helpMap;
        Vector2f slideOffscreenStartPosition;
        Vector2f slideEndPosition;
        Vector2f slideOffsrceenEndPosition;
        float slideSpeed;
        bool sliding;

        public LoadLevelState()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("LOADLEVELSTATE");
            Initialisation();
            //background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
        }

        void Initialisation()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            rectList = new List<IntRect>();

            rectList.Add(new IntRect(200, 600, 80, 80));   //Left           0
            rectList.Add(new IntRect(600, 600, 80, 80));   //Levels         1
            rectList.Add(new IntRect(900, 600, 80, 80));   //Settings       2   
            rectList.Add(new IntRect(1000, 600, 80, 80));  //Right          3   

            debugRect = new RectangleShape();
            debugButtonRect = new RectangleShape();
            mainMap = new RectangleShape(new Vector2f(980, 500));
            mainMap.Position = new Vector2f(150, 50);
            mainMap.FillColor = Color.Red;
            helpMap = new RectangleShape(mainMap);
            helpMap.Position = new Vector2f(-1000, -1000);
            slideSpeed = 10;
            sliding = false;

            //Initialisieren von  Text

            playOn = new Text("Continue Game ", font);
            playOn.Position = new Vector2f(250, 400);
            playOn.CharacterSize = 40;

            levelSelect = new Text("Levels", font);
            levelSelect.Position = new Vector2f(250, 450);
            levelSelect.CharacterSize = 40;

            control = new Text("Settings", font);
            control.Position = new Vector2f(250, 500);
            control.CharacterSize = 40;

            back = new Text("Back", font);
            back.Position = new Vector2f(250, 550);
            back.CharacterSize = 40;

            Text[] array = { playOn, levelSelect, control, back};
            textlist = array.ToList();

        }

        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)                          //Ist die Maus über ein IntRect
        {
            Vector2i mouse = win.InternalGetMousePosition();
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }

        public void LoadContent()
        {

        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            int index = -1;
            debugRect.Position = new Vector2f(-1000, -1000);
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    return GameState.MainMenu;
                if (sliding)
                {
                    SlideMap(deltaTime);
                    return GameState.LoadLevelState;
                }
                for (int e = 0; e < 4; e++)
                {   
                    if (IsMouseInRectangle(rectList[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                                                                                    //
                        case 0:
                            InitiateSlide(false);
                            return GameState.LoadLevelState;
                        //
                        case 1: return GameState.ChooseLevelState;
                        //
                        case 2: return GameState.Steuerung;
                        //Choose ur saveslot
                        case 3:
                            InitiateSlide(true);
                            return GameState.LoadLevelState;

                    }
                }
                else
                {
                    if (index != -1)
                    {
                        textlist[index].Color = Color.Blue;
                        IntRect curRect = rectList[index];
                        debugRect.Position = new Vector2f(curRect.Left, curRect.Top);
                        debugRect.Size = new Vector2f(curRect.Width, curRect.Height);
                        debugRect.FillColor = Color.Cyan;
                    }
                }
            }
            return GameState.LoadLevelState;
        }

        /// <summary>
        /// Initializes the Sliding of the map
        /// </summary>
        /// <param name="right">true -> right; false -> left</param>
        public void InitiateSlide(bool right)
        {
            sliding = true;
            slideEndPosition = mainMap.Position;
            if (right)
            {
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
            }
            else
            {
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
            }
            helpMap.Position = slideOffscreenStartPosition;
        }

        public void SlideMap(float deltaTime)
        {
            mainMap.Position = Vector2.lerp(mainMap.Position, slideOffsrceenEndPosition, slideSpeed * deltaTime);
            helpMap.Position = Vector2.lerp(helpMap.Position, slideEndPosition,  slideSpeed * deltaTime);
            if (Math.Abs(Vector2.distance( helpMap.Position, slideEndPosition)) <= 0.8f)
            {
                sliding = false;
                mainMap.Position = slideEndPosition;
                helpMap.Position = slideOffscreenStartPosition;
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            foreach (Text txt in textlist)
            {
                gui.Draw(txt);
                txt.Color = Color.White;
            }
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            win.Clear(Color.Green);
            win.Draw(mainMap);
            win.Draw(helpMap);
            foreach(IntRect ir in rectList)
            {
                debugButtonRect.Position = new Vector2f(ir.Left, ir.Top);
                debugButtonRect.Size = new Vector2f(ir.Width, ir.Height);
                debugButtonRect.FillColor = Color.Black;
                win.Draw(debugButtonRect);
            }
            win.Draw(debugRect);
        }

    }
}
