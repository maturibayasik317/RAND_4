using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_pillar : MonoBehaviour
{
    public GameObject flamePrefab; // 炎のプレハブ
    public float flameDuration = 2.0f; // 炎の持続時間
    public float interval = 5.0f; // 炎が出る間隔
    public float flameLength = 5.0f; // 炎の長さ
    private GameObject flameInstance;

    void Start()
    {
        // 炎の生成を開始する
        StartCoroutine(FlameRoutine());
    }

    IEnumerator FlameRoutine()
    {
        while (true)
        {
            // 指定された間隔で待機
            yield return new WaitForSeconds(interval);

            // 炎を生成
            SpawnFlame();

            // 指定された時間だけ待機
            yield return new WaitForSeconds(flameDuration);

            // 炎を非表示にする
            if (flameInstance != null)
            {
                flameInstance.SetActive(false);
            }
        }
    }

    void SpawnFlame()
    {
        if (flamePrefab == null)
        {
            Debug.LogError("炎のprefabがありません");
            return;
        }

        // 炎がまだ生成されていない場合、生成する
        if (flameInstance == null)
        {
            flameInstance = Instantiate(flamePrefab, transform.position, Quaternion.identity);
            flameInstance.transform.SetParent(transform);
        }

        // 炎を表示し、長さを調整する
        flameInstance.SetActive(true);
        flameInstance.transform.localScale = new Vector3(flameInstance.transform.localScale.x, flameLength, flameInstance.transform.localScale.z);
    }
}