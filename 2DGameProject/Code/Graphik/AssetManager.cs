using System.Collections.Generic;
using SFML.Graphics;
using SFML.Audio;

public class AssetManager
{
    static Dictionary<TextureName, Texture> textures = new Dictionary<TextureName, Texture>();
    static Dictionary<MusicName, Music> musics = new Dictionary<MusicName, Music>();
    static Dictionary<SoundName, SoundBuffer> sounds = new Dictionary<SoundName, SoundBuffer>();
    static Dictionary<int, Texture> screenShots = new Dictionary<int, Texture>(); 
    
    //Textures
    public static Texture GetTexture(TextureName textureName)
    {
        if (textures.Count == 0)
        {
            LoadTextures();
        }
        return textures[textureName];
    }

    public static Texture GetScreenShot(int levelIndex)
    {
        if (screenShots.Count == 0)
            LoadScreenShots();
        return screenShots[levelIndex];
    }

    static void LoadScreenShots()
    {
        screenShots.Add(0, new Texture("Assets/Textures/Screens/Gelb01.png"));
        screenShots.Add(1, new Texture("Assets/Textures/Screens/Gelb02.png"));
        screenShots.Add(2, new Texture("Assets/Textures/Screens/Gelb03.png"));
        screenShots.Add(3, new Texture("Assets/Textures/Screens/Gelb04.png"));
        screenShots.Add(4, new Texture("Assets/Textures/Screens/Gelb05.png"));
        screenShots.Add(5, new Texture("Assets/Textures/Screens/Gelb06.png"));
        screenShots.Add(6, new Texture("Assets/Textures/Screens/Gelb07neu.png"));
        screenShots.Add(7, new Texture("Assets/Textures/Screens/Gelb08neu.png"));
        screenShots.Add(8, new Texture("Assets/Textures/Screens/Gelb09.png"));
        screenShots.Add(9, new Texture("Assets/Textures/Screens/Gelb10.png"));
        screenShots.Add(10, new Texture("Assets/Textures/Screens/Gelb11.05.png"));
        screenShots.Add(11, new Texture("Assets/Textures/Screens/Gelb11.png"));
        screenShots.Add(12, new Texture("Assets/Textures/Screens/Rot1.png"));
        screenShots.Add(13, new Texture("Assets/Textures/Screens/Rot2.png"));
        screenShots.Add(14, new Texture("Assets/Textures/Screens/Rot3.png"));
        screenShots.Add(15, new Texture("Assets/Textures/Screens/Rot4.png"));
        screenShots.Add(16, new Texture("Assets/Textures/Screens/Rot5.png"));
        screenShots.Add(17, new Texture("Assets/Textures/Screens/Rot6.png"));
        screenShots.Add(18, new Texture("Assets/Textures/Screens/Rot7.png"));
        screenShots.Add(19, new Texture("Assets/Textures/Screens/Rot8.png"));
        screenShots.Add(20, new Texture("Assets/Textures/Screens/Rot9.png"));
        screenShots.Add(21, new Texture("Assets/Textures/Screens/Rot10.png"));
        screenShots.Add(22, new Texture("Assets/Textures/Screens/Rot11.png"));
        screenShots.Add(23, new Texture("Assets/Textures/Screens/Rot12.png"));
        screenShots.Add(24, new Texture("Assets/Textures/Screens/Gruen1.png"));
        screenShots.Add(25, new Texture("Assets/Textures/Screens/Gruen2.png"));
        screenShots.Add(26, new Texture("Assets/Textures/Screens/Gruen3.png"));
        screenShots.Add(27, new Texture("Assets/Textures/Screens/Gruen4.png"));
        screenShots.Add(28, new Texture("Assets/Textures/Screens/Gruen5.png"));
        screenShots.Add(29, new Texture("Assets/Textures/Screens/Gruen6.png"));
        screenShots.Add(30, new Texture("Assets/Textures/Screens/Gruen7.png"));
        screenShots.Add(31, new Texture("Assets/Textures/Screens/Blau1.png"));
        screenShots.Add(32, new Texture("Assets/Textures/Screens/Blau2.png"));
        screenShots.Add(33, new Texture("Assets/Textures/Screens/Blau3.png"));
        screenShots.Add(34, new Texture("Assets/Textures/Screens/Blau4.png"));
        screenShots.Add(35, new Texture("Assets/Textures/Screens/Blau5.png"));
        screenShots.Add(36, new Texture("Assets/Textures/Screens/Blau6.png"));
        screenShots.Add(37, new Texture("Assets/Textures/Screens/Blau7.png"));
        screenShots.Add(38, new Texture("Assets/Textures/Screens/Blau8.png"));
        screenShots.Add(39, new Texture("Assets/Textures/Screens/Blau9.png"));
        screenShots.Add(40, new Texture("Assets/Textures/Screens/Blau10.png"));
        screenShots.Add(41, new Texture("Assets/Textures/Screens/Blau11.png"));
        screenShots.Add(42, new Texture("Assets/Textures/Screens/Blau12.png"));
        screenShots.Add(43, new Texture("Assets/Textures/Screens/Blau13.png"));
        screenShots.Add(44, new Texture("Assets/Textures/Screens/Blau14.png"));

    }

