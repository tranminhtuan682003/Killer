using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float damage = 10;
    public GameObject damagePrefab;
    public GameObject floatingPrefap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);

            if (damagePrefab != null)
            {
                GameObject newDamage = Instantiate(damagePrefab, transform.position,Quaternion.identity);
                GameObject newFloating = Instantiate(floatingPrefap, transform.position, Quaternion.identity);
                Destroy(newDamage, 0.1f); 
                Destroy(newFloating, 0.1f);
            }

            gameObject.SetActive(false);
        }
    }
}
