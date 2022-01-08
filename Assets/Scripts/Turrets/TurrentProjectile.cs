using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentProjectile : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPosition;

    private ObjectPooler _pooler;
    private Projectile _currentProjectileLoaded;
    private Turret _turret;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadProjectile();
        }

        if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
        {
            _currentProjectileLoaded.transform.parent = null;
            _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
        }
    }


    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = _projectileSpawnPosition.position;
        newInstance.transform.SetParent(_projectileSpawnPosition);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        newInstance.SetActive(true);
    }

    

}
