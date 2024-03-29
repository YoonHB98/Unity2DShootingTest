using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _speed = 0.0f;
    public int _hp = 0;
    public Rigidbody2D _rb;
    public Transform _myTF;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _myTF = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = -1 * _myTF.up * _speed;
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
    }

}
