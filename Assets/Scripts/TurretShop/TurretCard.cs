using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
    [SerializeField] private Image _turretImage;

    [SerializeField] private TextMeshProUGUI _turretCost;

    public void SetupTurretButton(TurretSettings turretSettings)
    {
        _turretImage.sprite = turretSettings.TurretShopSprite;
        _turretCost.text = turretSettings.TurretShopCost.ToString();
    }
}
