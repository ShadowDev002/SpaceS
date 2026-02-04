using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    //[SerializeField] private PlayerController _playerController;

    [Header("Shooting settings")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] Laser_Projectile _projectilePrefab;
    [SerializeField] private int _poolSize = 50;
    
    [Header("Audio")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioSource _audioSource;
    
    [Header("Skills")]
    [SerializeField] private Skill_1 _skill1Prefab;
    

    private float _lastFireTime;
    private float _lastSkill1Time;
    private List<Laser_Projectile> _projectilePool;

    void Start()
    {
        _inputHandler = GetComponent<InputHandler>();
        //_playerController = GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();

        if (_inputHandler != null)
        {
            _inputHandler.OnSkill_1 += HandleSkill1Input;
        }

        CreatePool();
    }

    private void OnDestroy()
    {
        if (_inputHandler != null)
        {
            _inputHandler.OnSkill_1 -= HandleSkill1Input;
        }
    }

    void Update()
    {
        if (_inputHandler.IsFiring && !PauseController.Instance.IsGamePaused)
        {
            HandleShootInput();
        }
    }

    private void CreatePool()
    {
        _projectilePool = new List<Laser_Projectile>();
        GameObject projectileParent = new GameObject("ProjectileParent");
        for (int i = 0; i < _poolSize; i++)
        {
            Laser_Projectile obj = Instantiate(_projectilePrefab, projectileParent.transform);
            obj.gameObject.SetActive(false);
            _projectilePool.Add(obj);
        }
    }
    private void HandleShootInput()
    {
        if(Time.time < _lastFireTime + _fireRate) return;
        _lastFireTime = Time.time;
        Shoot();
    }
    private void Shoot()
    {
        Debug.Log("Pew");

        //get bullet from pool
        Laser_Projectile projectile = GetPooledProjectile();
        if(projectile == null) return;

        //direction
        Quaternion fireRotation = _firePoint.rotation;
        projectile.Activate(_firePoint.position, fireRotation);

        //audio
        PlayShootSound();
    }

    private void PlayShootSound()
    {
        if (_shootSound != null && _audioSource != null)
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f); //Or if no randomize 1f;
            _audioSource.PlayOneShot(_shootSound);
        }
    }

    private Laser_Projectile GetPooledProjectile()
    {
        foreach (var p in _projectilePool)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }
        return null;
    }
    private void HandleSkill1Input()
    {
        //cooldown check
        if (Time.time < _lastSkill1Time + _skill1Prefab.CooldownTimeSkil_1Global)
        {
            Debug.Log("Skill 1 on Cooldown");
            return;
        }
        
        ActivateSkill1();
        _lastSkill1Time = Time.time;
    }
    private void ActivateSkill1()
    {
        Debug.Log("Skill 1 Activated");
        Instantiate(_skill1Prefab, transform.position, Quaternion.identity);
    }
}