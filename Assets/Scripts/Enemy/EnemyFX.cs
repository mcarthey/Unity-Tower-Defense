using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        Projectile.OnEnemyHit += OnEnemyHit;
    }

    private void OnEnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgTxt;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= OnEnemyHit;
    }
}
