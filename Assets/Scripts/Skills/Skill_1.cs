using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_1 : MonoBehaviour
{

    [Header("Skill Settings")]
    [SerializeField] private float _expansionSpeed = 5f;
    [SerializeField] private float _maxSize = 20f;
    [SerializeField] private int _damage = 50;
    [SerializeField] public float _coolDown = 5f;
    public float CooldownTimeSkil_1Global => _coolDown;


    //list for not to damage the same enemy multiple times
    private List<GameObject> _hitEnemies = new List<GameObject>();

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        _hitEnemies.Clear();
    }

    void Update()
    {
        transform.localScale += Vector3.one * _expansionSpeed * Time.deltaTime;
        if (transform.localScale.x >= _maxSize)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_hitEnemies.Contains(other.gameObject)) return;
        //se if its damageable
        if(other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_damage);
            _hitEnemies.Add(other.gameObject);
        }
    }
}
