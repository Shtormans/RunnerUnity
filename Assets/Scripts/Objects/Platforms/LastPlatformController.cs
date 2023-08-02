using System.Collections.Generic;
using UnityEngine;

public class LastPlatformController : SpawnPlatformController
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private EndPoint _endPoint;

    private Vector3 _spawnOffset;

    public override IReadOnlyList<SpawnPoint> SpawnPoints => _spawnPoints;
    public override Vector3 StartPointPosition => _startPoint.Position;
    public override Vector3 EndPointPosition => _endPoint.Position;

    private void Awake()
    {
        _spawnOffset = transform.position - _startPoint.Position;
    }

    public override void DestroyPlatform()
    {
        Destroy(gameObject);
    }

    public override Vector2 GetRealSize()
    {
        return new Vector2(0, 0);
    }

    public override void MoveByOffset()
    {
        transform.position += _spawnOffset;
    }
}
