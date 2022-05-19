using UnityEngine;

public class StandartBullet : Bullet
{
    protected override void Move()
    {
        if (Vector2.Distance(StartPosition, transform.position) < MaxTraveledDistance)
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
