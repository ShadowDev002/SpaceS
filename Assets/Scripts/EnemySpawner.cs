using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private SimpleEnemy _enemyPrefab;
    [SerializeField] private float _spawnRate = 2f; //ms
    [SerializeField] private int _poolSize = 20;

    [Header("Spawn Area")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minSpawnDistance = 10f;
    [SerializeField] private float _maxSpawnDistance = 20f;

    private float _spawnTimer;
    private List<SimpleEnemy> _enemyPool;

    void Start()
    {
        if (_playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) _playerTransform = player.transform;
        }
        InitPool();
    }
    
    void Update()
    {
        if(_playerTransform == null) return;
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= _spawnRate)
        {
            SpawnEnemy();
            _spawnTimer = 0f;
        }
    }
    
    private void InitPool()
    {
        _enemyPool = new List<SimpleEnemy>();
        GameObject poolParent = new GameObject("EnemyPool");
        for(int i = 0; i < _poolSize; i++)
        {
            SimpleEnemy obj = Instantiate(_enemyPrefab, poolParent.transform);
            obj.gameObject.SetActive(false);
            _enemyPool.Add(obj);
        }
    }
    
    private void SpawnEnemy()
    {
        SimpleEnemy enemy = GetPooledEnemy();
        if(enemy == null) return;

        //random direction
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        //random distance
        float randomDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);

        //final position
        Vector2 spawnPosition = (Vector2)_playerTransform.position + (randomDir * randomDistance);

        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

    private SimpleEnemy GetPooledEnemy()
    {
        foreach (var e in _enemyPool)
        {
            if(!e.gameObject.activeInHierarchy)
            {
                return e;
            }
        }
        return null;
    }
}
