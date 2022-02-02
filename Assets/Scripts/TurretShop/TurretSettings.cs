using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Shop Setting")]
public class TurretSettings : ScriptableObject
{
    [SerializeField] private GameObject TurretPrefab;
    [SerializeField] private int TurretShopCost;
    [SerializeField] private Sprite TurretShopSprite;


}
