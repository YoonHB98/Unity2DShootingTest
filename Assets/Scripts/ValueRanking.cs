using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueRanking : MonoBehaviour
{
    public RankData[] _rankDataList;

    private void OnEnable()
    {
        string data = PlayerPrefs.GetString("UserData");
        UpdateUI();
    }

    void UpdateUI()
    {

    }
}
