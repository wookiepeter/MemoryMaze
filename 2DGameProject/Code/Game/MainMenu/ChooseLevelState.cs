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
    class ChooseLevelState : IGameState
    {
        Font font;
        Text playOn, levelSelect, control, back;
        Sprite background;

        List<Text> textlist;
        List<IntRect> list;
        Stopwatch stopwatch;

        bool selectingLevel = false;

        public ChooseLevelState()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("CHOOSELEVELSTATE");
            Initialization();
            //background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
        }

        void Initialization()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();

            list.Add(new IntRect(250, 420, 220, 60));   //choose a level 0
            list.Add(new IntRect(250, 470, 220, 60));   //slot2          1
            list.Add(new IntRect(250, 520, 220, 60));   //slot3          2   
            list.Add(new IntRect(250, 570, 220, 60));   //Back           3   

            //Initialisieren von  Text

            playOn = new Text("Enter Level", font);
            playOn.Position = new Vector2f(250, 400);
            playOn.CharacterSize = 40;

            levelSelect = new Text("...", font);
            levelSelect.Position = new Vector2f(250, 450);
            levelSelect.CharacterSize = 40;

            control = new Text("...", font);
            control.Position = new Vector2f(250, 500);
            control.CharacterSize = 40;

            back = new Text("Back", font);
            back.Position = new Vector2f(250, 550);
            back.CharacterSize = 40;

            Text[] array = { playOn, levelSelect, control, back };
            textlist = array.ToList();

        }

        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)                          //Ist die Maus über ein IntRect
        {
            Vector2i mouse = Mouse.GetPosition() - win.Position;
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }

        public void LoadContent()
        {

        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            int index = -1;
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape) && selectingLevel)
                {
                    stopwatch.Restart();
                    selectingLevel = false;
                    return GameState.ChooseLevelState;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    return GameState.ChooseSaveSlotState;
                for (int e = 0; e < 4; e++)
                {
                    if (IsMouseInRectangle(list[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (selectingLevel)
                {
                    String input = Console.ReadLine();
                    int level = int.Parse(input);

                    ManageProfiles manageProfiles = new ManageProfiles();
                    manageProfiles = manageProfiles.loadManageProfiles();

                    ManageStars manageStars = new ManageStars();
                    // TODO: find a freakin better way to do this... fucking hax :(
                    manageStars = manageStars.unsafelyLoadManageStars(manageProfiles.getActiveProfileName());

                    if(manageStars.levelIsUnlocked(level))
                    {
                        Console.WriteLine("Level " + level + " is starting...");
                        ProfileConstants.levelToPlay = level;
                        return GameState.StartGameAtLevel;
                    }
                    else
                    {
                        Console.WriteLine("Level " + level + " was not yet unlocked");
                    }

                    selectingLevel = false;
                }
                else
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                    {
                        //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                        switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                        {                                                               //bearbeitet das aktuelle TextFeld
                            case 0:
                                selectingLevel = true;
                                return GameState.ChooseLevelState;
                            //Levels
                            case 1: return GameState.LoadLevelState; //LevelsStarten
                                                                 //Steuerung
                            case 2: return GameState.LoadLevelState;
                            //MainMenu
                            case 3: return GameState.LoadLevelState;
                        }
                    }
                    else
                    {
                        if (index != -1)
                            textlist[index].Color = Color.Blue;
                    }
                }
            }

            return GameState.ChooseLevelState;
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

        }
    }
}
