using SFML.Window;
using System.Collections.Generic;

public class KeyboardInputManager
{
    static bool isInitialized = false;

    private static int keyCount;
    private static bool[] previousKeyIsPressed;
    private static bool[] currentKeyIsPressed;

    private static void Initialize()
    {
        keyCount = (int)Keyboard.Key.KeyCount;
        previousKeyIsPressed = new bool[keyCount];
        currentKeyIsPressed = new bool[keyCount];

        isInitialized = true;
    }

    public static void Update()
    {
        if (!isInitialized) { Initialize(); }

        for (int i = 0; i < keyCount; ++i)
        {
            previousKeyIsPressed[i] = currentKeyIsPressed[i];
            currentKeyIsPressed[i] = Keyboard.IsKeyPressed((Keyboard.Key)i);
        }
    }

    public static bool IsPressed(Keyboard.Key key)
    {
        return currentKeyIsPressed[(int)key];
    }

    /// <summary>returns true, if the key is pressed this frame and was not pressed previous frame</summary>
    /// <param name="key">Key to be evaluated</param>
    /// <returns>returns true, if the key is pressed this frame and was not pressed previous frame</returns>
    public static bool Downward(Keyboard.Key key)
    {
        if (!isInitialized) { Initialize(); }

        return !previousKeyIsPressed[(int)key] && currentKeyIsPressed[(int)key];
    }

    /// <summary>returns true, if the key is not pressed this frame and was pressed previous frame</summary>
    /// <param name="key">Key to be evaluated</param>
    /// <returns>returns true, if the key is not pressed this frame and was pressed previous frame</returns>
    public static bool Upward(Keyboard.Key key)
    {
        if (!isInitialized) { Initialize(); }

        return previousKeyIsPressed[(int)key] && !currentKeyIsPressed[(int)key];
    }

    public static char KeyToChar(Keyboard.Key key)
    {
        return (char)((int)key + (int)'A');
    }

    public static List<Keyboard.Key> PressedKeys()
    {
        List<Keyboard.Key> pressedKeys = new List<Keyboard.Key>();
        for (int i = 0; i < currentKeyIsPressed.Length; i++)
        {
            if(currentKeyIsPressed[i])
            {
                pressedKeys.Add((Keyboard.Key)i);
            }
        }
        return pressedKeys;
    }

    public static List<char> getCharInput()
    {
        List<char> result = new List<char>();
        for (Keyboard.Key i = Keyboard.Key.A; i <= Keyboard.Key.Z; i++)
        {
            if(Downward(i))
            {
                if(IsPressed(Keyboard.Key.LShift) || IsPressed(Keyboard.Key.RShift))
                {
                    result.Add(KeyToChar(i));
                }
                else
                {
                    result.Add((char)(KeyToChar(i) + ('a' - 'A')));
                }
            }
        }
        return result;
    }

    public static char NumberKeyToChar(Keyboard.Key key)
    {
        return (char)((key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9) ? 
            ((int)key - (Keyboard.Key.Num0) + (int)'0') : ((int)key - (Keyboard.Key.Numpad0) + (int)'0'));
    } 

    public static List<char> getNumberInput()
    {
        List<char> result = new List<char>();

        for (Keyboard.Key i = Keyboard.Key.Num0; i <= Keyboard.Key.Num9; i++)
        {
            if(Downward(i))
            {
                result.Add(NumberKeyToChar(i));
            }
        }
        for (Keyboard.Key i = Keyboard.Key.Numpad0; i <= Keyboard.Key.Numpad9; i++)
        {
            if(Downward(i))
            {
                result.Add(NumberKeyToChar(i));
            }
        }
        return result;
    }
}
