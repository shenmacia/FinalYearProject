using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected float delayAttack = 1f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float _nextAttack;
    protected ObjectPool _pooler;
    protected ArcherTurret _turret;
    protected ProjectileArrow _currentProjectileLoaded;

    private void Start()
    {
        _turret = GetComponent<ArcherTurret>();
        _pooler = GetComponent<ObjectPool>();

        Damage = damage;
        DelayPerShot = delayAttack;
        LoadArrow();
    }

    private void Update()
    {
        if (IsTurretEmpty())
        {
            LoadArrow();
        }

        if (Time.time > _nextAttack)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null
           && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }

            _nextAttack = Time.time + DelayPerShot;
        }
    }


    private void LoadArrow()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);

        _currentProjectileLoaded = newInstance.GetComponent<ProjectileArrow>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.Damage = Damage;
        newInstance.SetActive(true);
    }

    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }


}
