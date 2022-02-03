using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _turretCardPrefab;

    [SerializeField] private Transform _turretPanelContainer;

    [Header("Turret Settings")]
    [SerializeField] private TurretSettings[] _turrets;


    void Start()
    {
        foreach (var settings in _turrets)
        {
            CreateTurretCard(settings);
        }
    }

    private void CreateTurretCard(TurretSettings turretSettings)
    {
        GameObject newInstance = Instantiate(_turretCardPrefab, _turretPanelContainer.position,
            Quaternion.identity);
        newInstance.transform.SetParent(_turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardButton = newInstance.GetComponent<TurretCard>();
        cardButton.SetupTurretButton(turretSettings);
    }
    
}
