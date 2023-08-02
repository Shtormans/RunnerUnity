using UnityEngine;
using UnityEngine.UIElements;

public class HoodController : MonoBehaviour
{
    private Label _scoreText;

    private void Awake()
    {
        var document = GetComponent<UIDocument>();

        _scoreText = document.rootVisualElement.Q("ScoreText") as Label;
        ChangeScore(StatsContainer.Score);
    }

    public void ChangeScore(int score)
    {
        _scoreText.text = StringFormatter.FormatScore(score);
    }
}
