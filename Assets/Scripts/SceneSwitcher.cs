using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Inspector����Q�[���V�[���p��BGM���Z�b�g
    public AudioClip gameBGMClip;

    // �Q�[���X�^�[�g�{�^���������ꂽ�Ƃ��Ɏ��s����郁�\�b�h
    public void OnPressGameStart()
    {
        // 1. BGMController�̃C���X�^���X���擾
        BGMController bgmController = BGMController.GetInstance();

        if (bgmController != null)
        {
            // 2. BGM���~���A�Q�[��BGM�ɐ؂�ւ��鏈�����Ăяo��
            bgmController.StopAndSwitchBGM(gameBGMClip);
        }

        // 3. �Q�[���V�[���֑J��
        SceneManager.LoadScene("GameScene");
    }
}
