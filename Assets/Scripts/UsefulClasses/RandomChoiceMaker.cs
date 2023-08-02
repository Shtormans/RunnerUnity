using System;

public static class RandomChoiceMaker
{
    private static Lazy<Random> _random = new Lazy<Random>(() => new Random());

    public static T MakeChoiceWithChances<T>(RandomChoiceObject<T>[] values, T defaultValue = default)
    {
        var randomValue = _random.Value.NextDouble();

        double chanceSum = 0;

        foreach (var value in values)
        {
            chanceSum += value.Chances;

            if (randomValue <= chanceSum)
            {
                return value.Value;
            }
        }

        return defaultValue;
    }

    public static T MakeChoice<T>(T[] values)
    {
        int randomIndex = _random.Value.Next(0, values.Length);
        
        return values[randomIndex];
    }
}