    static void LoadTextures()
    {
        textures.Add(TextureName.WhitePixel, new Texture("Assets/Textures/pixel.png"));
        textures.Add(TextureName.MainMenuBackground, new Texture("Assets/Textures/Menu/Menu_Background2.png"));
        textures.Add(TextureName.ShinyEffectBar, new Texture("Assets/Textures/Menu/Menu_BackgroundEffect.png"));
        textures.Add(TextureName.Player, new Texture("Assets/Textures/Virus/Yellow.png"));
        textures.Add(TextureName.RedBot, new Texture("Assets/Textures/Virus/Red.png"));
        textures.Add(TextureName.GreenBot, new Texture("Assets/Textures/Virus/Green.png"));
        textures.Add(TextureName.BlueBot, new Texture("Assets/Textures/Virus/Blue.png"));
        textures.Add(TextureName.PlayerGhostAnimated, new Texture("Assets/Textures/Virus/ghost.png"));
        textures.Add(TextureName.Wall, new Texture("Assets/Textures/placeholder/WallTile.png"));
        textures.Add(TextureName.Item, new Texture("Assets/Textures/Item/key.png"));
        textures.Add(TextureName.KeyAnimated, new Texture("Assets/Textures/Item/key_animated.png"));
        textures.Add(TextureName.ParticlesAnimated, new Texture("Assets/Textures/set_01/particles.png"));
        textures.Add(TextureName.ParticlesOutgoingAnimated, new Texture("Assets/Textures/set_01/particlesOutgoing.png"));
        textures.Add(TextureName.Movable, new Texture("Assets/Textures/set_01/movable.png"));
        textures.Add(TextureName.GroundLonely, new Texture("Assets/Textures/set_01/floor/old/lonely floor.png"));
        textures.Add(TextureName.Ground1bottom, new Texture("Assets/Textures/set_01/floor/old/end floor top.png"));
        textures.Add(TextureName.Ground1left, new Texture("Assets/Textures/set_01/floor/old/end floor right.png"));
        textures.Add(TextureName.Ground1right, new Texture("Assets/Textures/set_01/floor/old/end floor left.png"));
        textures.Add(TextureName.Ground1top, new Texture("Assets/Textures/set_01/floor/old/end floor bottom.png"));
        textures.Add(TextureName.Ground2bottomright, new Texture("Assets/Textures/set_01/floor/old/corner floor right bottom.png"));
        textures.Add(TextureName.Ground2topright, new Texture("Assets/Textures/set_01/floor/old/corner floor right top.png"));
        textures.Add(TextureName.Ground2leftbottom, new Texture("Assets/Textures/set_01/floor/old/corner floor left bottom.png"));
        textures.Add(TextureName.Ground2lefttop, new Texture("Assets/Textures/set_01/floor/old/corner floor left top.png"));
        textures.Add(TextureName.Ground2horizontal, new Texture("Assets/Textures/set_01/floor/old/floor straight horizontal.png"));
        textures.Add(TextureName.Ground2vertical, new Texture("Assets/Textures/set_01/floor/old/floor straight vertical.png"));
        textures.Add(TextureName.Ground3left, new Texture("Assets/Textures/set_01/floor/old/intersection tlb.png"));
        textures.Add(TextureName.Ground3top, new Texture("Assets/Textures/set_01/floor/old/intersection ltr.png"));
        textures.Add(TextureName.Ground3right, new Texture("Assets/Textures/set_01/floor/old/intersection trb.png"));
        textures.Add(TextureName.Ground3bottom, new Texture("Assets/Textures/set_01/floor/old/intersection lbr.png"));
        textures.Add(TextureName.Ground4, new Texture("Assets/Textures/set_01/floor/old/center two.png"));
        textures.Add(TextureName.Goal, new Texture("Assets/Textures/set_01/floor/goal.png"));
        /// vvvvvvvvvvv will be deleted soon(ish) vvvvvvvvvvv
        textures.Add(TextureName.GoalLonely, new Texture("Assets/Textures/set_01/goal/lonely goal.png"));
        textures.Add(TextureName.Goal1bottom, new Texture("Assets/Textures/set_01/goal/goal b.png"));
        textures.Add(TextureName.Goal1left, new Texture("Assets/Textures/set_01/goal/goal l.png"));
        textures.Add(TextureName.Goal1right, new Texture("Assets/Textures/set_01/goal/goal r.png"));
        textures.Add(TextureName.Goal1top, new Texture("Assets/Textures/set_01/goal/goal t.png"));
        textures.Add(TextureName.Goal2bottomright, new Texture("Assets/Textures/set_01/goal/goal br.png"));
        textures.Add(TextureName.Goal2topright, new Texture("Assets/Textures/set_01/goal/goal tr.png"));
        textures.Add(TextureName.Goal2leftbottom, new Texture("Assets/Textures/set_01/goal/goal lb.png"));
        textures.Add(TextureName.Goal2lefttop, new Texture("Assets/Textures/set_01/goal/goal lt.png"));
        textures.Add(TextureName.Goal2horizontal, new Texture("Assets/Textures/set_01/goal/goal lr.png"));
        textures.Add(TextureName.Goal2vertical, new Texture("Assets/Textures/set_01/goal/goal tb.png"));
        textures.Add(TextureName.Goal3left, new Texture("Assets/Textures/set_01/goal/goal trb.png"));
        textures.Add(TextureName.Goal3top, new Texture("Assets/Textures/set_01/goal/goal lbr.png"));
        textures.Add(TextureName.Goal3right, new Texture("Assets/Textures/set_01/goal/goal ltb.png"));
        textures.Add(TextureName.Goal3bottom, new Texture("Assets/Textures/set_01/goal/goal ltr.png"));
        textures.Add(TextureName.Goal4, new Texture("Assets/Textures/set_01/goal/goal all.png"));
        /// ^^^^^^^^^^^ will be deleted soon(ish) ^^^^^^^^^^^
        textures.Add(TextureName.Teleporter, new Texture("Assets/Textures/set_01/teleporter.png"));
        textures.Add(TextureName.TeleporterExitOnly, new Texture("Assets/Textures/set_01/teleporter_exit.png"));
        textures.Add(TextureName.StarRotating, new Texture("Assets/Textures/set_01/star_rotating2.png"));
        textures.Add(TextureName.Star, new Texture("Assets/Textures/set_01/star.png"));
        textures.Add(TextureName.RedItem, new Texture("Assets/Textures/Virus/RedItem.png"));
        textures.Add(TextureName.BlueItem, new Texture("Assets/Textures/Virus/BlueItem.png"));
        textures.Add(TextureName.GreenItem, new Texture("Assets/Textures/Virus/GreenItem.png"));
        textures.Add(TextureName.ScoreItem, new Texture("Assets/Textures/ScoreItem.png"));
        textures.Add(TextureName.TrapTile, new Texture("Assets/Textures/Traptile.png"));
        textures.Add(TextureName.DasC, new Texture("Assets/Textures/DasC.png"));
        textures.Add(TextureName.DasF, new Texture("Assets/Textures/DasF.png"));
        textures.Add(TextureName.DasM, new Texture("Assets/Textures/DasM.png"));
        //textures.Add(TextureName.Enemy, new Texture("Assets/Textures/placeholder/antivirus.png"));
        textures.Add(TextureName.Overlay, new Texture("Assets/Textures/shader.png"));
        textures.Add(TextureName.BackGroundSteuerung, new Texture("Assets/Textures/BackgrundSteuerung.png"));
        textures.Add(TextureName.LeverOpen, new Texture("Assets/Textures/set_01/floor/Lever.png"));
        textures.Add(TextureName.LeverClosed, new Texture("Assets/Textures/set_01/floor/Lever_Closed.png"));
        textures.Add(TextureName.LeverCondensator, new Texture("Assets/Textures/set_01/floor/Lever_Condensator.png"));
        textures.Add(TextureName.MapBackground1, new Texture("Assets/Textures/Menu/LevelSelect_Background_Hatzte.png"));
        textures.Add(TextureName.MapBackground2, new Texture("Assets/Textures/Menu/LevelSelect_Background_Lueblow.png"));
        textures.Add(TextureName.MapBackground3, new Texture("Assets/Textures/Menu/LevelSelect_Background_Hildesheim.png"));
        textures.Add(TextureName.MapBackground4, new Texture("Assets/Textures/Menu/LevelSelect_Background_Scheuerfeld.png"));
        textures.Add(TextureName.MapBackground5, new Texture("Assets/Textures/Menu/Menu_Background.png"));
        textures.Add(TextureName.MapBackground6, new Texture("Assets/Textures/Menu/Menu_Background.png"));
        textures.Add(TextureName.MapBackground7, new Texture("Assets/Textures/Menu/LevelSelect_Background_Grimstad.png"));
        textures.Add(TextureName.LevelButton, new Texture("Assets/Textures/Menu/Button_Levelselection.png"));
        textures.Add(TextureName.LevelButtonGlow, new Texture("Assets/Textures/Menu/Button_Levelselection_glow.png"));
        textures.Add(TextureName.LevelButtonOptions, new Texture("Assets/Textures/Menu/Button_Options.png"));
        textures.Add(TextureName.LevelButtonOptionsGlow, new Texture("Assets/Textures/Menu/Button_Options_glow.png"));
        textures.Add(TextureName.LevelInfo, new Texture("Assets/Textures/Menu/Button_Levelinfo.png"));
        textures.Add(TextureName.ProfileButton, new Texture("Assets/Textures/Menu/Button_Profile.png"));
        textures.Add(TextureName.ProfileButtonGlow, new Texture("Assets/Textures/Menu/Button_Profile_glow.png"));
        textures.Add(TextureName.ProfileDeleteButton, new Texture("Assets/Textures/Menu/Button_ProfileDeletion.png"));
        textures.Add(TextureName.ProfileDeleteButtonGlow, new Texture("Assets/Textures/Menu/Button_ProfileDeletion_glow.png"));
        textures.Add(TextureName.BotBronze, new Texture("Assets/Textures/Virus/medal_bronze.png"));
        textures.Add(TextureName.BotSilver, new Texture("Assets/Textures/Virus/medal_silver.png"));
        textures.Add(TextureName.BotGold, new Texture("Assets/Textures/Virus/medal_gold.png"));
        textures.Add(TextureName.TutorialDeleteBot, new Texture("Assets/Textures/Menu/Tutorial/DeleteTutorial.png"));
        textures.Add(TextureName.TeleportBot, new Texture("Assets/Textures/Set_01/glow.png"));
        textures.Add(TextureName.TutorialGhostMove, new Texture("Assets/Textures/Menu/Tutorial/Scout2Tutorial.png"));
        textures.Add(TextureName.TutorialGhostSpawn, new Texture("Assets/Textures/Menu/Tutorial/Scout1Tutorial.png"));
        textures.Add(TextureName.TutorialMove, new Texture("Assets/Textures/Menu/Tutorial/MoveTutorial.png"));
        textures.Add(TextureName.TutorialReset, new Texture("Assets/Textures/Menu/Tutorial/ResetTutorial.png"));
        textures.Add(TextureName.TutorialSpawnBot, new Texture("Assets/Textures/Menu/Tutorial/Scout3Tutorial.png"));
        textures.Add(TextureName.TutorialSwitch, new Texture("Assets/Textures/Menu/Tutorial/SwitchTutorial.png"));
        textures.Add(TextureName.YellowEnergybar, new Texture("Assets/Textures/Virus/EnergybarYellow.png"));
        textures.Add(TextureName.GrayEnergybar, new Texture("Assets/Textures/Virus/EnergybarGray.png"));
        textures.Add(TextureName.BlueEnergybar, new Texture("Assets/Textures/Virus/EnergybarBlue.png"));
        textures.Add(TextureName.GreenEnergybar, new Texture("Assets/Textures/Virus/EnergybarGreen.png"));
        textures.Add(TextureName.RedEnergybar, new Texture("Assets/Textures/Virus/EnergybarRed.png"));
        textures.Add(TextureName.HUDYellowBox, new Texture("Assets/Textures/Menu/Hud_Yellow.png"));
        textures.Add(TextureName.HUDRedBox, new Texture("Assets/Textures/Menu/Hud_Red.png"));
        textures.Add(TextureName.HUDGreenBox, new Texture("Assets/Textures/Menu/Hud_Green.png"));
        textures.Add(TextureName.HUDBlueBox, new Texture("Assets/Textures/Menu/Hud_Blue.png"));
        textures.Add(TextureName.HUDKey, new Texture("Assets/Textures/Menu/Hud_Steps.png"));
        textures.Add(TextureName.HUDLevel, new Texture("Assets/Textures/Menu/Hud_Steps.png"));
        textures.Add(TextureName.HUDSteps, new Texture("Assets/Textures/Menu/Hud_Steps.png"));
        textures.Add(TextureName.HUDSkip, new Texture("Assets/Textures/Menu/Hud_Skip.png"));
        textures.Add(TextureName.SkipMedal, new Texture("Assets/Textures/Virus/medal_skip.png"));
        //textures.Add(TextureName.SpaceBar, new Texture("Assets/Textures/Menu/Tutoria/SpaceBar.png"));
        textures.Add(TextureName.IconDelete, new Texture("Assets/Textures/Menu/Icon_Trash.png"));
        textures.Add(TextureName.IconCredits, new Texture("Assets/Textures/Menu/Icon_Credits.png"));
        textures.Add(TextureName.IconOptions, new Texture("Assets/Textures/Menu/Icon_Options.png"));
        textures.Add(TextureName.IconExit, new Texture("Assets/Textures/Menu/Icon_Exit.png"));
        textures.Add(TextureName.KeyGoal, new Texture("Assets/Textures/set_01/floor/goal2.png"));
        textures.Add(TextureName.SpaceBar, new Texture("Assets/Textures/Menu/Tutorial/SpaceBar.png"));
    }

