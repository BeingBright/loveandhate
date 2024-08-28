using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int objectCount = 3;

    private List<GameObject> _pool;
    private int _index = 0;

    private void Awake()
    {
        _pool = new List<GameObject>(objectCount);
        for (var i = 0; i < objectCount; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            obj.layer = gameObject.layer;
            _pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        _index++;
        if (_index >= _pool.Count)
        {
            _index = 0;
        }
        
        var obj = _pool[_index];
        obj.SetActive(true);
        return obj;
    }

    public void Destroy(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = transform.position;
    }
}