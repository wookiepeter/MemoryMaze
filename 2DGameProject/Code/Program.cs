using SFML;
using SFML.Graphics;
using SFML.Window;
using System;


namespace MemoryMaze
{
    class Program
    {
        public static GameTime GameTime;
        public static Logger logger;

        static bool running = true;

        static GameState currentGameState = GameState.MainMenu;
        static GameState prevGameState = GameState.MainMenu;

        static IGameState state;

        static RenderWindow win;
        static readonly Vector2 windowSize = new Vector2(1280, 720);
        static View view;
        static GUI gui;

        

        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode((uint)windowSize.X, (uint)windowSize.Y), "Hadoken!!!");
            view = new View();
            ResetView();
            gui = new GUI(win, view);

            // prevent window resizing
            win.Resized += (sender, e) => { (sender as Window).Size = windowSize; };

            // exit Program, when window is being closed
            //win.Closed += new EventHandler(closeWindow);
            win.Closed += (sender, e) => { (sender as Window).Close(); };

            // initialize GameState
            HandleNewGameState();

            // initialize GameTime
            GameTime = new GameTime();
            GameTime.Start();
            logger = Logger.Instance;
            logger.SetLevel(2);
            // writeToFile should be optional
            logger.SetWriteMode(true, true);
            
            // debug Text
            Text debugText = new Text("debug Text", new Font("Assets/Fonts/calibri.ttf"));

            while (running && win.IsOpen())
            {
                KeyboardInputManager.Update();

                // update GameTime
                GameTime.Update();
                float deltaTime = (float)GameTime.EllapsedTime.TotalSeconds;
                // logger needs Timespan for Timestamp!
                logger.UpdateTime(GameTime.TotalTime);
                currentGameState = state.Update(deltaTime);

                if (currentGameState != prevGameState)
                {
                    HandleNewGameState();
                }

                // gather drawStuff from State
                win.Clear(new Color(100, 149, 237));    //cornflowerblue ftw!!! 1337
                state.Draw(win, view, deltaTime);
                state.DrawGUI(gui, deltaTime);

                // some DebugText
                debugText.DisplayedString = "fps: " + (1.0F / deltaTime);
                gui.Draw(debugText);

                // do the actual drawing
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();
                Update_view();
            }
        }

        static void HandleNewGameState()
        {
            switch (currentGameState)
            {
                case GameState.None:
                    running = false;
                    break;

                case GameState.MainMenu:
                    state = new MainMenuState();
                    break;

                case GameState.InGame:
                    state = new InGameState();
                    break;

                case GameState.Reset:
                    currentGameState = prevGameState;
                    prevGameState = GameState.Reset;
                    HandleNewGameState();
                    break;
            }

            prevGameState = currentGameState;

            ResetView();
        }

        static void ResetView()
        {
            view.Center = new Vector2(win.Size.X / 2F, win.Size.Y / 2F);
            view.Size = new Vector2(win.Size.X, win.Size.Y);
        }
        static InGameState Pv = new InGameState();

        static void Update_view()
        {

            

        }
    }
}