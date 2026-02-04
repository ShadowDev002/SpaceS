using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _parallaxFactor;

    private MeshRenderer _meshRenderer;
    private Transform _mainCamTransform;
    
    //cache the property ID
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _mainCamTransform = Camera.main.transform;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_meshRenderer == null) return;

        Vector2 offset = _mainCamTransform.position * _parallaxFactor;
        if (_meshRenderer.material.HasProperty(BaseMap))
        {
            _meshRenderer.material.SetTextureOffset(BaseMap, offset);
        }
    }
}
