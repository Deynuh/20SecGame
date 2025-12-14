using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class PostProcessing : MonoBehaviour
{
    [SerializeField] private float blinkDuration = 0.2f;

    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;

    void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();

        volume.profile.TryGetSettings(out lensDistortion);
        volume.profile.TryGetSettings(out chromaticAberration);
        volume.profile.TryGetSettings(out vignette);
    }

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < blinkDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (blinkDuration / 2f);

            lensDistortion.intensity.value = Mathf.Lerp(10f, 30f, t);
            chromaticAberration.intensity.value = Mathf.Lerp(.15f, 1f, t);
            vignette.intensity.value = Mathf.Lerp(0.7f, 0.9f, t);

            yield return null;
        }

        // back to normal
        elapsed = 0f;
        while (elapsed < blinkDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (blinkDuration / 2f);

            lensDistortion.intensity.value = Mathf.Lerp(30f, 10f, t);
            chromaticAberration.intensity.value = Mathf.Lerp(1f, .15f, t);
            vignette.intensity.value = Mathf.Lerp(0.7f, 0.5f, t);
            yield return null;
        }
    }
}
