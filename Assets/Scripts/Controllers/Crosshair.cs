using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;

    void OnEnable()
    {
        if (_crosshairImage == null) 
        {
            _crosshairImage = GetComponentInChildren<Image>(); 
        }

        LoadColor();
    }

    private void LoadColor()
    {
        if (_crosshairImage == null) return;

        float r = PlayerPrefs.GetFloat("CrosshairColorR", 1f);
        float g = PlayerPrefs.GetFloat("CrosshairColorG", 1f);
        float b = PlayerPrefs.GetFloat("CrosshairColorB", 1f);

        _crosshairImage.color = new Color(r, g, b, 1f);
    }
}
