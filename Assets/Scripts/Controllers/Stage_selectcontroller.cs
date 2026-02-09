using UnityEngine;
using UnityEngine.UI;

public class Stage_selectcontroller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _sceneIndexToLoad;

    [SerializeField] private Button _button;

    public void OnButtonClick()
    {
        LevelLoader.Instance.LoadNextLevel(_sceneIndexToLoad);
    }
}
