using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
