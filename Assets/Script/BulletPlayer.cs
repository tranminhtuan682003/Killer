using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    EnemyController enemy;
    MantisBoss mantis;
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<EnemyController>();
        mantis = collision.GetComponent<MantisBoss>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Mantis"))
        {
            mantis.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false) ;
        }
    }
}

