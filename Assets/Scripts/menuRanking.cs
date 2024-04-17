using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class menuRanking : MonoBehaviour
{
    public TMP_Text _tRank;
    public TMP_Text _tID;
    public TMP_Text _tScore;

    RankData _rank;
    public void init(RankData rank)
    {
        gameObject.SetActive(true);
        _rank = rank;
        UpdateUI();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        _tRank.text = _rank._rank.ToString();
        _tID.text = _rank._id;
        _tScore.text = _rank._score.ToString();
    }
}
