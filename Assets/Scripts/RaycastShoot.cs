using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{
    public float range = 50f;
    public float fireRate = 0.1f;
    public float damage = 5f;
    public float hitForce = 100f;

    private Camera fpsCam;
    private AudioSource shootAudio;
    private LineRenderer shootLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire; // time when player can fire again

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        shootAudio = GetComponent<AudioSource>();
        shootLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, fpsCam.nearClipPlane));
            Vector3 lineStart = gameObject.transform.position;
            Vector3 shootDirection = fpsCam.transform.forward;

            RaycastHit hit;
            shootLine.SetPosition(0, lineStart);

            if (Physics.Raycast(rayOrigin, shootDirection, out hit, range))
            {
                shootLine.SetPosition(1, hit.point);

                ShootableEnemy enemy = hit.collider.GetComponent<ShootableEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                shootLine.SetPosition(1, rayOrigin + (shootDirection * range));
            }

            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        shootAudio.Play();
        shootLine.enabled = true;
        yield return shotDuration;
        shootLine.enabled = false;
    }
}
