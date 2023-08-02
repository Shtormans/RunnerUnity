using UnityEngine;

public abstract class MovementType : MonoBehaviour
{
    public abstract bool CanUse { get; }

    public abstract void Use();
}
