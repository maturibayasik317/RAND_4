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
        // ボタンを非表示にする
        pauseMenuUI.SetActive(false);

        // ボタンにリスナーを追加
        pauseButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(GoToMenu);
    }
    void Update()
    {       // Pキーが押されたときの処理
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

    //ゲームの停止
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //ゲームの再開
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //メニュー画面に移動
    void GoToMenu()
    {
        SceneManager.LoadScene(startScene); // スタートシーンに移動
    }
}
