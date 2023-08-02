using UnityEngine;

[CreateAssetMenu(fileName = "LevelPart", menuName = "ScriptableObject/LevelPart")]
public class LevelPart : ScriptableObject
{
    public GameObject Prefab;

    [Range(0f, 1f)] public float Chances;
}
