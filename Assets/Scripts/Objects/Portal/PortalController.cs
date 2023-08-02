using UnityEngine;

public class PortalController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerBehaviour playerBehaviour))
        {
            playerBehaviour.FinishLevel();
        }
    }
}
