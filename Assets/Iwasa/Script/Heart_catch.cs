using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_catch : MonoBehaviour
{
    public Image heartIllustration; // イラストを表示するためのUIイメージ
    public Sprite initialSprite; // 初期状態のイラスト
    public Sprite secondStageSprite; // 2段階目のイラスト
    public Sprite thirdStageSprite; // 3段階目のイラスト
    public Sprite finalStageSprite; // 最終段階のイラスト

    private int collectedHearts = 0;

    private void Start()
    {
        // 初期状態のイラストを設定
        heartIllustration.sprite = initialSprite;
    }

    public void CollectHeart()
    {
        collectedHearts++;
        UpdateHeartIllustration();
    }

    private void UpdateHeartIllustration()
    {
        switch (collectedHearts)
        {
            case 1:
                heartIllustration.sprite = secondStageSprite;//イラストを次の段階に変化
                Debug.Log("イラスト変化1");
                break;
            case 2:
                heartIllustration.sprite = thirdStageSprite;
                Debug.Log("イラスト変化2");
                break;
            case 3:
                heartIllustration.sprite = finalStageSprite;
                Debug.Log("イラスト変化3");
                break;
        }
    }
}