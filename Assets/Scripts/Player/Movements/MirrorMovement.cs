using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MirrorMovement : MovementType
{
    private Rigidbody2D _rigidbody;
    private Vector3 _lastPlatformSize;
    private GameObject _lastPlatform;
    private bool _canUse;

    public override bool CanUse => _canUse;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _canUse = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SpawnPlatformController platformController))
        {
            _lastPlatformSize = platformController.GetRealSize();
            _lastPlatform = other.gameObject;

            _canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _lastPlatform)
        {
            _canUse = false;
        }
    }

    public override void Use()
    {
        if (!_canUse)
        {
            return;
        }

        _rigidbody.gravityScale *= -1;

        var offset = new Vector3(0, ((_lastPlatformSize.y) + 0.1f) * Mathf.Sign(_rigidbody.gravityScale));
        transform.position += offset;

        transform.eulerAngles += new Vector3(0, 180f, 180f);
    }
}
