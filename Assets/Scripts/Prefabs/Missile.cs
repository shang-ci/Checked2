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
            // 处理导弹与玩家的碰撞逻辑
            collision.gameObject.GetComponent<Character>().TakeDamage(10f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 处理导弹与障碍物的碰撞逻辑
            Destroy(gameObject);
        }
    }
}

