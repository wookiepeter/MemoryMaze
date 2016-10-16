using System;

public class Rand
{
    static Random random = new Random();

    /// <summary>random Value in Range [0.0, 1.0]</summary>
    public static float Value()
    {
        return (float)random.NextDouble();
    }

    /// <summary>random Value in Range [0.0, maxValue]</summary>
    public static float Value(float maxValue)
    {
        return (float)(random.NextDouble() * maxValue);
    }

    /// <summary>random Value in Range [minValue, maxValue]</summary>
    public static float Value(float minValue, float maxValue)
    {
        return (float)(minValue + random.NextDouble() * (maxValue - minValue));
    }

    /// <summary>random nonNegative number</summary>
    public static int IntValue()
    {
        return random.Next();
    }

    /// <summary>random integer [minValue, maxValue)</summary>
    public static int IntValue(int maxValue)
    {
        return random.Next(maxValue);
    }

    /// <summary>random integer [minValue, maxValue)</summary>
    public static int IntValue(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }
}

