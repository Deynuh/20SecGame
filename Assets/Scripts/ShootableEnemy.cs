using UnityEngine;

public class ShootableEnemy : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage(float amount)
    {
        Debug.Log("Enemy hit! Remaining health: " + (health - amount));
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
