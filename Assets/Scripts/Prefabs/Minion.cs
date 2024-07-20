using UnityEngine;

public class Minion : MonoBehaviour
{
    // 定义仆从被击杀的事件
    public delegate void MinionKilledHandler(GameObject minion);
    public event MinionKilledHandler OnMinionKilled;
    public float health = 50f; // 仆从的生命值

    public void TakeDamage(float damage)
    {
        // 减少仆从的生命值
        health -= damage;
        // 检查生命值是否小于等于0
        if (health <= 0)
        {
            // 触发仆从死亡事件
            if (OnMinionKilled != null)
            {
                OnMinionKilled(gameObject);
            }
            // 销毁仆从
            Destroy(gameObject);
        }
    }

    public void Enhance()
    {
        // 增强仆从的属性
        health *= 2;
        Debug.Log("Minion enhanced");
    }
}

