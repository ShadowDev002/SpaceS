using UnityEngine;

public class Laser_Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 40f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _hitLayer; //what to asign hit

    private float _lifeTimer;

    public void Activate(Vector3 spawnPos, Quaternion spawnRot)
    {
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        _lifeTimer = _lifeTime;
        gameObject.SetActive(true);
    } 
    void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, moveDistance, _hitLayer);
        if (hit2D.collider != null)
        {
            HandleHit(hit2D.collider);
            transform.position = hit2D.point;
            //gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(Vector3.up * moveDistance);
        }
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    void HandleHit(Collider2D other)
    {
        if(other.CompareTag("Player")) return;

        //if the hit object can be damaged
        if(other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_damage);

            gameObject.SetActive(false);
        }
    }
}
