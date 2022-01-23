using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float _moveSpeed = 10f;
    [SerializeField] protected float _damage = 2f;
    [SerializeField] private float _minDistanceToDealDamage = 0.1f;

    public TurrentProjectile TurretOwner { get; set; }
    protected Enemy _enemyTarget;

    protected virtual void Update()
    {
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }
    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position,
            _moveSpeed * Time.deltaTime);
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget < _minDistanceToDealDamage)
        {
            OnEnemyHit?.Invoke(_enemyTarget, _damage);

            _enemyTarget.EnemyHealth.DealDamage(_damage);
            TurretOwner.ResetTurretProjectile();
            ObjectPooler.ReturnInstanceToPool(gameObject);
        }
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

    public void ResetProjectile()
    {
        _enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
