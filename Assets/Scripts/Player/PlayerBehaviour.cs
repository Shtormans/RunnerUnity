using System;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerBehaviour : MonoBehaviour
{
    private MovementController _movementController;
    private MovementType _movementType;
    private ScriptsContainer _scriptsContainer;
    private IBonus _currentBonus;

    public event Action OnDied;
    public event Action OnStartedMoving;
    public event Action OnFinishedLevel;
    public event Action OnUsedMovement;
    public event Action OnBonusCollected;
    public event Action OnAppeared;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
        _scriptsContainer = GetComponent<ScriptsContainer>();
        _movementType = GetComponent<MovementType>();
    }

    private void OnEnable()
    {
        OnAppeared?.Invoke();
    }

    public void UseMovementType()
    {
        if (!_movementController.IsMoving)
        {
            StartMovement();
            return;
        }

        if (_movementType.CanUse)
        {
            OnUsedMovement?.Invoke();
        }

        _movementType.Use();
    }

    public void TakeDamage()
    {
        _currentBonus?.Deactivate();

        _scriptsContainer.EnterCutsceneMode();

        OnDied?.Invoke();
    }

    public void FinishLevel()
    {
        _scriptsContainer.EnterCutsceneMode();

        OnFinishedLevel?.Invoke();
    }

    public void SetBonus(IBonus bonus)
    {
        OnBonusCollected?.Invoke();

        if (_currentBonus != null)
        {
            _currentBonus.Deactivate();
        }

        _currentBonus = gameObject.AddComponent(bonus.GetType()) as IBonus;
        _currentBonus.Activate();
    }

    private void StartMovement()
    {
        _scriptsContainer.EnterGameMode();

        OnStartedMoving?.Invoke();
    }
}
