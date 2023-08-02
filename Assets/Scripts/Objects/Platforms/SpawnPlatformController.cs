using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPlatformController : MonoBehaviour
{
    public abstract IReadOnlyList<SpawnPoint> SpawnPoints { get; }
    public abstract Vector3 StartPointPosition { get; }
    public abstract Vector3 EndPointPosition { get; }

    public abstract Vector2 GetRealSize();
    public abstract void MoveByOffset();
    public abstract void DestroyPlatform();
}
