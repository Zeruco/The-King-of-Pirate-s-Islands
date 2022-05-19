using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private float _offsetY;
    private Player _player;

    private void Start()
    {

    }

    private void Update()
    {
        _player = FindObjectOfType<Player>();
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y - _offsetY, -10);
    }
}
