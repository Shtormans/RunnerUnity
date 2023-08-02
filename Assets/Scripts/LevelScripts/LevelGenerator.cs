using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField, Range(0, 50)] private int _startPlatformsAmount = 20;
    [SerializeField, Range(0, 50)] private int _amountBeforeFirstLevelPartSpawn = 14;
    [SerializeField, Range(0, 50)] private float _minDistanceToSpawn = 30f;
    [SerializeField, Range(0, 50)] private float _maxDistanceBeforeDelete = 30f;
    [SerializeField, Range(1, 50)] private int _gapBetweenDifferentPositions = 1;
    [SerializeField, Range(1, 900)] private int _blocksBeforeLastPart = 100;

    [SerializeField] private SpawnPoint _firstSpawnPoint;
    [SerializeField] private GameObject _lastPlatform;
    [SerializeField] private GameObject[] _platforms;
    [SerializeField] private List<LevelPart> _levelPartsWithChances;

    private int _lastSpawnPointIndex;
    private int? _nextSpawnPointIndex;

    private Vector3 _nextSpawnPoint;
    private int _createdPlatformsAmount;
    private int _currentGap;
    private bool _needGap;

    public Vector3 LastPoint => _nextSpawnPoint;
    public float MinDistanceToSpawn => _minDistanceToSpawn;
    public float MaxDistanceBeforeDelete => _maxDistanceBeforeDelete;
    public bool LastPartWasGenerated => _createdPlatformsAmount == _blocksBeforeLastPart;

    private void Awake()
    {
        _nextSpawnPoint = _firstSpawnPoint.Position;

        _lastSpawnPointIndex = -1;
        _nextSpawnPointIndex = null;

        _currentGap = 0;
        _needGap = false;
    }

    public SpawnPlatformController[] GenerateStartLevel()
    {
        var platforms = Enumerable
            .Range(0, _startPlatformsAmount)
            .Select(x => GenerateNewPlatformWithParts())
            .ToArray();

        return platforms;
    }

    public SpawnPlatformController GenerateNewPlatformWithParts()
    {
        if (_createdPlatformsAmount + 1 == _blocksBeforeLastPart)
        {
            return InstantiateNewPlatform(_lastPlatform);
        }

        var platformController = GenerateNewPlatform();
        GenerateNewEntity(platformController);

        return platformController;
    }

    public SpawnPlatformController GenerateNewPlatform()
    {
        var platform = PickNewPlatform();

        var platformController = InstantiateNewPlatform(platform);

        return platformController;
    }

    private SpawnPlatformController InstantiateNewPlatform(GameObject platform)
    {
        platform = Instantiate(platform, _nextSpawnPoint, new Quaternion());
        _createdPlatformsAmount++;

        var platformController = platform.GetComponent<SpawnPlatformController>();

        platformController.MoveByOffset();
        _nextSpawnPoint = platformController.EndPointPosition;

        return platformController;
    }

    private void GenerateNewEntity(SpawnPlatformController platformController)
    {
        if (platformController.SpawnPoints.Count == 0)
        {
            return;
        }

        Vector3 position;
        Quaternion rotation;
        var levelPart = PickNewPlatform(platformController, out position, out rotation);

        if (levelPart != null)
        {
            var prefab = Instantiate(levelPart, position, rotation);

            prefab.transform.parent = platformController.transform;
        }
    }

    private GameObject PickNewPlatform()
    {
        var levelPart = RandomChoiceMaker.MakeChoice(_platforms);

        return levelPart;
    }

    private GameObject PickNewPlatform(SpawnPlatformController platformController, out Vector3 position, out Quaternion rotation)
    {
        if (_createdPlatformsAmount <= _amountBeforeFirstLevelPartSpawn)
        {
            return ReturnNull(out position, out rotation);
        }

        if (_needGap && _currentGap < _gapBetweenDifferentPositions)
        {
            _currentGap++;

            return ReturnNull(out position, out rotation);
        }

        var levelPart = PickLevelPart();

        if (levelPart == null)
        {
            return ReturnNull(out position, out rotation);
        }

        var spawnPoints = platformController.SpawnPoints;

        var randomIndex = Random.Range(0, spawnPoints.Count);

        if (randomIndex != _lastSpawnPointIndex && _lastSpawnPointIndex != -1)
        {
            _needGap = true;
            _currentGap++;

            _lastSpawnPointIndex = -1;
            _nextSpawnPointIndex = randomIndex;

            return ReturnNull(out position, out rotation);
        }

        if (_needGap)
        {
            _currentGap = 0;
            _needGap = false;
        }

        randomIndex = _nextSpawnPointIndex ?? randomIndex;
        var spawnPoint = spawnPoints[randomIndex];
        _nextSpawnPointIndex = null;

        _lastSpawnPointIndex = randomIndex;

        position = spawnPoint.Position;
        rotation = spawnPoint.Rotation;

        return levelPart;
    }

    private GameObject PickLevelPart()
    {
        var chanceObjects = _levelPartsWithChances.Select(x => new RandomChoiceObject<GameObject>(x.Prefab, x.Chances)).ToArray();

        var selectedPart = RandomChoiceMaker.MakeChoiceWithChances(chanceObjects, null);

        return selectedPart;
    }

    private GameObject ReturnNull(out Vector3 position, out Quaternion rotation)
    {
        position = default;
        rotation = default;

        return null;
    }
}
