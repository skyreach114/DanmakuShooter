using UnityEngine;

public class EnemyShooter_Aim : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject playerObj;

    public Transform firePoint;

    public float fireRate = 0.9f;
    private float nextFireTime = 0f;

    void Start()
    {
        playerObj = FindAnyObjectByType<Player>().gameObject;
    }

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
        if (playerObj == null) return;

        GameObject bulletObj = Instantiate(bulletPrefab);
        bulletObj.transform.position = firePoint.position;
        Vector3 dir = playerObj.transform.position - firePoint.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);

        // ’e‚Ì‰¹‚ğ–Â‚ç‚·ˆ—‚È‚Ç‚ğ‚±‚±‚É“ü‚ê‚é
    }
}