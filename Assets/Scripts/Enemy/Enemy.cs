using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float _moveSpeed = 3f;

    public Waypoint Waypoint { get; set; }
    public float MoveSpeed { get; set; }

    private int _currentWaypointIndex;
    private EnemyHealth _enemyHealth;

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private void Start()
    {
        _currentWaypointIndex = 0;
        MoveSpeed = _moveSpeed;

        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        Move();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = _moveSpeed;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);

    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;

        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnInstanceToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
