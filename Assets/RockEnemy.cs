using UnityEngine;

public class RockEnemy : MonoBehaviour,IEnemy,IDamageable
{
    public float health { get; set; }
    private GameObject player;
    [SerializeField] private float speed;
    private Animator animator;
    private float damage = 10f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        this.health = 50;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Player not Found");
        }
    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Da nhan damage");
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        animator.SetBool("IsDead", true);
        MusicManager.instance.PlayerSounds("BossDie");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.TakeDamage(damage);
            Die();
        }
    }
    public void Active()
    {
        gameObject.SetActive(false);
    }
}
