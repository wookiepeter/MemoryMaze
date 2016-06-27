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
        textures.Add(TextureName.MainMenuBackground, new Texture("Assets/Textures/MainMenu_Background.jpg"));
        textures.Add(TextureName.Player, new Texture("Assets/Textures/playerface.png"));
        textures.Add(TextureName.Wall, new Texture("Assets/Textures/placeholder/WallTile.png"));
        textures.Add(TextureName.Item, new Texture("Assets/Textures/placeholder/ItemTile.png"));
        textures.Add(TextureName.Movable, new Texture("Assets/Textures/set_01/movable.png"));
        textures.Add(TextureName.GroundEmpty, new Texture("Assets/Textures/set_01/floor/lonely floor.png"));
        textures.Add(TextureName.Ground1bottom, new Texture("Assets/Textures/set_01/floor/end floor top.png"));
        textures.Add(TextureName.Ground1left, new Texture("Assets/Textures/set_01/floor/end floor right.png"));
        textures.Add(TextureName.Ground1right, new Texture("Assets/Textures/set_01/floor/end floor left.png"));
        textures.Add(TextureName.Ground1top, new Texture("Assets/Textures/set_01/floor/end floor bottom.png"));
        textures.Add(TextureName.Ground2bottomright, new Texture("Assets/Textures/set_01/floor/corner floor right bottom.png"));
        textures.Add(TextureName.Ground2topright, new Texture("Assets/Textures/set_01/floor/corner floor right top.png"));
        textures.Add(TextureName.Ground2leftbottom, new Texture("Assets/Textures/set_01/floor/corner floor left bottom.png"));
        textures.Add(TextureName.Ground2lefttop, new Texture("Assets/Textures/set_01/floor/corner floor left top.png"));
        textures.Add(TextureName.Ground2horizontal, new Texture("Assets/Textures/set_01/floor/floor straight horizontal.png"));
        textures.Add(TextureName.Ground2vertical, new Texture("Assets/Textures/set_01/floor/floor straight vertical.png"));
        textures.Add(TextureName.Ground3left, new Texture("Assets/Textures/set_01/floor/intersection tlb.png"));
        textures.Add(TextureName.Ground3top, new Texture("Assets/Textures/set_01/floor/intersection ltr.png"));
        textures.Add(TextureName.Ground3right, new Texture("Assets/Textures/set_01/floor/intersection trb.png"));
        textures.Add(TextureName.Ground3bottom, new Texture("Assets/Textures/set_01/floor/intersection lbr.png"));
        textures.Add(TextureName.Ground4, new Texture("Assets/Textures/set_01/floor/center two.png"));
        textures.Add(TextureName.Goal, new Texture("Assets/Textures/pattern.png"));
        //textures.Add(TextureName.PlayerGhost, new Texture("Assets/Textures/playerfaceGhost.png"));
        //textures.Add(TextureName.Enemy, new Texture("Assets/Textures/placeholder/antivirus.png"));
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
        Player,
        Wall,
        GroundEmpty,
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
        Item,
        Movable,
        Enemy,
        Goal,
        PlayerGhost

    }
}
