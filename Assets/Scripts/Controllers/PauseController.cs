using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameCrosshair;

    private bool _isPaused = false;
    public bool IsGamePaused => _isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        if(_inputHandler == null)
        {
            var player = GameObject.FindGameObjectsWithTag("Player");
            if(player != null)
            {
                _inputHandler = player[0].GetComponent<InputHandler>();
            }
        }
        _gameCrosshair = GameObject.FindGameObjectWithTag("Crosshair");

        if(_inputHandler != null)
        {
            _inputHandler.OnPause += TogglePause;
        }
        _pauseMenu.SetActive(false);
    }
    private void OnDestroy()
    {
        if(_inputHandler != null)
        {
            _inputHandler.OnPause -= TogglePause;
        }        
    }
    public void TogglePause()
    {
        _isPaused = !_isPaused;
        if(_isPaused) {PauseScene();}
        else {ResumeScene();}
    }

    private void PauseScene()
    {
        Time.timeScale = 0f; //time stops
        _pauseMenu.SetActive(true);
        if (_gameCrosshair != null) _gameCrosshair.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Game Paused");
    }

    public void ResumeScene()
    {
        _isPaused = false;
        Time.timeScale = 1f; //time resumes
        _pauseMenu.SetActive(false);
        if (_gameCrosshair != null) _gameCrosshair.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void RestartScene()
    {
        Time.timeScale = 1f; //ensure time is normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload current scene
    }
    public void QuitToMainMenu()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;

        LevelLoader.Instance.LoadNextLevel(0); //load main menu scene
        
    }
}
