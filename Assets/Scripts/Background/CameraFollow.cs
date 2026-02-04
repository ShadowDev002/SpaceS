using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    public float _smoothSpeed = 0.125f;
    public Vector3 _offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (_target == null) return;
        Vector3 targetPosition = _target.position + _offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);
        transform.position = smoothPosition;
    }
}
