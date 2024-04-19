using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueRanking : MonoBehaviour
{
    public menuRanking[] _rankDataList;
    UserData playerData;

    private void OnEnable()
    {
        string data = PlayerPrefs.GetString("UserData");
        if (string.IsNullOrEmpty(data) == false)
        {
            playerData = SaveData.Deserialize<UserData>(data);
            playerData.SortList();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < _rankDataList.Length; i++)
        {
            if (playerData._list.Count > i)
            {
                RankData data = playerData._list[i];
                data._rank = i + 1;
                playerData._list[i] = data;
                _rankDataList[i].init(playerData._list[i]);
            }
            else
            {
                _rankDataList[i].HideUI();
            }

        }
    }
}
