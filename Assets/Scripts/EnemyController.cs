using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _speed = 0.0f;
    public int _hp = 0;
    public int _dmg = 1;
    public Rigidbody2D _rb;
    public Transform _myTF;
    public Sprite[] _images;
    public SpriteRenderer _spriteRenderer;
    public GameObject[] _bullet;
    public float _fireRate = 0.0f;
    private float _onfireRate = 0.0f;
    public int _enemyType = 0;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _myTF = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _onfireRate = _fireRate;
        _rb.velocity = -1 * _myTF.up * _speed;
    }

    private void Update()
    {
        Fire();
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            OnHit(bullet._Damage);
            Destroy(collision.gameObject);
        }
    }

    void OnHit(int dmg)
    {
        _hp = _hp - dmg;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
        _spriteRenderer.sprite = _images[1];
        Invoke("ReturnSpriteImage", 0.1f);
    }

    void ReturnSpriteImage()
    {
        _spriteRenderer.sprite = _images[0];
    }

    void Fire()
    {
        if(_enemyType == 0)
        {
            return;
        }
        _fireRate -= Time.deltaTime;

        if (_fireRate <= _onfireRate)
        {
            _fireRate = _fireRate + _onfireRate;
            Vector3 bPosition = _myTF.position;
            Quaternion rotate = Quaternion.identity;
            rotate.eulerAngles = new Vector3(0, 0, 0);
            PlayerController player = GameManager.instance._player.GetComponent<PlayerController>();
            if (GameManager.instance._player != null)
            {
                Vector3 dir = player.transform.position - bPosition;
                rotate.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + new Vector3(0, 0, 270);
            }
            switch (_enemyType)
            {
                case 1:
                    Instantiate(_bullet[0], bPosition, rotate);
                    break;
                case 2:
                    Instantiate(_bullet[1], bPosition, rotate);
                    break;
            }

        }
    }
}
