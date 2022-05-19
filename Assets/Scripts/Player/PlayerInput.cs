using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerInput : MonoBehaviour
{
    private Joystick _moveJoystick;
    private Vector2 _moveInput;
    private PlayerMover _playerMover;

    private void Start()
    {
        _moveJoystick = GameObject.Find("MoveJoystick").GetComponent<Joystick>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);

        _playerMover.Move(moveInput);
    }
}
