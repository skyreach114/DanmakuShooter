using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterData[] characters;   // �������Ԃ� Inspector �ɃZ�b�g
    public GameObject playerPrefab;      // ���ʂ̃v���C���[�v���n�u
    public Transform playerSpawnPoint;

    public PlayerSpeedButton speedButton;

    void Start()
    {
        int idx = PlayerPrefs.GetInt("SelectedCharacter", 0);
        idx = Mathf.Clamp(idx, 0, characters.Length - 1);

        var data = characters[idx];

        // PlayerPrefab �𐶐�
        var playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        // PlayerController �Ƀf�[�^��K�p
        var pc = playerObj.GetComponent<PlayerController>();
        var pm = playerObj.GetComponent<PlayerMovement>();

        pc.SetupFromData(data);

        if (speedButton != null && pm != null)
        {
            // PlayerSpeedButton�����삷�ׂ�PlayerMovement�R���|�[�l���g��ݒ�
            speedButton.playerMove = pm;

            // �v���C���[�̏������x��normalSpeed�ɐݒ�i�{�^����ToggleSpeed�Ɠ����`���ŌĂԁj
            // SetSpeed(currentSpeed, isLowSpeed, isUIChange)
            // �Q�[���J�n����UI�ł̕ύX�ł͂Ȃ��̂� isUIChange �� false �ɂ���
            //pm.SetSpeed(pm.normalSpeed, false, false);
        }
    }
}