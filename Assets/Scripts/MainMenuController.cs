using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene"); // Replace "GameScene" with the name of your actual game scene.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
