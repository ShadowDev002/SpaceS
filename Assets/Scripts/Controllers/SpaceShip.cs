using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SpaceShip : MonoBehaviour
{
    [SerializeField] protected float _speed = 10f;
    [SerializeField] protected float _speedPlus = 15f;
    [SerializeField] protected float _rotationSpeed = 10f; //lerp speed

    protected Rigidbody2D _rb;
    protected Vector2 _targetPosition; //cursor
    protected bool _isMoving;
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    //physics always in fixed update
    protected virtual void FixedUpdate()
    {
        //distance between the ship and the cursor
        float distance = Vector2.Distance(transform.position, _targetPosition);
        
        if (distance > 0.1f && _isMoving)
        {
            Move();
            Rotate();
        }
        else
        {
            _rb.linearVelocity = transform.up * _speed;
        }
    }

    public void SetTargetPosition(Vector2 position)
    {
        _targetPosition = position;
        _isMoving = true;
    }

    protected virtual void Move()
    {
        Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
        _rb.linearVelocity = direction * _speed;
    }

    protected virtual void Rotate()
    {
        Vector2 direction = _targetPosition - (Vector2)transform.position;
        if (direction.sqrMagnitude > 0.001f)
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetAngle -= 90f;
            
            //slerp between current angle and target angle
            //Quaternion targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, _rotationSpeed *Time.fixedDeltaTime);
        
            float smoothAngle = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, _rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(smoothAngle);
        }
    }
}
