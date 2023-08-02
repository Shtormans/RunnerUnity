using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private void Awake()
    {
        var document = GetComponent<UIDocument>();

        var bestScoreText = document.rootVisualElement.Q("BestScoreText") as Label;
        var lastScoreText = document.rootVisualElement.Q("LastScoreText") as Label;
        var startGameButton = document.rootVisualElement.Q("StartGameButton") as Button;

        bestScoreText.text = StringFormatter.FormatScore(MemoryContainer.GetBestScore());
        lastScoreText.text = StringFormatter.FormatScore(MemoryContainer.GetLastScore());
        startGameButton.clicked += StartGameButton_clicked;
    }

    private void StartGameButton_clicked()
    {
        LevelMover.StartNewGame();
    }
}
