using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentProjectile : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPosition;
    [SerializeField] private float _delayBtwAttacks = 2f;

    private float _nextAttackTime;
    private ObjectPooler _pooler;
    private Projectile _currentProjectileLoaded;
    private Turret _turret;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler>();

        LoadProjectile();
    }

    private void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + _delayBtwAttacks;
        }
    }


    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = _projectileSpawnPosition.position;
        newInstance.transform.SetParent(_projectileSpawnPosition);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        newInstance.SetActive(true);
    }

    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }

}
