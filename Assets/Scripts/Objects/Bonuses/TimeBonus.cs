using System.Collections;
using UnityEngine;

public class TimeBonus : MonoBehaviour, IBonus
{
    private float _time = 7f;
    private float _newTimeScale = 0.4f;

    private void OnDisable()
    {
        Deactivate();
    }

    public void Activate()
    {
        StartCoroutine(TimeBonusCoroutine());
    }

    public void Deactivate()
    {
        Time.timeScale = 1f;
        Destroy(this);
    }

    private IEnumerator TimeBonusCoroutine()
    {
        Time.timeScale = _newTimeScale;

        yield return new WaitForSecondsRealtime(_time);

        Deactivate();
    }
}
