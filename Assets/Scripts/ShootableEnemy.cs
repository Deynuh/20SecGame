using UnityEngine;
using System.Collections;

public class ShootableEnemy : MonoBehaviour
{
    public float health = 5f;
    public float moveSpeed = 2f;

    private Rigidbody rb;
    private Renderer enemyRenderer;
    private Color originalColor;

    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        originalColor = enemyRenderer.material.color;
    }

    void FixedUpdate()
    {
        // move forward continuously
        Vector3 newPosition = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        StartCoroutine(FlashWhite());

        if (health <= 0f)
        {
            Die();
        }
    }

    IEnumerator FlashWhite()
    {
        enemyRenderer.material.color = Color.white;
        yield return flashDuration;
        enemyRenderer.material.color = originalColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
