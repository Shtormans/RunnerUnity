using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerBehaviour playerBehaviour))
        {
            playerBehaviour.TakeDamage();
        }
    }
}
