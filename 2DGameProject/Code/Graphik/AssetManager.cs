using System.Collections.Generic;
using SFML.Graphics;

public class AssetManager
{
    static Dictionary<TextureName, Texture> textures = new Dictionary<TextureName, Texture>();

    public static Texture GetTexture(TextureName textureName)
    {
        if (textures.Count == 0)
        {
            LoadTextures(); 
        }
        return textures[textureName];
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
    }
}

