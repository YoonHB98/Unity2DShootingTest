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
        UserData ud = GameManager.instance._userData;
        GameManager.instance._userData._list.Add(new RankData(1, _editText.text, GameManager.instance._score));

        bool isLike = false;
        for(int i = 0; i < ud._list.Count; i++)
        {
            if (ud._list[i]._id.Equals(_editText.text))
            {
                if (ud._list[i]._score < GameManager.instance._score)
                {
                    ud._list[i] = new RankData(1, _editText.text, GameManager.instance._score);
                }
                isLike = true;
                break;
            }
            if(isLike == false)
            {
                GameManager.instance._userData._list.Add(new RankData(1, _editText.text, GameManager.instance._score));
            }
        }


        string data = SaveData.ObjectToStringSerialize(GameManager.instance._userData);
        PlayerPrefs.SetString("UserData", data);

        gameObject.SetActive(false);
        _menuRank.SetActive(true);
    }
}
