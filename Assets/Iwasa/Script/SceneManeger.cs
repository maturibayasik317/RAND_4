using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SceneManeger : MonoBehaviour
{
    public StageManager stageManager;

    public GameObject GoalPoint;
    private bool gameOver = false;


    [SceneField] public string Tutorial;//チュートリアルステージ
    [SceneField] public string GameClearScene;//クリアシーン
    [SceneField] public string GameOverScene;//ゲームオーバーシーン
    [SceneField] public string MenyScene;//メニュー画面
    [SceneField] public string stageSelectScene;//ステージ選択シーンに移動
    [SceneField] public string Scene1;//ステージ１
    [SceneField] public string Scene2;//ステージ２
    [SceneField] public string Scene3;//ステージ３
    [SceneField] public string Scene4;//ステージ４
    [SceneField] public string Scene5;//ステージ５

    public static bool ReStart = false;//ステージの中間ポイント判定
    private string lastActiveScene; // 最後にアクティブだったシーンを保存する変数

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // チュートリアルおよびステージ1～4シーンのみでプレイヤーの存在を確認する
        if ((currentScene == Tutorial ||
             currentScene == Scene1 ||
             currentScene == Scene2 ||
             currentScene == Scene3 ||
             currentScene == Scene4 ||
             currentScene == Scene5)
             && !gameOver && GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOver = true;
            GameManager.previousScene = currentScene; // プレイヤーがデストロイされたシーンを保存
            Debug.Log("プレイヤーがデストロイされました。GameManager.previousScene: " + GameManager.previousScene);
            StartCoroutine(ChangeSceneAfterDelay("GameOver", 2.0f));
        }

    }

    // メインメニューに移動
    public void menyScene()
    {
        SceneManager.LoadScene(MenyScene);
        Debug.Log("スタート画面に移動");
    }

    public void StageSelect()
    {
        ReStart = false;
        Spawn.CheckPoint = Vector2.zero;
        SceneManager.LoadScene(stageSelectScene);
        Debug.Log("ステージ選択画面に移動");
    }

    //チュートリアルに移動
    public void tutorialScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Tutorial); // ゲーム画面に移動
        Debug.Log("チュートリアルシーンに移動");
    }

    //シーン1に移動
    public void scene1()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Scene1); 
        Debug.Log("ステージ1に移動");
    }

    //シーン２に移動
    public void scene2() 
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Scene2);
        Debug.Log("ステージ2に移動");
    }

    //シーン３に移動
    public void scene3()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Scene3); 
        Debug.Log("ステージ3に移動");
    }

    //シーン４に移動
    public void scene4()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Scene4); 
        Debug.Log("ステージ4に移動");
    }

    public void scene5()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(Scene4);
        Debug.Log("ステージ5に移動");
    }

    //ゲームクリア画面に移動
    public void gameClearScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(GameClearScene); // ゲームクリアシーンに移動
        Debug.Log("ゲームクリアに移動");
    }

    //ゲーム―オーバー画面に移動
    public void gameOverScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // 現在のシーン名を保存
        SceneManager.LoadScene(GameOverScene); // ゲームオーバー画面に移動
        ReStart = false;
        Spawn.CheckPoint = Vector2.zero;
        Debug.Log("ゲームオーバーシーンに移動");
    }

    public void ReStartGame()
    {
        ReStart = true;
        Spawn.CheckPoint = Vector2.zero; // CheckPointをリセット

        string sceneToLoad = GameManager.previousScene;

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Previous scene not set or empty, loading default tutorial scene.");
            sceneToLoad = Tutorial; // デフォルトのシーンを設定
        }

        Debug.Log("リトライするシーン: " + sceneToLoad);

        try
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        catch (ArgumentException e)
        {
            Debug.Log("The scene " + sceneToLoad + " cannot be loaded: " + e.Message);
            // デフォルトシーンにフォールバック
            SceneManager.LoadScene(Tutorial);
        }
    }


    //そのシーンに移動できるかどうかの判別
    public void LoadStage(int stageIndex)
    {
        if (stageManager.IsStageUnlocked(stageIndex))
        {
            SceneManager.LoadScene(stageManager.stages[stageIndex].stageName);
            Debug.Log(stageManager.stages[stageIndex].stageName + "に移動しました。");
        }
        else
        {
            Debug.Log("このステージはまだ解禁されていません。");
        }
    }

    //シーンの移動処理
    IEnumerator ChangeSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    //ゲームクリアの処理    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameClearScene();
        }
    }
}

internal class SceneFieldAttribute1 : Attribute
{
}