using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct RankData 
{
    public int _rank;
    public string _id;
    public int _score;
    public RankData(int rank, string id, int score)
    {
        _rank = rank;
        _id = id;
        _score = score;
    }
}
