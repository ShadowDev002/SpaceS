using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    [Header("UI References")]
    [SerializeField] private Image _transitionImage;
    [Header("Settings")]
    [SerializeField] private float _transitionTime = 1f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //not being destroyed on scene load
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadNextLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelRoutine(sceneIndex));
    }
    private IEnumerator LoadLevelRoutine(int sceneIndex)
    {
        _transitionImage.fillClockwise = true;
        float timer = 0f;
        _transitionImage.fillAmount = 0f;
        _transitionImage.raycastTarget = true; //block user input during transition
        
        while (timer < _transitionTime)
        {
            timer += Time.unscaledDeltaTime;
            _transitionImage.fillAmount = timer / _transitionTime;
            yield return null; //wait for next frame
        }
        _transitionImage.fillAmount = 1f;
        //map load in the background
        yield return new WaitForSecondsRealtime(0.01f);

        //start the load
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //wait until load is complete
        while (!operation.isDone)
        {
            yield return null;
        }

        //transition back
        Time.timeScale = 1f; //ensure time is normal
        _transitionImage.fillClockwise = false;
        timer = 0f;
        while (timer < _transitionTime)
        {
            timer += Time.unscaledDeltaTime;
            _transitionImage.fillAmount = 1f - (timer / _transitionTime);
            yield return null; //wait for next frame
        }

        _transitionImage.fillAmount = 0f;
        _transitionImage.raycastTarget = false; //unblock user input during transition
    }
}
