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


    [SceneField] public string Tutorial;//�`���[�g���A���X�e�[�W
    [SceneField] public string GameClearScene;//�N���A�V�[��
    [SceneField] public string GameOverScene;//�Q�[���I�[�o�[�V�[��
    [SceneField] public string MenyScene;//���j���[���
    [SceneField] public string stageSelectScene;//�X�e�[�W�I���V�[���Ɉړ�
    [SceneField] public string Scene1;//�X�e�[�W�P
    [SceneField] public string Scene2;//�X�e�[�W�Q
    [SceneField] public string Scene3;//�X�e�[�W�R
    [SceneField] public string Scene4;//�X�e�[�W�S
    [SceneField] public string Scene5;//�X�e�[�W�T

    public static bool ReStart = false;//�X�e�[�W�̒��ԃ|�C���g����
    private string lastActiveScene; // �Ō�ɃA�N�e�B�u�������V�[����ۑ�����ϐ�

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // �`���[�g���A������уX�e�[�W1�`4�V�[���݂̂Ńv���C���[�̑��݂��m�F����
        if ((currentScene == Tutorial ||
             currentScene == Scene1 ||
             currentScene == Scene2 ||
             currentScene == Scene3 ||
             currentScene == Scene4 ||
             currentScene == Scene5)
             && !gameOver && GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOver = true;
            GameManager.previousScene = currentScene; // �v���C���[���f�X�g���C���ꂽ�V�[����ۑ�
            Debug.Log("�v���C���[���f�X�g���C����܂����BGameManager.previousScene: " + GameManager.previousScene);
            StartCoroutine(ChangeSceneAfterDelay("GameOver", 2.0f));
        }

    }

    // ���C�����j���[�Ɉړ�
    public void menyScene()
    {
        SceneManager.LoadScene(MenyScene);
        Debug.Log("�X�^�[�g��ʂɈړ�");
    }

    public void StageSelect()
    {
        ReStart = false;
        Spawn.CheckPoint = Vector2.zero;
        SceneManager.LoadScene(stageSelectScene);
        Debug.Log("�X�e�[�W�I����ʂɈړ�");
    }

    //�`���[�g���A���Ɉړ�
    public void tutorialScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Tutorial); // �Q�[����ʂɈړ�
        Debug.Log("�`���[�g���A���V�[���Ɉړ�");
    }

    //�V�[��1�Ɉړ�
    public void scene1()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Scene1); 
        Debug.Log("�X�e�[�W1�Ɉړ�");
    }

    //�V�[���Q�Ɉړ�
    public void scene2() 
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Scene2);
        Debug.Log("�X�e�[�W2�Ɉړ�");
    }

    //�V�[���R�Ɉړ�
    public void scene3()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Scene3); 
        Debug.Log("�X�e�[�W3�Ɉړ�");
    }

    //�V�[���S�Ɉړ�
    public void scene4()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Scene4); 
        Debug.Log("�X�e�[�W4�Ɉړ�");
    }

    public void scene5()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(Scene4);
        Debug.Log("�X�e�[�W5�Ɉړ�");
    }

    //�Q�[���N���A��ʂɈړ�
    public void gameClearScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(GameClearScene); // �Q�[���N���A�V�[���Ɉړ�
        Debug.Log("�Q�[���N���A�Ɉړ�");
    }

    //�Q�[���\�I�[�o�[��ʂɈړ�
    public void gameOverScene()
    {
        GameManager.previousScene = SceneManager.GetActiveScene().name; // ���݂̃V�[������ۑ�
        SceneManager.LoadScene(GameOverScene); // �Q�[���I�[�o�[��ʂɈړ�
        ReStart = false;
        Spawn.CheckPoint = Vector2.zero;
        Debug.Log("�Q�[���I�[�o�[�V�[���Ɉړ�");
    }

    public void ReStartGame()
    {
        ReStart = true;
        Spawn.CheckPoint = Vector2.zero; // CheckPoint�����Z�b�g

        string sceneToLoad = GameManager.previousScene;

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Previous scene not set or empty, loading default tutorial scene.");
            sceneToLoad = Tutorial; // �f�t�H���g�̃V�[����ݒ�
        }

        Debug.Log("���g���C����V�[��: " + sceneToLoad);

        try
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        catch (ArgumentException e)
        {
            Debug.Log("The scene " + sceneToLoad + " cannot be loaded: " + e.Message);
            // �f�t�H���g�V�[���Ƀt�H�[���o�b�N
            SceneManager.LoadScene(Tutorial);
        }
    }


    //���̃V�[���Ɉړ��ł��邩�ǂ����̔���
    public void LoadStage(int stageIndex)
    {
        if (stageManager.IsStageUnlocked(stageIndex))
        {
            SceneManager.LoadScene(stageManager.stages[stageIndex].stageName);
            Debug.Log(stageManager.stages[stageIndex].stageName + "�Ɉړ����܂����B");
        }
        else
        {
            Debug.Log("���̃X�e�[�W�͂܂����ւ���Ă��܂���B");
        }
    }

    //�V�[���̈ړ�����
    IEnumerator ChangeSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    //�Q�[���N���A�̏���    
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