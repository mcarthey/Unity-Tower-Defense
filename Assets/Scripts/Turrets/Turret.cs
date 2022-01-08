using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;

    private bool _gameStarted;
    private List<Enemy> _enemies;

    public Enemy CurrentEnemyTarget { get; set; }

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();
    }

    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null) return;

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    private void GetCurrentEnemyTarget()
    {
        CurrentEnemyTarget = _enemies.FirstOrDefault();

        //if (_enemies.Count <= 0)
        //{
        //    CurrentEnemyTarget = null;
        //    return;
        //}

        //CurrentEnemyTarget = _enemies[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }

}
