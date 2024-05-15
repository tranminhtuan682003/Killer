using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,IHealth
{
    public static PlayerController instance { get; private set; }
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float healh,maxHealh;
    public Slider slideHealth;
    private Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        healh = maxHealh;
        slideHealth.value = 1;
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
            MusicManager.instance.PlayerSounds("Shoting");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsShoot", false);
            MusicManager.instance.StopPlayerSounds("Shoting");
        }
    }
    public void TakeDamage(float amount)
    {
        healh -= amount;
        slideHealth.value -= amount/maxHealh;
        if (healh <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}

