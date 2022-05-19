using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] protected float Speed;
    [SerializeField] protected float MaxTraveledDistance;

    public float Damage => _damage;

    protected Vector2 StartPosition;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
