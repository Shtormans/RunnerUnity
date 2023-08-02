using UnityEngine;

public class AnimationController : MonoBehaviour, IDisabled
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _dustParticles;
    [SerializeField] private ParticleSystem _jumpParticles;

    private PlayerBehaviour _playerBehaviour;
    private MovementController _movementController;

    private void Awake()
    {
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        _movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        _animator.SetFloat(ConstStrings.PlayerAnimator.Speed, _movementController.CurrentSpeed);
    }

    private void OnEnable()
    {
        Enable();

        _playerBehaviour.OnDied += OnDied;
        _playerBehaviour.OnStartedMoving += OnStartedMoving;
        _playerBehaviour.OnFinishedLevel += OnFinishedLevel;
        _playerBehaviour.OnUsedMovement += OnUsedMovement;
    }

    private void OnDisable()
    {
        Disable();

        _playerBehaviour.OnDied -= OnDied;
        _playerBehaviour.OnStartedMoving -= OnStartedMoving;
        _playerBehaviour.OnFinishedLevel -= OnFinishedLevel;
        _playerBehaviour.OnUsedMovement -= OnUsedMovement;
    }

    public void Enable()
    {
        _animator.SetFloat(ConstStrings.PlayerAnimator.Speed, 0f);
        _animator.SetBool(ConstStrings.PlayerAnimator.IsDead, false);
        _animator.SetBool(ConstStrings.PlayerAnimator.HasWon, false);
    }

    public void Disable()
    {
        _dustParticles.Stop();
    }

    private void OnDied()
    {
        _dustParticles.Stop();
        _animator.SetBool(ConstStrings.PlayerAnimator.IsDead, true);
    }

    private void OnStartedMoving()
    {
        _dustParticles.Play();
    }

    private void OnFinishedLevel()
    {
        _dustParticles.Stop();
        _animator.SetBool(ConstStrings.PlayerAnimator.HasWon, true);
    }

    private void OnUsedMovement()
    {
        _jumpParticles.Emit(20);
    }
}
