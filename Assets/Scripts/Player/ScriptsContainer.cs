using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptsContainer : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _playerCollider;
    private BoxCollider2D _playerTriggerCollider;

    private List<IDisabled> _scripts;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CapsuleCollider2D>();
        _playerTriggerCollider = GetComponent<BoxCollider2D>();

        _scripts = GetComponents<IDisabled>().ToList();
    }

    public void EnterCutsceneMode()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = new Vector2(0, 0);
        _playerCollider.enabled = false;
        _playerTriggerCollider.enabled = false;

        _scripts.ForEach(x => x.Disable());
    }

    public void EnterGameMode()
    {
        _rigidbody.isKinematic = false;
        _playerCollider.enabled = true;
        _playerTriggerCollider.enabled = true;

        _scripts.ForEach(x => x.Enable());
    }
}
