using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 0.0f;
    public int _Damage = 0;
    public Rigidbody2D _rb;
    public Transform _myTF;
    // Start is called before the first frame update
    void Start()
    {
        _rb.AddForce(_myTF.up * _speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        DeathTime();
    }

    void DeathTime()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
