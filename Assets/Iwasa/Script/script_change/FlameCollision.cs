using UnityEngine;

public class FlameCollision : MonoBehaviour
{
    public string invincibility; // 無敵のオブジェクトの名前

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤータグを持っているが無敵の名前と一致するオブジェクトは破壊しない
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name != invincibility)
            {
                Destroy(other.gameObject);
                Debug.Log("Playerは炎により破壊");
            }
            else
            {
                Debug.Log("無敵のオブジェクトは破壊されない");
            }
        }
    }
}
