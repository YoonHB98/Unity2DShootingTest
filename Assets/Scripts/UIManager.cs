using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public PlayerController _player;
    public TMP_Text _scoreText;
    public TMP_Text _gameOverText;
    public Button _retryButton;
    public GameObject[] _life;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        _player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        GameManager.instance.onScoreChange += OnScoreChange;
        GameManager.instance.onLifeChange += OnLifeChange;
    }
    private void OnDisable()
    {
        GameManager.instance.onScoreChange -= OnScoreChange;
        GameManager.instance.onLifeChange -= OnLifeChange;
    }
    private void OnScoreChange(int num)
    {
        _scoreText.text = num.ToString();
    }

    private void OnLifeChange(int num)
    {
        for(int i = 0; i < _life.Length; i++)
        {
            if (i < num)
            {
                _life[i].SetActive(true);
            }
            else
            {
                _life[i].SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.onScoreChange += OnScoreChange;
    }
}
