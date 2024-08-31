using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float smoothTime = 0.1f; // スムーズ化に使用する時間
    public Vector3 offset; // カメラとプレイヤーの距離
    public float minZoom = 5f; // 最小ズーム値
    public float maxZoom = 10f; // 最大ズーム値
    public float zoomSpeed = 10f; // ズームのスピード
    public float jumpThreshold = -1f; // ジャンプと判定する高さ

    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private Transform target;

    void Start()
    {

    }

    void Update()
    {
        cam = GetComponent<Camera>();

        // Playerタグが付いたオブジェクトを探してtargetに設定
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.Log("Playerタグが付いたオブジェクトが見つかりません！");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            bool isJumping = target.position.y > jumpThreshold;


            // ジャンプしているときはズームアウト、していないときはズームイン
            float targetZoom = isJumping ? maxZoom : minZoom;
            float newZoom = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
            cam.orthographicSize = newZoom;

            float clampedX = Mathf.Clamp(transform.position.x, -10000f, 10000f);
            float clampedY = Mathf.Clamp(transform.position.y, -10000f, 10000f);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
