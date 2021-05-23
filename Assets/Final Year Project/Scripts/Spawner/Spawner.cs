using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum SpawnType
    {
        Fixed,
        Random
    }



    public class Spawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpawnType spawnMode = SpawnType.Fixed;
        [SerializeField] private int enemyCount = 10;
        // [SerializeField] private GameObject testing;
        [SerializeField] private float delayBtwWaves = 1f;


        [Header("Fixed Delay")]
        [SerializeField] private float spawnDelay;

        [Header("Random Delay")]
        [SerializeField] private float minRandomDelay;
        [SerializeField] private float maxRandomDelay;

        private float _spawnTimer;
        private float _enemiesSpawned;
        private int _enemiesLeft;

        private ObjectPool _pooler;
        private Waypoint _waypoint;
     

        // Start is called before the first frame update
        private void Start()
        {
             _pooler = GetComponent<ObjectPool>();
             _waypoint = GetComponent<Waypoint>();

             _enemiesLeft = enemyCount;
        }

        // Update is called once per frame
        void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0)
            {
                _spawnTimer = GetSpawnDelay();
                if (_enemiesSpawned < enemyCount)
                {
                    _enemiesSpawned++;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy() // spawn our enemy
        {
            GameObject newInstance = _pooler.GetInstanceFromPool();
            Movement enemy = newInstance.GetComponent<Movement>();
            enemy.Waypoint = _waypoint;
            enemy.ResetEnemy();

            enemy.transform.localPosition = transform.position;
            newInstance.SetActive(true);
        }

        private float GetSpawnDelay() // fixed delay enemy
        {
            float delay = 0f;

            if (spawnMode == SpawnType.Fixed) // if mode is fixed
            {
                delay = spawnDelay; // delay is equal to spawnDelay
            }
            else
            {
                delay = GetRandomDelay(); //delay is random
            }

            return delay;
        }

        private float GetRandomDelay() // random delay enemy
        {
            float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
            return randomTimer;
        }

        private IEnumerator NextWave()
        {
            yield return new WaitForSeconds(delayBtwWaves);
            GameManager.waveNum += 1;
            _enemiesLeft = enemyCount;
            _spawnTimer = 0f;
            _enemiesSpawned = 0;
        }
    
    
        private void RecordEnemy()
        {
             _enemiesLeft--;
             if(_enemiesLeft <= 0)
             {
                StartCoroutine(NextWave());
             }
        }

    
        private void OnEnable()
        {
            Movement.OnEndReached += RecordEnemy;
            EnemyHealth.OnEnemyKilled += RecordEnemy;
        } 

        private void OnDisable()
        {
            Movement.OnEndReached -= RecordEnemy;
            EnemyHealth.OnEnemyKilled -= RecordEnemy;
        }
    }


