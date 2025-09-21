using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform firePoint;

    public float fireRate = 0.4f;

    // ���ɒe�𔭎˂ł��鎞����ێ����邽�߂̕ϐ�
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

        // �e�̉���炷�����Ȃǂ������ɓ����
    }
}