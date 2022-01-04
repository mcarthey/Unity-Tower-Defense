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
    [SerializeField] private float _delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float _delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float _minRandomDelay;
    [SerializeField] private float _maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;

    private ObjectPooler _pooler;
    private Waypoint _waypoint;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _waypoint = GetComponent<Waypoint>();

        _enemiesRemaining = _enemyCount;
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
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;

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

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(_delayBtwWaves);
        _enemiesRemaining = _enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy()
    {
        _enemiesRemaining--;

        if (_enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }


}
