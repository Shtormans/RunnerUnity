using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour, IDisabled
{
    [SerializeField, Range(0, 20)] private float _speed = 12;

    public float CurrentSpeed { get; private set; }
    public bool IsMoving => CurrentSpeed > 0;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        CurrentSpeed = 0;
    }

    private void FixedUpdate()
    {
        MoveHorizontally(1f);
    }

    public void SetCanRun(bool canRun)
    {
        CurrentSpeed = canRun ? _speed : 0;
    }

    private void MoveHorizontally(float direction)
    {
        var velocity = _rigidbody.velocity;
        velocity.x = CurrentSpeed * direction;

        _rigidbody.velocity = velocity;
    }

    public void Enable()
    {
        SetCanRun(true);
    }

    public void Disable()
    {
        SetCanRun(false);
    }
}
