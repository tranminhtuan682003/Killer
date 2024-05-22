using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            FactoryEnemy.Instance.UseFloatingPrefab(transform,0.1f);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            gameObject.SetActive(false);
        }
    }
}
