using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameOver : MonoBehaviour
{
    public TMP_InputField _editText;
    public GameObject _menuRank;

    public void ButtonAct_Save()
    {
        GameManager.instance._userData._list.Add(new RankData(1, _editText.text, GameManager.instance._score));
        string data = SaveData.ObjectToStringSerialize(GameManager.instance._userData);
        PlayerPrefs.SetString("UserData", data);

        gameObject.SetActive(false);
        _menuRank.SetActive(true);
    }
}
