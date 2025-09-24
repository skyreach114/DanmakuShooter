using UnityEngine;

public class EnemyShooter_Simple : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform[] firePoints;

    public float fireRate = 0.7f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();

            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 180));
        }

        // ’e‚Ì‰¹‚ğ–Â‚ç‚·ˆ—‚È‚Ç‚ğ‚±‚±‚É“ü‚ê‚é
    }
}