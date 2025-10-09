using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletSidePrefab;

    public Transform firePoint;
    public Transform[] sideFirePoints;

    public float fireRate = 0.4f;
    private float nextFireTime = 0f;

    private bool sideGunsEnabled = false;

    public AudioSource fireSound;
    public AudioSource sideGunsFireSound;

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isGameActive)
        {
            return;
        }

        if (Time.time > nextFireTime)
        {
            Shoot(bulletPrefab, firePoint);
            AudioSource.PlayClipAtPoint(fireSound.clip, new Vector3(0, 0, -6), 0.08f);

            if (sideGunsEnabled)
            {
                foreach (Transform sidePoint in sideFirePoints)
                {
                    Shoot(bulletSidePrefab, sidePoint);
                }

                AudioSource.PlayClipAtPoint(sideGunsFireSound.clip, new Vector3(0, 0, -1), 0.08f);
            }

            nextFireTime = Time.time + fireRate;
        }
    }

    public void SetFireRate(float newRate)
    {
        fireRate = newRate;
    }

    public void EnableSideGuns()
    {
        sideGunsEnabled = true;
    }

    void Shoot(GameObject bulletP, Transform firePoint)
    {
        Instantiate(bulletP, firePoint.position, firePoint.rotation);
    }
}