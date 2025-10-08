using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner runnerPrefab;
    private NetworkRunner _runner;
    public NetworkObject playerPrefab;

    public static int SelectedPlayerCount = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // オフライン（共有モード）で起動
    public async void StartShared()
    {
        if (_runner != null) return;

        _runner = Instantiate(runnerPrefab);
        DontDestroyOnLoad(_runner.gameObject);
        _runner.AddCallbacks(this);

        Debug.Log("Runner生成開始（Sharedモード）");

        var sceneManager = _runner.GetComponent<NetworkSceneManagerDefault>();
        if (sceneManager == null)
        {
            sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        // Sharedモードで起動（ネット通信なし）
        var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "LocalSession", 
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), 
            SceneManager = sceneManager
        });

        Debug.Log(result);
    }

    public void SetPlayerCount(int count)
    {
        SelectedPlayerCount = count;
        Debug.Log($"プレイ人数が {count} 人に設定されました");
    }

    public void GoToCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    // Sharedモードでも呼ばれる（ローカルプレイヤー生成）
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (SceneManager.GetActiveScene().name != "GameScene") return;

        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
        runner.Spawn(playerPrefab, spawnPos, Quaternion.identity, player);
        Debug.Log($"プレイヤー {player.PlayerId} を生成しました（共有モード）");
    }

    // ====== 以下は空でOK ======
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}