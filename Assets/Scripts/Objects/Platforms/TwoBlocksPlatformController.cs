using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TwoBlocksPlatformController : SpawnPlatformController
{
    [SerializeField, Range(0, 10)] private float _secondsBeforeFall = 1f;

    [SerializeField, Range(0, 10)] private float _shakeDuration = 0.2f;
    [SerializeField, Range(0, 10)] private float _shakeMagnitude = 0.2f;

    [SerializeField, Range(0, 10)] private float _secondsBeforeDisappear = 3;

    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private EndPoint _endPoint;

    public override IReadOnlyList<SpawnPoint> SpawnPoints => _spawnPoints;
    public override Vector3 StartPointPosition => _startPoint.Position;
    public override Vector3 EndPointPosition => _endPoint.Position;

    private Rigidbody2D _rigidBody;
    private List<BoxCollider2D> _boxColliderList;
    private Vector3 _spawnOffset;

    public event Action OnDisabled;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxColliderList = GetComponents<BoxCollider2D>().ToList();

        _spawnOffset = transform.position - _startPoint.Position;
    }

    private void OnEnable()
    {
        _rigidBody.isKinematic = true;
        _boxColliderList.ForEach(x => x.enabled = true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerBehaviour playerBehaviour))
        {
            DestroyPlatformWithAnimation();
        }
    }

    public override Vector2 GetRealSize()
    {
        return new Vector2(0, 0);
    }

    public override void MoveByOffset()
    {
        transform.position += _spawnOffset;
    }

    public override void DestroyPlatform()
    {
        Destroy(gameObject);
    }

    public void DestroyPlatformWithAnimation()
    {
        StartCoroutine(PlayCoroutineAnimation(_secondsBeforeFall));
    }

    private IEnumerator PlayCoroutineAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _boxColliderList.ForEach(x => x.enabled = false);

        yield return null;

        yield return ShakeDuringTime(_shakeDuration);

        _rigidBody.isKinematic = false;

        yield return new WaitForSeconds(_secondsBeforeDisappear);

        DestroyPlatform();
    }

    private IEnumerator ShakeDuringTime(float seconds)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            float xOffset = UnityEngine.Random.Range(-0.5f, 0.5f) * _shakeMagnitude;
            float yOffset = UnityEngine.Random.Range(-0.5f, 0.5f) * _shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
