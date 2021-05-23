using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public static Action OnEndReached;

    [SerializeField] private float moveSpeed = 3f; // speed of our enemy
    [SerializeField] private int dealDamage = 1; //dealdamage to palyer health if reach last waypoint;
    // [SerializeField] private Waypoint waypoint;

    public Waypoint Waypoint { get; set; }
    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    
    private EnemyHealth _enemyHealth;


    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _currentWaypointIndex = 0;
        
        EnemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        Move(); // call move function
        Rotate(); // call rotate function

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime); // move towards next waypoint
    }

    private void Rotate()
    {
        transform.LookAt(CurrentPointPosition, (new Vector3(0f, 0f, 90f))); //look at the next point position
        transform.Rotate(-90f, -90f, -360f); // rotate 90f  (flat)
    }
    

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            //_lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1; // last waypoint = waypoint length - 1

        if(_currentWaypointIndex < lastWaypointIndex) // if currentwaypoint is less than last waypoint
        {
            _currentWaypointIndex++; // add one to the index
        }

        else
        {
            EndPointReached(); // else, the enemy reach last waypoint. call endpointreached method
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke();
        _enemyHealth.ResetHealth(); // invoke enemy health and reset health
        ObjectPool.ReturnToPool(gameObject); // return gameobject to the pool
        GameManager.health -= dealDamage;
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}









   /* public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
   */




