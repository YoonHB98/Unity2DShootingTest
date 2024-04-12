using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float _speed = 0.0f;
    public Vector2 _minBoundary = Vector2.zero;
    public Vector2 _maxBoundary = Vector2.zero;
    public Animator _anim;
    public Transform[] _bullet;
    public Transform[] _bullets;
    public Transform _myTF;
    public int _playerLevel = 0;
    public float _fireRate = 0.0f;
    private float _onfireRate = 0.0f;
    public int _playerHp = 0;

    private void Awake()
    {
        _onfireRate = _fireRate;
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 nextPos = _myTF.position + new Vector3(h, v, 0) * _speed * Time.deltaTime;
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            _anim.SetInteger("Input", (int)h);
        }
        
        nextPos.x = Mathf.Clamp(nextPos.x, _minBoundary.x, _maxBoundary.x);
        nextPos.y = Mathf.Clamp(nextPos.y, _minBoundary.y, _maxBoundary.y);

        _myTF.position = nextPos;

        if (Input.GetButton("Fire1"))
        {
            Fire2();
        }
        else
        {
            _fireRate = _fireRate - Time.deltaTime;
            if (_fireRate <= 0)
            {
                _fireRate = 0;
            }
        }

    }

    private void Fire(int level)
    {
        _fireRate -= Time.deltaTime;
 
        if (_fireRate <= 0)
        {
            
            Vector3 bPosition = _myTF.position;
            bPosition.x -= level * 0.16f;
            for (int i = 0; i < level; i++)
            {
                bPosition.x += i * 0.16f;
                _fireRate += 0.2f;
                Instantiate(_bullet[0], bPosition, Quaternion.identity);
            }
        }


    }

    private void Fire2()
    {
        _fireRate -= Time.deltaTime;

        if (_fireRate <= 0)
        {
            Vector3 bPosition = _myTF.position;
            _fireRate = _fireRate + _onfireRate;
            switch (_playerLevel)
            {
                case 1:
                    PoolManager.Spawn(_bullet[0].gameObject, bPosition, Quaternion.identity);
                    break;
                case 2:
                    PoolManager.Spawn(_bullet[0].gameObject, bPosition + new Vector3(-0.12f, 0, 0), Quaternion.identity);
                    PoolManager.Spawn(_bullet[0].gameObject, bPosition + new Vector3(0.12f, 0, 0), Quaternion.identity);
                    break;
                case 3:
                    PoolManager.Spawn(_bullet[1].gameObject, bPosition, Quaternion.identity);
                    PoolManager.Spawn(_bullet[0].gameObject, bPosition +new Vector3(0.25f,0,0), Quaternion.identity);
                    PoolManager.Spawn(_bullet[0].gameObject, bPosition +new Vector3(-0.25f, 0, 0), Quaternion.identity);
                    break;
            }
            
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Enemy":
                EnemyController enemy = collision.GetComponent<EnemyController>();
                _playerHp = _playerHp - enemy._dmg;
                Destroy(collision.gameObject);
                if (_playerHp <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;
            case "EnemyBullet":
                Bullet bullet = collision.GetComponent<Bullet>();
                _playerHp = _playerHp - bullet._Damage;
                Destroy(collision.gameObject);
                if (_playerHp <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;
            case "Item":
                ItemController item = collision.GetComponent<ItemController>();
                if (item != null)
                {
                    switch (item._itemType)
                    {
                        case ItemType.Power:
                            if (_playerLevel < 3)
                            {
                                _playerLevel++;
                            }
                            Destroy(collision.gameObject);
                            break;
                        case ItemType.Coin:
                            GameManager.instance.AddScore(100);
                            Destroy(collision.gameObject);
                            break;
                        case ItemType.Bomb:
                            GameManager.instance.ShowBoomEffect();
                            Destroy(collision.gameObject);
                            break;

                    }
                }
            break;
        }

    }

}
