using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class UserData
{
    public List<RankData> _list;

    public UserData()
    {
        _list = new List<RankData>();
    }

    public void SortList()
    {
        _list = _list.OrderByDescending(i=>i._score).ToList();
    }

}
