using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton;
    public Button menuButton;
    [SceneField] public string startScene;

    private bool isPaused = false;

    void Start()
    {
        // �{�^�����\���ɂ���
        pauseMenuUI.SetActive(false);

        // �{�^���Ƀ��X�i�[��ǉ�
        pauseButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(GoToMenu);
    }
    void Update()
    {       // P�L�[�������ꂽ�Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.P))
        {
          if (isPaused)
          {
                Resume();
            }
          else
          {
                Pause();
          }
        }
    }

    //�Q�[���̒�~
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //�Q�[���̍ĊJ
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //���j���[��ʂɈړ�
    void GoToMenu()
    {
        SceneManager.LoadScene(startScene); // �X�^�[�g�V�[���Ɉړ�
    }
}
