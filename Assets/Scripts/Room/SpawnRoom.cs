using UnityEngine;

public class SpawnRoom : Room
{
    [SerializeField] private Player _player;

    private void Start()
    {
        Instantiate(_player, transform.position, Quaternion.identity);
    }
}
