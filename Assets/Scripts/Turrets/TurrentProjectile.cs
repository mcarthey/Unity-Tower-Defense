using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentProjectile : MonoBehaviour
{
    [SerializeField] protected Transform _projectileSpawnPosition;
    [SerializeField] protected float _delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    protected float _nextAttackTime;
    protected ObjectPooler _pooler;
    protected Projectile _currentProjectileLoaded;
    protected Turret _turret;
    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    private void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler>();

        Damage = damage;
        DelayPerShot = _delayBtwAttacks;

        LoadProjectile();
    }

    protected virtual void Update()
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

            _nextAttackTime = Time.time + DelayPerShot;
        }
    }


    protected virtual void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = _projectileSpawnPosition.position;
        newInstance.transform.SetParent(_projectileSpawnPosition);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.Damage = Damage;
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
