using UnityEngine;

public class Boss : MonoBehaviour
{
    public Skill[] phase1Skills; // 第一阶段的技能数组
    public Skill[] phase2Skills; // 第二阶段的技能数组
    private int currentPhase = 1; // 当前阶段
    public float maxHealth = 1000f; // 最大生命值
    private float currentHealth; // 当前生命值

    void Start()
    {
        // 初始化当前生命值
        currentHealth = maxHealth;
    }

    void Update()
    {
        // 检查生命值是否降到一半以下，并且当前阶段是第一阶段
        if (currentHealth <= maxHealth / 2 && currentPhase == 1)
        {
            // 进入第二阶段
            TransitionToPhase2();
        }
    }

    void TransitionToPhase2()
    {
        // 切换到第二阶段
        currentPhase = 2;
        // 增加生命值和其他属性
        maxHealth *= 3;
        currentHealth = maxHealth;
        Debug.Log("Transitioned to Phase 2");
    }

    public void UseSkill(int skillIndex)
    {
        // 根据当前阶段使用对应的技能
        if (currentPhase == 1)
        {
            phase1Skills[skillIndex].UseSkill();
        }
        else
        {
            phase2Skills[skillIndex].UseSkill();
        }
    }

    public void TakeDamage(float damage)
    {
        // 减少当前生命值
        currentHealth -= damage;
        // 检查生命值是否小于等于0
        if (currentHealth <= 0)
        {
            // Boss死亡
            Die();
        }
    }

    void Die()
    {
        // Boss死亡逻辑
        Destroy(gameObject);
        Debug.Log("Boss died");
    }
}

