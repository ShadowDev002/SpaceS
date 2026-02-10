using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _redSlider;
    [SerializeField] private Slider _greenSlider;
    [SerializeField] private Slider _blueSlider;
    [SerializeField] private Image _colorPreviewImage;

    void Start()
    {
        _redSlider.value = PlayerPrefs.GetFloat("CrosshairColorR", 1f) * 255f;
        _greenSlider.value = PlayerPrefs.GetFloat("CrosshairColorG", 1f) * 255f;
        _blueSlider.value = PlayerPrefs.GetFloat("CrosshairColorB", 1f) * 255f;


        UpdateColorPreview();

        //adding listeners
        _redSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        _greenSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
        _blueSlider.onValueChanged.AddListener(delegate { UpdateColorPreview(); });
    }
    private void UpdateColorPreview()
    {
        float r = _redSlider.value / 255f;
        float g = _greenSlider.value / 255f;
        float b = _blueSlider.value / 255f;
        
        Color newColor = new Color(r, g, b, 1);
        if (_colorPreviewImage != null) _colorPreviewImage.color = newColor;

        //save the color values to PlayerPrefs
        PlayerPrefs.SetFloat("CrosshairColorR", r);
        PlayerPrefs.SetFloat("CrosshairColorG", g);
        PlayerPrefs.SetFloat("CrosshairColorB", b);
        PlayerPrefs.Save();   
    }
}
