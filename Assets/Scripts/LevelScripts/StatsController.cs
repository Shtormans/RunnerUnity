using UnityEngine;

public class StatsController : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private HoodController _hood;
    private float _timer;

    private float _playerLastPositionX;

    private void Start()
    {
        _player.OnBonusCollected += OnBonusCollected;
        _playerLastPositionX = _player.Position.x;
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer >= 1)
        {
            StatsContainer.AddSecondsToTimer((int)_timer);
            _timer -= (int)_timer;
        }

        var scoreToAdd = (int)(_player.Position.x - _playerLastPositionX);
        if (scoreToAdd != 0)
        {
            _playerLastPositionX = _player.Position.x;
            StatsContainer.AddToScore(scoreToAdd);

            _hood.ChangeScore(StatsContainer.Score);
        }
    }

    private void OnBonusCollected()
    {
        StatsContainer.IncrementBonusAmount();
    }
}
