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

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isGameActive)
        {
            return;
        }

        if (Time.time > nextFireTime)
        {
            Shoot(bulletPrefab, firePoint);

            if (sideGunsEnabled)
            {
                foreach (Transform sidePoint in sideFirePoints)
                {
                    Shoot(bulletSidePrefab, sidePoint);
                }
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

        // ’e‚Ì‰¹‚ğ–Â‚ç‚·ˆ—‚È‚Ç‚ğ‚±‚±‚É“ü‚ê‚é
    }
}