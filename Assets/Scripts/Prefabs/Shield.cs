using UnityEngine;

public class Shield : MonoBehaviour
{
    private float shieldValue; // 护盾值

    public void SetShieldValue(float value)
    {
        // 设置护盾值
        shieldValue = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞到的是不是玩家
        if (other.CompareTag("Player"))
        {
            // 对玩家造成伤害
            other.GetComponent<Player>().TakeDamage(10f);
            // 减少护盾值
            shieldValue -= 10f;
            // 检查护盾值是否小于等于0
            if (shieldValue <= 0)
            {
                // 销毁护盾
                Destroy(gameObject);
                Debug.Log("Shield destroyed");
            }
        }
    }
}

