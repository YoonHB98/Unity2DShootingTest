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
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < _rankDataList.Length; i++)
        {
            if(playerData._list.Count > i)
            {
                _rankDataList[i].init(playerData._list[i]);
            }
            else
            {
                _rankDataList[i].HideUI();
            }

        }
    }
}
