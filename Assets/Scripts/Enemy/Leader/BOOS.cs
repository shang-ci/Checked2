using UnityEngine;

public class Boss : MonoBehaviour
{
    public Skill[] phase1Skills; // ��һ�׶εļ�������
    public Skill[] phase2Skills; // �ڶ��׶εļ�������
    private int currentPhase = 1; // ��ǰ�׶�
    public float maxHealth = 1000f; // �������ֵ
    private float currentHealth; // ��ǰ����ֵ

    void Start()
    {
        // ��ʼ����ǰ����ֵ
        currentHealth = maxHealth;
    }

    void Update()
    {
        // �������ֵ�Ƿ񽵵�һ�����£����ҵ�ǰ�׶��ǵ�һ�׶�
        if (currentHealth <= maxHealth / 2 && currentPhase == 1)
        {
            // ����ڶ��׶�
            TransitionToPhase2();
        }
    }

    void TransitionToPhase2()
    {
        // �л����ڶ��׶�
        currentPhase = 2;
        // ��������ֵ����������
        maxHealth *= 3;
        currentHealth = maxHealth;
        Debug.Log("Transitioned to Phase 2");
    }

    public void UseSkill(int skillIndex)
    {
        // ���ݵ�ǰ�׶�ʹ�ö�Ӧ�ļ���
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
        // ���ٵ�ǰ����ֵ
        currentHealth -= damage;
        // �������ֵ�Ƿ�С�ڵ���0
        if (currentHealth <= 0)
        {
            // Boss����
            Die();
        }
    }

    void Die()
    {
        // Boss�����߼�
        Destroy(gameObject);
        Debug.Log("Boss died");
    }
}

