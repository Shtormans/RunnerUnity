using UnityEngine;

public class BonusController : MonoBehaviour
{
    private IBonus _bonus;
    private bool isTaken;

    private void Awake()
    {
        _bonus = GetComponent<IBonus>();
        isTaken = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTaken)
        {
            return;
        }

        if (other.TryGetComponent(out PlayerBehaviour player))
        {
            isTaken = true;
            player.SetBonus(_bonus);
        }

        Destroy(gameObject);
    }
}
