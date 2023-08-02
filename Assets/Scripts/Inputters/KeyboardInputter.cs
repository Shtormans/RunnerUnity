using UnityEngine;

public class KeyboardInputter : Inputter, IDisabled
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    private bool _enabled;

    private void Awake()
    {
        _playerBehaviour = GetComponent<PlayerBehaviour>();

        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled)
        {
            return;
        }

        if (DoUseSignatureAbility())
        {
            _playerBehaviour.UseMovementType();
        }
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
    }

    private bool DoUseSignatureAbility()
    {
        return Input.GetMouseButtonDown(0);
    }
}
