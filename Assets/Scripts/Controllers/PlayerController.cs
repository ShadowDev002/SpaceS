using UnityEngine;

public class PlayerController : SpaceShip
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private RectTransform _cursorUITransform;

    private Vector2 _currentMouseScreenPos;
    
    private void Start()
    {
        if(Camera.main != null) _mainCam = Camera.main;
        if (_inputHandler == null) _inputHandler = GetComponent<InputHandler>();
    }
    private void Update()
    {
        HandleMouseInput();
    }
    private void LateUpdate()
    {
        HandleCursorInput();
    }
    protected override void Move()
    {
        float targetSpeed = _inputHandler != null && _inputHandler.IsSprinting ? _speedPlus : _speed;
        Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
        _rb.linearVelocity = direction * targetSpeed;
    }

    private void HandleMouseInput()
    {
        if(_inputHandler == null) return;

        _currentMouseScreenPos = _inputHandler.Look;

        //convert to world position
        Vector3 worldPosV3 = _mainCam.ScreenToWorldPoint(new Vector3(_currentMouseScreenPos.x, _currentMouseScreenPos.y, -_mainCam.transform.position.z));
        Vector2 worldPosV2 = new Vector2(worldPosV3.x, worldPosV3.y);
        
        SetTargetPosition(worldPosV2);
    }
    private void HandleCursorInput()
    {
        if(_cursorUITransform == null) return;

        _cursorUITransform.position = _currentMouseScreenPos;
    }
}
