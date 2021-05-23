using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTurret : MonoBehaviour
{
    // [SerializeField] private float attackRange = 3f;

    public Movement CurrentEnemyTarget { get; set; }

    //public Transform partToRotate;

    private bool _gameStarted;
    private List<Movement> _enemies;

    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Movement>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }


        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(-transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);

        // Debug.Log("Rotate");
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Movement newEnemy = other.GetComponent<Movement>();
            _enemies.Add(newEnemy);

            //  Debug.Log("Seen"); // testing
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Movement enemy = other.GetComponent<Movement>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);

                // Debug.Log("Lost Sight"); // testing
            }
        }
    }




    /* private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    */
}
