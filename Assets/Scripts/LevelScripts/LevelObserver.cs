using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private PlayerAnimationEvents _playerAnimationEvents;
    [SerializeField] private GameOverController _gameOverController;


    private void Start()
    {
        _player.OnDied += Player_OnDied;
        _playerAnimationEvents.OnDeathAnimationEnds += OnDiedAfterAnimationEnds;
        _playerAnimationEvents.OnLevelFinishedAnimationEnds += OnFinishedLevelAfterAnimationEnds;

        StartGame();
    }

    private void OnDisable()
    {
        _player.OnDied -= Player_OnDied;
        _playerAnimationEvents.OnDeathAnimationEnds -= OnDiedAfterAnimationEnds;
        _playerAnimationEvents.OnLevelFinishedAnimationEnds -= OnFinishedLevelAfterAnimationEnds;
        _gameOverController.OnHomeButtonClicked -= OnHomeButtonClicked;
        _gameOverController.OnRestartButtonClicked -= OnRestartButtonClicked;
    }

    private void Player_OnDied()
    {
        if (StatsContainer.Score > MemoryContainer.GetBestScore())
        {
            MemoryContainer.SetBestScore(StatsContainer.Score);
        }
        MemoryContainer.SetLastScore(StatsContainer.Score);

        _gameOverController.gameObject.SetActive(true);
        _gameOverController.SetGameOverInfo(StatsContainer.Score, StatsContainer.TimeInSeconds, StatsContainer.CollectedBonuses);

        _gameOverController.OnHomeButtonClicked += OnHomeButtonClicked;
        _gameOverController.OnRestartButtonClicked += OnRestartButtonClicked;
    }

    private void OnDiedAfterAnimationEnds()
    {
        _player.gameObject.SetActive(false);
    }

    private void OnFinishedLevelAfterAnimationEnds()
    {
        _player.gameObject.SetActive(false);

        var levelName = SceneManager.GetActiveScene().name;
        LevelMover.MoveToNewLevel(levelName);
    }

    private void OnRestartButtonClicked()
    {
        LevelMover.StartNewGame();
    }

    private void OnHomeButtonClicked()
    {
        LevelMover.MoveToMainMenu();
    }

    private void StartGame()
    {
        _player.gameObject.SetActive(true);
    }
}
