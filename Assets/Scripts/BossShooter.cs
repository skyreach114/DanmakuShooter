using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatternSetting
{
    // ���s����e���˃��\�b�h
    public System.Action patternAction;
    // ���̃p�^�[����p�̔��˃��[�g
    public float fireRate;
}

public class BossShooter : MonoBehaviour
{
    public GameObject bulletBallPrefab;
    public GameObject bulletDefaultPrefab;
    public GameObject bulletThinPrefab;
    private GameObject playerObj;

    public Transform[] firePoints;

    public float defaultFireRate = 0.7f;
    private float initialDelayTime = 2.5f;
    private float patternChangeInterval = 3.5f;

    private List<PatternSetting> shotPatterns;

    private Coroutine currentShootingCoroutine;

    void Start()
    {
        playerObj = FindAnyObjectByType<PlayerController>().gameObject;

        shotPatterns = new List<PatternSetting>
        {
            new PatternSetting { patternAction = ShootAim, fireRate = 0.5f },
            new PatternSetting { patternAction = () => ShootFan(6, 90f), fireRate = 1.4f },
            new PatternSetting { patternAction = ShootSpiral, fireRate = 0.12f },
            new PatternSetting { patternAction = () => ShootRing(45), fireRate = 0.8f },
        };

        StartCoroutine(PatternControllerRoutine());
    }

    IEnumerator PatternControllerRoutine()
    {
        yield return new WaitForSeconds(initialDelayTime);

        while (GameManager.Instance.isGameActive)
        {
            PatternSetting randomSetting = GetRandomPattern();

            if (currentShootingCoroutine != null) StopCoroutine(currentShootingCoroutine);

            currentShootingCoroutine = StartCoroutine(ShootingPatternRoutine(randomSetting));

            yield return new WaitForSeconds(patternChangeInterval);
        }
    }

    PatternSetting GetRandomPattern()
    {
        int randomIndex = Random.Range(0, shotPatterns.Count);
        return shotPatterns[randomIndex];
    }

    IEnumerator ShootingPatternRoutine(PatternSetting setting)
    {
        while (true) // �O���� PatternControllerRoutine �Œ�~�����܂Ŗ������[�v
        {
            setting.patternAction.Invoke(); // �I�����ꂽ�e���˃��\�b�h�����s

            yield return new WaitForSeconds(setting.fireRate);
        }
    }

    public void StopShooting()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        StopAllCoroutines();
    }

    void ShootAim()
    {
        if (playerObj == null) return;

        foreach (Transform firePoint in firePoints)
        {
            GameObject bulletObj = Instantiate(bulletBallPrefab);
            bulletObj.transform.position = firePoint.position;
            Vector3 dir = playerObj.transform.position - firePoint.position;
            bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
    }

    // �e����ɔ��˂���p�^�[��
    void ShootFan(int bulletCount, float spreadAngle)
    {
        if (playerObj == null) return;

        // �v���C���[�ւ̕�������Ƃ���
        Vector3 aimDirection = (playerObj.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // �v���C���[�ւ̊p�x

        float angleStep = spreadAngle / (bulletCount - 1); // �e�Ԃ̊p�x
        float startAngle = baseAngle - spreadAngle / 2f; // �J�n�p�x

        for (int i = 0; i < bulletCount; i++)
        {
            foreach (Transform firePoint in firePoints)
            {
                float angle = startAngle + angleStep * i;

                // �p�x���x�N�g���ɕϊ�
                Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                dir.Normalize();

                GameObject bulletObj = Instantiate(bulletThinPrefab, firePoint.position, Quaternion.identity);

                bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            }
        }
    }

    // �e���Q����ɔ��˂���p�^�[��
    void ShootSpiral()
    {
        if (playerObj == null) return;

        // �����̊�p�x�����Ԃ̌o�߂ŉ�]������
        // Time.time ���g�����ƂŁA�Q�[���S�̂�ʂ��Ċp�x�����炩�ɕω���������
        float angle = Time.time * 90f;

        // firePoints �̂ǂ��炩���甭��
        Transform firePoint = firePoints[0]; //����firePoint���甭��

        // �p�x���x�N�g���ɕϊ�
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        dir.Normalize();

        GameObject bulletObj = Instantiate(bulletBallPrefab, firePoint.position, Quaternion.identity);
        bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        firePoint = firePoints[1]; // �E��firePoint���甭��
        angle += 90f; // ���炵�Ĕ���
        dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        dir.Normalize();

        bulletObj = Instantiate(bulletDefaultPrefab, firePoint.position, Quaternion.identity);
        bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    void ShootRing(int bulletCount)
    {
        float angleStep = 360f / bulletCount;

        //�{�X�{�̂̈ʒu���甭��
        Vector3 centerPosition = transform.position;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;

            // �p�x���x�N�g���ɕϊ�
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            dir.Normalize();

            GameObject bulletObj = Instantiate(bulletThinPrefab, centerPosition, Quaternion.identity);
            bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
    }
}
