using UnityEngine;
using System.Collections;

public class ShootableEnemy : MonoBehaviour
{
    private GameObject target;

    public float health = 5f;
    public float moveSpeed = 2f;

    private Rigidbody rb;
    // private Renderer enemyRenderer;
    private Color originalColor;

    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);


    void Start()
    {
        // enemyRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        // originalColor = enemyRenderer.material.color;
        target = GameObject.FindWithTag("Target");
    }

    void FixedUpdate()
    {
        // move forward continuously
        Vector3 newPosition = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void TakeDamage(float amount)
    {
        // StartCoroutine(FlashWhite());
        health -= amount;

        if (health <= 0f)
        {
            GameScore.Instance.AddScore(1);
            Die();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            DealDamage();
            Die();
        }
    }

    public void DealDamage()
    {
        // StartCoroutine(FlashBlack());
        GameScore.Instance.SubtractScore(2);
    }

    // IEnumerator FlashWhite()
    // {
    //     enemyRenderer.material.color = Color.white;
    //     yield return flashDuration;
    //     enemyRenderer.material.color = originalColor;
    // }

    // IEnumerator FlashBlack()
    // {
    //     enemyRenderer.material.color = Color.black;
    //     yield return flashDuration;
    //     enemyRenderer.material.color = originalColor;
    // }

    // eventually add a dying animation here
    void Die()
    {
        Destroy(gameObject);
    }
}
