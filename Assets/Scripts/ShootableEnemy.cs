using UnityEngine;
using System.Collections;

public class ShootableEnemy : MonoBehaviour
{
    public float health = 10f;
    public float moveSpeed = 2f;

    private Renderer enemyRenderer;
    private Color originalColor;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
    }

    void Update()
    {
        // move forward continuously
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
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