    public enum TextureName
    {
        /*
            The positions for textures(based on the position of the first groundtexture) can be computed using a bitword(results in a normal integer^^)
            One Bit equals the one direct neighbor.
            The order is left | top | right | bottom !

            YOLO :)
        */
        WhitePixel,
        MainMenuBackground,
        ShinyEffectBar,
        Player,
        Wall,
        GroundLonely,
        Ground1bottom,
        Ground1right,
        Ground2bottomright,
        Ground1top,
        Ground2vertical,
        Ground2topright,
        Ground3right,
        Ground1left,
        Ground2leftbottom,
        Ground2horizontal,
        Ground3bottom,
        Ground2lefttop,
        Ground3left,
        Ground3top,
        Ground4,
        Goal,
        KeyGoal,
        /// vvvvvvvvvvv will be deleted soon(ish) vvvvvvvvvvv
        GoalLonely,
        Goal1bottom,
        Goal1right,
        Goal2bottomright,
        Goal1top,
        Goal2vertical,
        Goal2topright,
        Goal3right,
        Goal1left,
        Goal2leftbottom,
        Goal2horizontal,
        Goal3bottom,
        Goal2lefttop,
        Goal3left,
        Goal3top,
        Goal4,
        /// ^^^^^^^^^^^ will be deleted soon(ish) ^^^^^^^^^^^
        Teleporter,
        TeleporterExitOnly,
        StarRotating,
        Star,
        Item,
        KeyAnimated,
        ParticlesAnimated,
        ParticlesOutgoingAnimated,
        Movable,
        Enemy,
        PlayerGhostAnimated,
        RedBot,
        GreenBot,
        BlueBot,
        TeleportBot,
        RedItem,
        BlueItem,
        GreenItem,
        ScoreItem,
        TrapTile,
        Overlay,
        DasC,
        DasM,
        BackGroundSteuerung,
        DasF,
        LeverOpen,
        LeverClosed,
        LeverCondensator,
        MapBackground1,
        MapBackground2,
        MapBackground3,
        MapBackground4,
        MapBackground5,
        MapBackground6,
        MapBackground7,
        LevelButton,
        LevelButtonGlow,
        LevelButtonOptions,
        LevelButtonOptionsGlow,
        LevelInfo,
        ProfileButton,
        ProfileButtonGlow,
        ProfileDeleteButton,
        ProfileDeleteButtonGlow,
        BotBronze,
        BotSilver,
        BotGold,
        TutorialMove,
        TutorialReset,
        TutorialGhostSpawn,
        TutorialGhostMove,
        TutorialSpawnBot,
        TutorialDeleteBot,
        TutorialSwitch,
        YellowEnergybar,
        GreenEnergybar,
        RedEnergybar,
        BlueEnergybar,
        GrayEnergybar,
        HUDYellowBox,
        HUDRedBox,
        HUDGreenBox,
        HUDBlueBox,
        HUDSteps,
        HUDLevel,
        HUDKey,
        HUDSkip,
        SkipMedal,
        IconDelete,
        IconCredits,
        IconExit,
        IconOptions,
        SpaceBar
    }

