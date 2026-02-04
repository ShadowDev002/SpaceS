using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayGame()
    {
        LevelLoader.Instance.LoadNextLevel(1);
    }
    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
