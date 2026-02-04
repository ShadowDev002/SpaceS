using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected float _movementSpeed = 2f;
    //[SerializeField] protected float _collisionDMG = 10;

    [Header("UI")]
    [SerializeField] protected Healthbar _healthBar;

    protected int _currentHealth;
    protected Transform _playerTarget;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _playerTarget = player.transform;
        if (_healthBar != null) _healthBar.UpdateHealthbar(_currentHealth, _maxHealth);
    }

    protected virtual void OnEnable() 
    {
        _currentHealth = _maxHealth;
        if (_healthBar != null) _healthBar.UpdateHealthbar(_currentHealth, _maxHealth);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (_playerTarget == null) return;
        //simple follow logic
        Vector2 direction = (_playerTarget.position - transform.position).normalized;
        _rb.linearVelocity = direction * _movementSpeed;

        //rotation towards the player
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //_rb.rotation = angle;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if(_healthBar != null) _healthBar.UpdateHealthbar(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    protected virtual void Death()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        //sounds or effect
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //dmg
        }        
    }
}
