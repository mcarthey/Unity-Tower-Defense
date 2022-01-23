using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    private TurrentProjectile turrentProjectile;
    void Start()
    {
        turrentProjectile=GetComponent<TurrentProjectile>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTurret();
        }
    }
    void UpgradeTurret()
    {
        turrentProjectile.Damage += damageIncremental;
        turrentProjectile.DelayPerShot -= delayReduce;
    }
}
