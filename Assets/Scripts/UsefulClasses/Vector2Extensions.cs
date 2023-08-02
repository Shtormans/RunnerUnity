using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 ToRealSize(this Vector2 sizeInUnits, Vector2 localSize)
    {
        return new Vector2(localSize.x * sizeInUnits.x, localSize.y * sizeInUnits.y);
    }
}
