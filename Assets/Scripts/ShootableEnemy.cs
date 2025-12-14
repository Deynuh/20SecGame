using UnityEngine;
using System.Collections;

public class ShootableEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject deathSmokePrefab;
    public GameObject scorePopupPrefab;

    private AudioSource audioSource;
    private GameObject target;

    private Rigidbody rb;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Target");
    }

    void FixedUpdate()
    {
        // move forward continuously
        Vector3 newPosition = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    public void TakeDamage()
    {
        GameScore.Instance.AddScore(1);
        ShowScorePopup("+1", Color.green);
        Die();

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
        GameScore.Instance.SubtractScore(2);
        ShowScorePopup("-2", Color.red);
    }

    void ShowScorePopup(string text, Color color)
    {
        Vector3 popupPosition = transform.position + Vector3.up * 1f;
        GameObject popup = Instantiate(scorePopupPrefab, popupPosition, Quaternion.identity);
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();
        popupScript.Setup(text, color, target.transform);
    }

    void Die()
    {
        Vector3 smokePosition = transform.position + Vector3.up * 1f;
        Instantiate(deathSmokePrefab, smokePosition, Quaternion.identity);
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 0.2f);
        Destroy(gameObject);
    }
}
