using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 6f;
    public float runSpeed; // ��ɫ�����ٶ�
    private Rigidbody2D rb; // Rigidbody2D���������
    private PlayerAnimation playerAnimation; // PlayerAnimation���������
    private SpriteRenderer spriteRenderer;
    private PhysicsCheck physicsCheck;
    private float move;

    //������
    public bool isHurt=false;
    public float hurtForce;

    public bool isDead;

    public Transform newPoint;


    void Start()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // ��ȡRigidbody2D���
        playerAnimation = GetComponent<PlayerAnimation>(); // ��ȡPlayerAnimation���
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal"); // ��ȡˮƽ����

        //���������˱�����״̬�����޷������ƶ�
        if(!isHurt && !isDead ) 
        {
            Move(); 
            Jump(); 
        }

       //Attack(); // ����Attack�������ƽ�ɫ����
    }

    // ���ƽ�ɫ�ƶ��ķ���
    void Move()
    {

        rb.velocity = new Vector2(move * runSpeed , rb.velocity.y);
        if (move > 0f)
        {
            spriteRenderer.flipX = false;
        }
        if(move < 0f)
        {
            spriteRenderer.flipX=true;
        }

    }

    // ���ƽ�ɫ��Ծ�ķ���
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) // ���������Ծ��ť
        {
            if(physicsCheck.isGround)
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse); // ����ɫ������ϵ�˲ʱ��

        }
    }

    public void GetHurt(Transform attacker)
    {
        Debug.Log("hurt");
        isHurt = true;
        rb.velocity = Vector2.zero;
        //��ñ������ķ���
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0);

        rb.AddForce(dir * hurtForce);
    }

    public void PlayerDead()
    {
        isDead = true;
        //�������޷�����
        SceneManager.LoadScene("EndScenes");    
    }

    public void Restart ()
    {
        Debug.Log("100000");
        transform.position = newPoint.position;
        GetComponent<Character>().currentHealth = 100f;
        isDead = false;
    }

}
