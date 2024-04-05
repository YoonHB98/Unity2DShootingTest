using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemType _itemType;
    public float _speed;
    Rigidbody2D _rd;
    // Start is called before the first frame update
    void Start()
    {
        _rd = GetComponent<Rigidbody2D>();
        _rd.velocity = Vector2.down * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum ItemType
{
    none,
    Coin,
    Power,
    Bomb
}
