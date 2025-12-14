using UnityEngine;
using TMPro;
using System.Collections;

public class ScorePopup : MonoBehaviour
{
    private Transform targetToFace;
    private TextMeshProUGUI text;
    private float lifetime = 1f;
    private float moveSpeed = 2f;


    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    public void Setup(string scoreText, Color color, Transform target)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = scoreText;
        Color transparentColor = new Color(color.r, color.g, color.b, 0.6f);
        text.color = transparentColor;
        targetToFace = target;

        StartCoroutine(AnimatePopup());
    }

    IEnumerator AnimatePopup()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            transform.position = startPos + Vector3.up * moveSpeed * elapsed;
            transform.LookAt(targetToFace);
            transform.Rotate(0, 180f, 0); // to face the camera correctly
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f - (elapsed / lifetime));

            yield return null;
        }
    }

}
