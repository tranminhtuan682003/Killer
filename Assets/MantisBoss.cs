using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MantisBoss : MonoBehaviour,IHealth
{
    public Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float health,maxHealth;
    private Animator animator;
    private PolygonCollider2D polygon;
    public LayerMask playerMask;
    void Start()
    {
        animator = GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
        health = maxHealth;
    }
    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 direction = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,direction.normalized, Mathf.Infinity);
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position,polygon.bounds.size,0f,Vector2.right,0f,playerMask);
        if(hit2.collider != null)
        {
             animator.SetBool("AttackLeft", true);
        }
        Debug.DrawRay(transform.position, direction, Color.green);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
            if (angle > 45 && angle <= 135)
            {
                animator.SetBool("MoveDown", true);
                animator.SetBool("MoveLeft", false);
                transform.localScale = new Vector3(5, -5, 5);
            }
            else if (angle > -45 && angle <= 45)
            {
                animator.SetBool("MoveDown", false);
                animator.SetBool("MoveLeft", true);
                transform.localScale = new Vector3(-5, 5, 5);
            }
            else if (angle > -135 && angle <= -45)
            {
                animator.SetBool("MoveDown", true);
                animator.SetBool("MoveLeft", false);
            }
            else
            {
                animator.SetBool("MoveDown", false);
                animator.SetBool("MoveLeft", true);
                transform.localScale = new Vector3(5, 5, 5);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

}
