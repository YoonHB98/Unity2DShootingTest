using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UserData
{
    public List<RankData> _list;

    public UserData()
    {
        _list = new List<RankData>();
    }
}
