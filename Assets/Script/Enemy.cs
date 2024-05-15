using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int attackPower;

    public virtual void Attack()
    {
        Debug.Log("Boss attacks with power: " + attackPower);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Boss takes damage: " + amount + ", remaining health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
