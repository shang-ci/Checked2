using UnityEngine;

public class Shield : MonoBehaviour
{
    private float shieldValue; // ����ֵ

    public void SetShieldValue(float value)
    {
        // ���û���ֵ
        shieldValue = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �����ײ�����ǲ������
        if (other.CompareTag("Player"))
        {
            // ���������˺�
            other.GetComponent<Player>().TakeDamage(10f);
            // ���ٻ���ֵ
            shieldValue -= 10f;
            // ��黤��ֵ�Ƿ�С�ڵ���0
            if (shieldValue <= 0)
            {
                // ���ٻ���
                Destroy(gameObject);
                Debug.Log("Shield destroyed");
            }
        }
    }
}

