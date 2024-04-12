using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    public float _speed = 0.0f;
    public float _endY = 0.0f;
    public float _startY = 0.0f;
    public Transform _myTF;

    private void Awake()
    {
        _startY = 11.8f;
        _endY = -11.8f;
    }
    void Start()
    {
        _myTF = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _myTF.Translate(Vector2.down * _speed * Time.deltaTime);
        if (_myTF.position.y <= _endY)
        {
            _myTF.Translate(Vector2.up * _startY * 2);
        }
    }
}
