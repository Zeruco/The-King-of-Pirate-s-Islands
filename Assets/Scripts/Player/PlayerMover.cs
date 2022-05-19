using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _player;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveInput)
    {
        Vector2 moveVelocity = moveInput.normalized * _speed;

        _player.MovePosition(_player.position + moveVelocity * Time.deltaTime);
    }
}
