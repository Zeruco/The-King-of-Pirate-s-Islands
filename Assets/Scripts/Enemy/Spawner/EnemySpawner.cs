using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    private EnemySpawnPoint[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();

        Spawn();
    }

    private void Spawn()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            var spawnedEnemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
        }
    }
}
