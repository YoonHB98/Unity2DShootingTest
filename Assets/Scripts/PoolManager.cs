using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    static GameObject _folder;
    static Dictionary<int, List<GameObject>> _dic = new Dictionary<int, List<GameObject>>();

    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if(_folder == null)
        {
            _folder = new GameObject("Pool");
        }
        if(_dic.ContainsKey(prefab.GetInstanceID()))
        {
            List<GameObject> _list = _dic[prefab.GetInstanceID()];
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].activeSelf == false)
                {
                    GameObject clone = _list[i];
                    clone.transform.position = pos;
                    clone.transform.rotation = rot;
                    clone.SetActive(true);
                    return clone;
                }
            }
            GameObject newClone = GameObject.Instantiate(prefab, pos, rot);
            _list.Add(newClone);
            _dic[prefab.GetInstanceID()] = _list;
            newClone.transform.parent = _folder.transform;
            return newClone;
        }
        else
        {
            List<GameObject> _list = new List<GameObject>();
            GameObject newClone = GameObject.Instantiate(prefab, pos, rot);
            _list.Add(newClone);
            _dic.Add(prefab.GetInstanceID(), _list);
            newClone.transform.parent = _folder.transform;
            return newClone;
        }
    }
      



    public static void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }
}

