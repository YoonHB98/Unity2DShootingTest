using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bullet":
                PoolManager.Despawn(collision.gameObject);
                break;
            case "Enemy":
            case "EnemyBullet":
                Destroy(collision.gameObject);
                break;
        }
    }

}
