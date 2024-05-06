using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IHealth
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float health, maxHealth;
    private Animator animator;

    private bool checkFlip;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        Move();
        MousePosition();

    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        animator.SetBool("IsRun", true);
        if(horizontal == 0 && vertical == 0)
        {
            animator.SetBool("IsRun", false);
        }
        Attack();
    }
    private void MousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }
    private void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsShoot", true);

        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsShoot", false);
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetBool("IsHit", true);
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

