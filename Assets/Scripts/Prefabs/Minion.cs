using UnityEngine;

public class Minion : MonoBehaviour
{
    // �����ʹӱ���ɱ���¼�
    public delegate void MinionKilledHandler(GameObject minion);
    public event MinionKilledHandler OnMinionKilled;
    public float health = 50f; // �ʹӵ�����ֵ

    public void TakeDamage(float damage)
    {
        // �����ʹӵ�����ֵ
        health -= damage;
        // �������ֵ�Ƿ�С�ڵ���0
        if (health <= 0)
        {
            // �����ʹ������¼�
            if (OnMinionKilled != null)
            {
                OnMinionKilled(gameObject);
            }
            // �����ʹ�
            Destroy(gameObject);
        }
    }

    public void Enhance()
    {
        // ��ǿ�ʹӵ�����
        health *= 2;
        Debug.Log("Minion enhanced");
    }
}

