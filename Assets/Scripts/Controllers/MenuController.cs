using UnityEngine;

public class MenuController : MonoBehaviour
{
    //[SerializeField] private GameObject _mainMenu;
    //[SerializeField] private GameObject _stageSelectMenu;
    [SerializeField] private Animator _CameraHolder;
    
    public void OpenStageSelectMenu()
    {
        _CameraHolder.SetTrigger("ToStage");
    }

    public void CloseStageSelectMenu()
    {
        _CameraHolder.SetTrigger("ToMain");
    }

    public void OpenSettingsMenu()
    {
        _CameraHolder.SetTrigger("ToSettings");
    }

    public void CloseSettingsMenu()
    {
        //_CameraHolder.SetTrigger("ToMainfromSettings");
        _CameraHolder.SetTrigger("a2");
        Debug.Log("a2 megnyomva");
    }
}
