using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncrement;
    [SerializeField] private float damageIncrement;
    [SerializeField] private float delayReduction;

    private ArrowProjectile _turretProjectile;


    // Start is called before the first frame update
    void Start()
    {
        _turretProjectile = GetComponent<ArrowProjectile>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTurret();
        }
    }
    
    private void UpgradeTurret()
    {
        _turretProjectile.Damage += damageIncrement;
        _turretProjectile.DelayPerShot -= delayReduction;
    }
}
