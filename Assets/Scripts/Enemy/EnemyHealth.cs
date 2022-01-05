using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _barPosition;
    [SerializeField] private float _initialHealth = 10f;
    [SerializeField] private float _maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

    void Start()
    {
        CreateHealthBar();
        CurrentHealth = _initialHealth;

        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / _maxHealth, Time.deltaTime * 10f);
    }
    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(_healthBarPrefab, _barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = _initialHealth;
        _healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        OnEnemyKilled?.Invoke(_enemy);
    }

}
