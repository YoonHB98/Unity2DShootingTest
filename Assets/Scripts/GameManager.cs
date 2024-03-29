using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public float _spawnTimeMin = 0.0f;
    public float _spawnTimeMax = 0.0f;
    public float _spawnTime = 0.0f;
    public float _randomX = 0.0f;
    public Transform _player;
    public GameObject[] _enemyPrefab;
    public Transform[] _spawnPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }

        _player = FindObjectOfType<PlayerController>().transform;
    }


    void SpawnEnemy()
    {
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0)
        {
            _spawnTime = Random.Range(_spawnTimeMin, _spawnTimeMax);
            int RandspawnPoint = Random.Range(0, _spawnPoint.Length);
            float randomX = Random.Range(-_randomX, _randomX);
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
            Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], respawn.position + new Vector3(randomX, 0, 0), rotate);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
}
