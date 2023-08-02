using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{
    public event Action OnHomeButtonClicked;
    public event Action OnRestartButtonClicked;

    public void SetGameOverInfo(int score, int timeInSeconds, int bonusCollected)
    {
        var document = GetComponent<UIDocument>();

        var scoreText = document.rootVisualElement.Q("ScoreText") as Label;
        var timeText = document.rootVisualElement.Q("TimeText") as Label;
        var bonusText = document.rootVisualElement.Q("BonusText") as Label;
        var homeButton = document.rootVisualElement.Q("HomeButton") as Button;
        var restartButton = document.rootVisualElement.Q("RestartButton") as Button;

        scoreText.text = StringFormatter.FormatScore(score);
        timeText.text = StringFormatter.FormatTimeFromSeconds(timeInSeconds);
        bonusText.text = bonusCollected.ToString();

        homeButton.clicked += HomeButton_clicked;
        restartButton.clicked += RestartButton_clicked;
    }

    private void HomeButton_clicked()
    {
        OnHomeButtonClicked?.Invoke();
    }

    private void RestartButton_clicked()
    {
        OnRestartButtonClicked?.Invoke();
    }
}
