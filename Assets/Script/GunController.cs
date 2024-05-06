using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Transform parentBullet;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int amountBullet;
    private List<GameObject> bulletPool;
    private float nextFireTime;
    [SerializeField] private float bulletSpeed = 1f;
    private GameObject newBulletPrefab; // Biến lưu trữ prefab của viên đạn mới
    private string[] bulletName;

    void Start()
    {
        CreateBulletPool();
        bulletName = new string[] { "Bullet2", "Bullet3" };
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 0.1f / fireRate;
            }
        }
    }

    private void CreateBulletPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < amountBullet; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, parentBullet);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        bullet.SetActive(false);
    }

    private void Fire()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = spawnpoint.position;
                bullet.transform.rotation = spawnpoint.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;

                StartCoroutine(DeactivateBullet(bullet));

                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletName != null)
        {
            foreach (var namebullet in bulletName)
            {
                if (collision.gameObject.CompareTag(namebullet))
                {
                    newBulletPrefab = collision.gameObject;
                    bulletPrefab.GetComponent<BulletPlayer>().damage = 10 + 10;
                    ReplaceBulletPrefab();

                }
            }
        }
    }

    private void ReplaceBulletPrefab()
    {
        foreach (GameObject bullet in bulletPool)
        {
                bullet.GetComponent<SpriteRenderer>().sprite = newBulletPrefab.GetComponent<SpriteRenderer>().sprite;
                bulletPrefab = newBulletPrefab;
        }
    }
}
