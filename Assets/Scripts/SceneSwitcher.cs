using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Inspectorからゲームシーン用のBGMをセット
    public AudioClip gameBGMClip;

    // ゲームスタートボタンが押されたときに実行されるメソッド
    public void OnPressGameStart()
    {
        // 1. BGMControllerのインスタンスを取得
        BGMController bgmController = BGMController.GetInstance();

        if (bgmController != null)
        {
            // 2. BGMを停止し、ゲームBGMに切り替える処理を呼び出す
            bgmController.StopAndSwitchBGM(gameBGMClip);
        }

        // 3. ゲームシーンへ遷移
        SceneManager.LoadScene("GameScene");
    }
}
