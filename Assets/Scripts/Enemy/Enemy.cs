using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;
    private Player _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
        {
            _health -= bullet.Damage;

            if (_health <= 0)
                Destroy(this.gameObject);
        }
    }
}
