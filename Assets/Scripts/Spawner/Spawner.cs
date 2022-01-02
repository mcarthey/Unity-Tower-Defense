using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawnModes _spawnMode = SpawnModes.Fixed;
    [SerializeField] private int _enemyCount = 10;
    [SerializeField] private GameObject _testGameObject;

    [Header("Fixed Delay")]
    [SerializeField] private float _delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float _minRandomDelay;
    [SerializeField] private float _maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;

    private ObjectPooler _pooler;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();

            if (_enemiesSpawned < _enemyCount)
            {
                SpawnEnemy();
                _enemiesSpawned++;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay;
        switch (_spawnMode)
        {
            case SpawnModes.Fixed:
                delay = _delayBtwSpawns;
                break;
            case SpawnModes.Random:
                delay = Random.Range(_minRandomDelay, _maxRandomDelay);
                break;
            default:
                delay = 0.5f;
                break;
        }

        return delay;
    }
}
