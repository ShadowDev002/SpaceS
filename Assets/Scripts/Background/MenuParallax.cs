using UnityEngine;

public class MenuParallax : MonoBehaviour
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
        //camera current y rotation and normalize it
        float currentYRotation = _mainCamTransform.rotation.eulerAngles.y;
        if (currentYRotation > 180)
        {
            currentYRotation -= 360;
        }
        //calculate offset 
        Vector2 offset = new Vector2(currentYRotation * _parallaxFactor, 0);
        if (_meshRenderer.material.HasProperty(BaseMap))
        {
            _meshRenderer.material.SetTextureOffset(BaseMap, offset);
        }
    }
}
