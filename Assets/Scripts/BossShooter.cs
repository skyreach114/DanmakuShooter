using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatternSetting
{
    // 実行する弾発射メソッド
    public System.Action patternAction;
    // このパターン専用の発射レート
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
        while (true) // 外部の PatternControllerRoutine で停止されるまで無限ループ
        {
            setting.patternAction.Invoke(); // 選択された弾発射メソッドを実行

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

    // 弾を扇状に発射するパターン
    void ShootFan(int bulletCount, float spreadAngle)
    {
        if (playerObj == null) return;

        // プレイヤーへの方向を基準とする
        Vector3 aimDirection = (playerObj.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // プレイヤーへの角度

        float angleStep = spreadAngle / (bulletCount - 1); // 弾間の角度
        float startAngle = baseAngle - spreadAngle / 2f; // 開始角度

        for (int i = 0; i < bulletCount; i++)
        {
            foreach (Transform firePoint in firePoints)
            {
                float angle = startAngle + angleStep * i;

                // 角度をベクトルに変換
                Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                dir.Normalize();

                GameObject bulletObj = Instantiate(bulletThinPrefab, firePoint.position, Quaternion.identity);

                bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
            }
        }
    }

    // 弾を渦巻状に発射するパターン
    void ShootSpiral()
    {
        if (playerObj == null) return;

        // 螺旋の基準角度を時間の経過で回転させる
        // Time.time を使うことで、ゲーム全体を通して角度が滑らかに変化し続ける
        float angle = Time.time * 90f;

        // firePoints のどちらかから発射
        Transform firePoint = firePoints[0]; //左のfirePointから発射

        // 角度をベクトルに変換
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        dir.Normalize();

        GameObject bulletObj = Instantiate(bulletBallPrefab, firePoint.position, Quaternion.identity);
        bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        firePoint = firePoints[1]; // 右のfirePointから発射
        angle += 90f; // ずらして発射
        dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        dir.Normalize();

        bulletObj = Instantiate(bulletDefaultPrefab, firePoint.position, Quaternion.identity);
        bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    void ShootRing(int bulletCount)
    {
        float angleStep = 360f / bulletCount;

        //ボス本体の位置から発射
        Vector3 centerPosition = transform.position;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;

            // 角度をベクトルに変換
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            dir.Normalize();

            GameObject bulletObj = Instantiate(bulletThinPrefab, centerPosition, Quaternion.identity);
            bulletObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
    }
}
