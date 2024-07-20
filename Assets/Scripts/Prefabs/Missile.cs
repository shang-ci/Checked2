using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ����������ҵ���ײ�߼�
            collision.gameObject.GetComponent<Character>().TakeDamage(10f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���������ϰ������ײ�߼�
            Destroy(gameObject);
        }
    }
}

