using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IHealth
{

    [SerializeField] private float health, maxHealth;
    [SerializeField] private float speed;
    private Animator animator;
    private Transform player;
    public GameObject floatingPoint;
    public GameObject DamageImage;
    public Transform[] positionDamage;
    private bool isDead;

    void Start()
    {
        GameObject playerr = GameObject.FindGameObjectWithTag("Player");
        player = playerr.transform;
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    void Update()
    {
        if (!isDead && player != null) 
        {
            FollowPlayer(); 
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            ((IHealth)this).Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            StartCoroutine(WaitForDestroy());
        }
    }

    IEnumerator WaitForDestroy()
    {
        if (!isDead)
        {
            GameObject newFloatingPoint = Instantiate(floatingPoint, positionDamage[0].position, Quaternion.identity);
            floatingPoint.GetComponent<Animator>().SetBool("IsAttack", true);
            GameObject newDamageImage = Instantiate(DamageImage, positionDamage[1].position, Quaternion.identity);
            newDamageImage.GetComponent<Animator>().SetBool("IsAttack",true);

            yield return new WaitForSeconds(0.2f);
            Destroy(newFloatingPoint);
            Destroy(newDamageImage);
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Random.Range(speed-1,speed*2) * Time.deltaTime);
    }

    void IHealth.Dead()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }
}
