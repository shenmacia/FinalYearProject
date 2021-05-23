using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool1 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _pool; // list of gameObject which is under pool
    private GameObject _poolContainer;

    private void Awake()
    {
        _pool = new List<GameObject>(); // initialise a new list
        _poolContainer = new GameObject($"Pool - {prefab.name}");
        CreatePooler();

    }

    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }


    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab); // when new gameobject created, spawn prefab
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }


    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy) // if our pool is not in the hierarchy
            {
                return _pool[i]; // return pool instance
            }
        }

        return CreateInstance(); // if all is being used
    }

    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }

}
