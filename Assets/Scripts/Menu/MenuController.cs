using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartGameButtonClick()
    {
        LevelMover.MoveToNewLevel("");
    }

    public void OnExitToMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
