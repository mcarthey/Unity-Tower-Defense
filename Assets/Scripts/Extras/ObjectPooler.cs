using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 10;

    private List<GameObject> _pool;
    private GameObject _poolContainer;

    private void Awake()
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"Pool - {_prefab.name}");
        CreatePooler();
    }
    private void CreatePooler()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(_prefab);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);

        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        // TODO Convert to Dictionary<> to improve performance
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }

        return CreateInstance();
    }
}
