using UnityEngine;
using System.Collections;

public class ShootableEnemy : MonoBehaviour
{
    public float health = 10f;
    public float moveSpeed = 2f;

    private Rigidbody rb;
    private Renderer renderer;
    private Color originalColor;

    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    void Start()
    {
        renderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        originalColor = renderer.material.color;
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
        renderer.material.color = Color.white;
        yield return flashDuration;
        renderer.material.color = originalColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