    //Music!
    public enum MusicName
    {
        MainMenu,
    }
    public static Music GetMusic(MusicName musicName)
    {
        if (musics.Count == 0)
        {
            LoadMusic();
        }
        return musics[musicName];
    }

    static void LoadMusic()
    {
        musics.Add(MusicName.MainMenu, new Music("Assets/Musics/MyFirstSong V2.wav"));
    }

    //Sounds
    public enum SoundName
    {
        TestSong,
        CreateBot,
        Teleport,
        Wall,
        MenueClick,
        LeverNormal,
        BlueLever,
        VirusDetected,
        DeleteBot,
        ItemPick,
        Key
    }
    public static SoundBuffer GetSound(SoundName soundName)
    {
        if (sounds.Count == 0)
            LoadSound();
        return sounds[soundName];
    }
    static void LoadSound()
    {
        System.Console.WriteLine(sounds);
        sounds.Add(SoundName.CreateBot, new SoundBuffer("Assets/Musics/Sounds/CreateBot.wav"));
        sounds.Add(SoundName.Teleport, new SoundBuffer("Assets/Musics/Sounds/teleport.wav"));
        sounds.Add(SoundName.Wall, new SoundBuffer("Assets/Musics/Sounds/Wand.wav"));
        sounds.Add(SoundName.MenueClick, new SoundBuffer("Assets/Musics/Sounds/Menuclick.wav"));
        sounds.Add(SoundName.LeverNormal, new SoundBuffer("Assets/Musics/Sounds/SchalterNormal.wav"));
        sounds.Add(SoundName.BlueLever, new SoundBuffer("Assets/Musics/Sounds/SchalterBlue.wav"));
        sounds.Add(SoundName.VirusDetected, new SoundBuffer("Assets/Musics/Sounds/VirusDetected.wav"));
        sounds.Add(SoundName.ItemPick, new SoundBuffer("Assets/Musics/Sounds/ItemPick.wav"));
        sounds.Add(SoundName.DeleteBot, new SoundBuffer("Assets/Musics/Sounds/DeleteBot.wav"));
        sounds.Add(SoundName.Key, new SoundBuffer("Assets/Musics/Sounds/Schluessel2.wav"));




    }
}

