using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityMovement : MovementType
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private GameObject _lastPlatform;
    private bool _canUse;

    public override bool CanUse => _canUse;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();

        _canUse = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SpawnPlatformController platformController))
        {
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

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

        _rigidbody.gravityScale *= -1;
        transform.eulerAngles += new Vector3(0, 180f, 180f);

        var offset = new Vector3(0, _collider.size.ToRealSize(transform.localScale).y * Mathf.Sign(_rigidbody.gravityScale) * -1);
        transform.position += offset;

        _canUse = false;
    }
}
