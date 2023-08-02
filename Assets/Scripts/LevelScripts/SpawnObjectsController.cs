using UnityEngine;

public class SpawnObjectsController : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;

    private LevelGenerator _levelGenerator;
    private SpawnObjectsContainer _container;

    private void Awake()
    {
        _container = new SpawnObjectsContainer();
        _levelGenerator = GetComponent<LevelGenerator>();
    }

    private void Start()
    {
        var platforms = _levelGenerator.GenerateStartLevel();

        foreach (var platform in platforms)
        {
            _container.Add(platform);
        }
    }

    private void Update()
    {
        WorkWithLevelParts();
    }

    private void CreateNewPart()
    {
        _container.Add(_levelGenerator.GenerateNewPlatformWithParts());
    }

    private void WorkWithLevelParts()
    {
        if (_levelGenerator.LastPartWasGenerated)
        {
            return;
        }

        if (Vector3.Distance(_player.Position, _levelGenerator.LastPoint) < _levelGenerator.MinDistanceToSpawn)
        {
            CreateNewPart();
        }

        DeleteOldPart();
    }

    private void DeleteOldPart()
    {
        if (_container.Count == 0)
        {
            return;
        }

        _container.RemoveFirstNulls();

        var platform = _container.GetFirst();

        if (Vector3.Distance(_player.Position, platform.StartPointPosition) > _levelGenerator.MaxDistanceBeforeDelete)
        {
            _container.RemoveFirst();
        }
    }
}
