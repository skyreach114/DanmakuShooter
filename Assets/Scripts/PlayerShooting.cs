using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform firePoint;

    public float fireRate = 0.4f;

    // Ÿ‚É’e‚ğ”­Ë‚Å‚«‚é‚ğ•Û‚·‚é‚½‚ß‚Ì•Ï”
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // ’e‚Ì‰¹‚ğ–Â‚ç‚·ˆ—‚È‚Ç‚ğ‚±‚±‚É“ü‚ê‚é
    }
}