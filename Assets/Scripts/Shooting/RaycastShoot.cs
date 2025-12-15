using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{
    public float range = 50f;
    public float fireRate = 0.1f;
    public float damage = 5f;
    public float hitForce = 100f;
    [SerializeField] private GameObject[] shootLines;

    private Camera fpsCam;
    private AudioSource shootAudio;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire; // time when player can fire again
    private bool shootingEnabled = false;

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        shootAudio = GetComponent<AudioSource>();
    }

    public void EnableShooting()
    {
        shootingEnabled = true;
    }

    void Update()
    {
        if (!shootingEnabled) return;

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, fpsCam.nearClipPlane));
            Vector3 lineStart = gameObject.transform.position;
            Vector3 shootDirection = fpsCam.transform.forward;
            Vector3 targetPosition;
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, shootDirection, out hit, range))
            {
                targetPosition = hit.point;
            }
            else
            {
                targetPosition = lineStart + shootDirection * range;
            }

            GameObject shootLineObj = GetRandomShootLine();
            GameObject instance = Instantiate(shootLineObj, lineStart, Quaternion.LookRotation(shootDirection));
            instance.GetComponent<MoveShootLine>().targetPosition = targetPosition;

            shootAudio.Play();
        }
    }

    private GameObject GetRandomShootLine()
    {
        int index = Random.Range(0, shootLines.Length);
        return shootLines[index].gameObject;
    }
}
