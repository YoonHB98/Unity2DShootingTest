using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static PlayerController player;
    public GameObject _menuGameOver;
    public GameObject _oBoomEffect;


    public float _spawnTimeMin = 0.0f;
    public float _spawnTimeMax = 0.0f;
    public float _spawnTime = 0.0f;
    public float _randomX = 0.0f;
    public Transform _player;
    public GameObject[] _enemyPrefab;
    public Transform[] _spawnPoint;
    private bool RespawnFlag = true;
    WaitForSeconds waitTime02 = new WaitForSeconds(0.2f);
    public event Action<int> onScoreChange;
    public int _score;
    public event Action<int> onLifeChange;
    public int _life;
    public UserData _userData;
    public float _currenttime;

    void Awake()
    {
        _life = 3;
        _score = 0;
        _menuGameOver.SetActive(false);
        instance = this;
        player = FindObjectOfType<PlayerController>();
        _player = player.transform;
    }


    void SpawnEnemy()
    {
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0)
        {
            _spawnTime = UnityEngine.Random.Range(_spawnTimeMin, _spawnTimeMax);
            int RandspawnPoint = UnityEngine.Random.Range(0, _spawnPoint.Length);
            float randomX = UnityEngine.Random.Range(-_randomX, _randomX);
            //Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], _spawnPoint[RandspawnPoint].position + new Vector3(randomX, 0, 0), _spawnPoint[RandspawnPoint].rotation);
            Quaternion rotate = Quaternion.identity;
            rotate.eulerAngles = new Vector3(0, 0, 0);
            Transform respawn = _spawnPoint[RandspawnPoint];
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                Vector3 dir = player.transform.position - respawn.position;
                rotate.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + new Vector3(0,0,90);
            }
            Instantiate(_enemyPrefab[UnityEngine.Random.Range(0, _enemyPrefab.Length)], respawn.position + new Vector3(randomX, 0, 0), rotate);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _currenttime = Time.time + UnityEngine.Random.Range(2.0f, 4.0f);
        onScoreChange?.Invoke(_score);
        HideBoomEffect();

        string data = PlayerPrefs.GetString("UserData");
        if (string.IsNullOrEmpty(data) == false)
        {
            _userData = SaveData.Deserialize<UserData>(data);
        }
        else
        {
            _userData = new UserData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
        if (player.gameObject.activeSelf == false && _life >= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        //3초 후에 부활
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        if (RespawnFlag == false)
        {
            yield break;
        }
        if (RespawnFlag)
        {
            RespawnFlag = false;
        }

        RespawnPlayer();
        yield return new WaitForSeconds(3.0f);
        player.gameObject.SetActive(true);
        player._playerHp = 1;
        player.transform.position = new Vector3(0, -3.2f, 0);
        RespawnFlag = true;
        player.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(RespawnEffectCoroutine());
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator RespawnEffectCoroutine()
    {
        float count = 0;
        while (count < 2.0f)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
            yield return waitTime02;
            player.GetComponent<SpriteRenderer>().enabled = true;
            yield return waitTime02;
            count += 0.4f;
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        if(onScoreChange != null)
        {
            onScoreChange.Invoke(_score);
        }
    }

    public void RespawnPlayer()
    {
        _life--;
        if (_life < 0)
        {
            _menuGameOver.SetActive(true);
        }
        else
        {
            onLifeChange?.Invoke(_life);
            player.gameObject.SetActive(false);
        }
    }

    public void ButtonAct_Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowBoomEffect()
    {
        _oBoomEffect.SetActive(true);
        EnemyClearAll();
        Invoke("HideBoomEffect", 1.0f);
    }

    private void HideBoomEffect()
    {
        _oBoomEffect.SetActive(false);
    }

    private void EnemyClearAll()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyController>().OnHit(100);
        }
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
    }
}


