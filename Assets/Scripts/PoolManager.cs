using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    static GameObject _folder;
    static List<GameObject> _list = new List<GameObject>();

    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if(_folder == null)
        {
            _folder = new GameObject("Pool");
        }
        for(int i = 0; i < _list.Count; i++)
        {
            if (_list[i].activeSelf == false)
            {
                GameObject clone = _list[i];
                clone.transform.position = pos;
                clone.transform.rotation = rot;
                clone.SetActive(true);
                return _list[i];
            }
        }
        GameObject newClone = GameObject.Instantiate(prefab, pos, rot);
        _list.Add(newClone);
        newClone.transform.SetParent(_folder.transform);
        return newClone;
    }

    public static void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }
}

