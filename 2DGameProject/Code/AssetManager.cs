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
        textures.Add(TextureName.Wall, new Texture("Assets/Textures/placeholder/WallTile.png"));
        textures.Add(TextureName.Ground, new Texture("Assets/Textures/placeholder/GroundTile.png"));
        textures.Add(TextureName.Item, new Texture("Assets/Textures/placeholder/ItemTile.png"));
    }

    public enum TextureName
    {
        WhitePixel,
        MainMenuBackground,
        Wall,
        Ground, 
        Item
    }
}
