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
    class ChooseSaveSlotState : IGameState
    {
        Font font;
        Text profileOneText, profileOneReset,  profileTwoText, profileTwoReset, profileThreeText, profileThreeReset, back;
        Sprite background;

        List<Text> textlist;
        List<IntRect> list;
        Stopwatch stopwatch;

        ManageProfiles profiles;

        bool settingNewProfile = false;

        public ChooseSaveSlotState()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("CHOOSESAVESLOTSTATE");
            profiles = new ManageProfiles();
            profiles = profiles.loadManageProfiles();
            Initialization();
            //background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
        }

        void Initialization()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();

            list.Add(new IntRect(250, 420, 220, 60));   //slot1          0
            list.Add(new IntRect(750, 420, 220, 60));
            list.Add(new IntRect(250, 470, 220, 60));   //slot2          1
            list.Add(new IntRect(750, 470, 220, 60));
            list.Add(new IntRect(250, 520, 220, 60));   //slot3          2
            list.Add(new IntRect(750, 520, 220, 60));
            list.Add(new IntRect(250, 570, 220, 60));   //Back           3   

            //Initialisieren von  Text

            profileOneText = new Text(profiles.getProfileName(MemoryMaze.profiles.one), font);
            profileOneText.Position = new Vector2f(250, 400);
            profileOneText.CharacterSize = 40;

            profileOneReset = new Text("...", font);
            profileOneReset.Position = new Vector2f(750, 400);
            profileOneReset.CharacterSize = 40;

            profileTwoText = new Text(profiles.getProfileName(MemoryMaze.profiles.two), font);
            profileTwoText.Position = new Vector2f(250, 450);
            profileTwoText.CharacterSize = 40;

            profileTwoReset = new Text("...", font);
            profileTwoReset.Position = new Vector2f(750, 450);
            profileTwoReset.CharacterSize = 40;

            profileThreeText = new Text(profiles.getProfileName(MemoryMaze.profiles.three), font);
            profileThreeText.Position = new Vector2f(250, 500);
            profileThreeText.CharacterSize = 40;

            profileThreeReset = new Text("...", font);
            profileThreeReset.Position = new Vector2f(750, 500);
            profileThreeReset.CharacterSize = 40;

            back = new Text("Back", font);
            back.Position = new Vector2f(250, 550);
            back.CharacterSize = 40;

            Text[] array = { profileOneText, profileOneReset, profileTwoText, profileTwoReset, profileThreeText, profileThreeReset, back };
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
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !settingNewProfile)
                    return GameState.MainMenu;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && settingNewProfile)
                {
                    settingNewProfile = false;
                    stopwatch.Restart();
                    return GameState.ChooseSaveSlotState;
                }
                for (int e = 0; e < 7; e++)
                {
                    if (IsMouseInRectangle(list[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (settingNewProfile)
                {
                    String newProfileName = Console.ReadLine();
                    profiles.setProfile(newProfileName, ProfileConstants.activeProfile);
                    settingNewProfile = false;
                    switch(ProfileConstants.activeProfile)
                    {
                        case MemoryMaze.profiles.one: profileOneText.DisplayedString = profiles.getActiveProfileName();
                            break;
                        case MemoryMaze.profiles.two: profileTwoText.DisplayedString = profiles.getActiveProfileName();
                            break;
                        case MemoryMaze.profiles.three: profileThreeText.DisplayedString = profiles.getActiveProfileName();
                            break;
                    }
                }
                else
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                    {
                        //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                        switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                        {                                                               //bearbeitet das aktuelle TextFeld
                            case 0:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                                return GameState.LoadLevelState;
                            case 1:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                                settingNewProfile = true;
                                break;
                            case 2:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                                return GameState.LoadLevelState; //LevelsStarten
                            case 3:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                                settingNewProfile = true;
                                break;
                            case 4:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.three;
                                return GameState.LoadLevelState;
                            case 5:
                                ProfileConstants.activeProfile = MemoryMaze.profiles.three;
                                settingNewProfile = true;
                                break;
                            case 6: return GameState.MainMenu;

                        }
                    }
                    else
                    {
                        if (index != -1)
                            textlist[index].Color = Color.Blue;
                    }
                }
            }
            return GameState.ChooseSaveSlotState;
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
