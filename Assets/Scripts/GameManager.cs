using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterData[] characters;   // 同じ順番で Inspector にセット
    public GameObject playerPrefab;      // 共通のプレイヤープレハブ
    public Transform playerSpawnPoint;

    public PlayerSpeedButton speedButton;

    void Start()
    {
        int idx = PlayerPrefs.GetInt("SelectedCharacter", 0);
        idx = Mathf.Clamp(idx, 0, characters.Length - 1);

        var data = characters[idx];

        // PlayerPrefab を生成
        var playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        // PlayerController にデータを適用
        var pc = playerObj.GetComponent<PlayerController>();
        var pm = playerObj.GetComponent<PlayerMovement>();

        pc.SetupFromData(data);

        if (speedButton != null && pm != null)
        {
            // PlayerSpeedButtonが操作すべきPlayerMovementコンポーネントを設定
            speedButton.playerMove = pm;

            // プレイヤーの初期速度をnormalSpeedに設定（ボタンのToggleSpeedと同じ形式で呼ぶ）
            // SetSpeed(currentSpeed, isLowSpeed, isUIChange)
            // ゲーム開始時はUIでの変更ではないので isUIChange は false にする
            //pm.SetSpeed(pm.normalSpeed, false, false);
        }
    }
}