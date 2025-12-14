using UnityEngine;

public class MoveShootLine : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 targetPosition;

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        // calculates how far the line should move this frame
        float step = speed * Time.deltaTime;
        transform.position += direction * step;

        // destroy if reaches/passes target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ShootableEnemy enemy = other.GetComponent<ShootableEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();
            Destroy(gameObject);
        }
    }
}
