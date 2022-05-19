using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _startTimeBetweenShoots;
    [SerializeField] private float _offset;

    private Joystick _joystick;
    private float rotZ;
    private Vector2 diff;

    private float timeBetweenShoots;

    private void Start()
    {
        _joystick = GameObject.Find("ShootJoystick").GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        rotZ = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + _offset);

        if (timeBetweenShoots <= 0)
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
                Shoot();
        }
        else
            timeBetweenShoots -= Time.deltaTime;
    }
    private void Shoot()
    {
        Instantiate(_bullet, _shootPoint.position, transform.rotation);
        timeBetweenShoots = _startTimeBetweenShoots;
    }
}
