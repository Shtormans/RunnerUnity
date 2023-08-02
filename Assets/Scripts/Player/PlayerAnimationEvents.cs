using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event Action OnDeathAnimationEnds;
    public event Action OnLevelFinishedAnimationEnds;

    public void DeathAnimationEnds()
    {
        OnDeathAnimationEnds?.Invoke();
    }

    public void LevelFinishedAnimationEnds()
    {
        OnLevelFinishedAnimationEnds?.Invoke();
    }
}
