using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
// MonoBehaviour : ��� ������Ʈ�� �θ� : �ð����� ������Ʈ�� �ҷ���
{
    Rigidbody2D rbody; // 
    float axisH = 0.0f;
    public float velocity = 3.0f;

    public float jump = 6.0f;
    bool goJump = false; // ����Ű �������� �ȴ�������
    bool onGround = false; // �߿�! 2������������ ����ϴ� ���ӵ��� ����

    // Start is called before the first frame update
    void Start()
    // Ư�� ������Ʈ�� �����ϴ� �ʱ�ȭ �ڵ�
    {
        rbody = this.GetComponent<Rigidbody2D>();
        // Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update() // 1/60�� , ��ǻ���� ��Ȳ���� ȣ������ ���� ���� ���� (ex. Ű �Է� �̺�Ʈ)
    {
       // Debug.Log("FPS: " + (1.0f / Time.deltaTime));

        axisH = Input.GetAxisRaw("Horizontal");
        // Edit - project setting - input manager

        if(axisH >0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // ������ ���� �޴� ���� = Input�ȿ� Ư���� Ű�� �Է°��� �޾ƿ���
        // <-, ->�� -, +�� �޾Ƽ� ó����
/*        if (axisH != 0)
        {
            Debug.Log(axisH);
        }*/
    }
    void FixedUpdate() // 0.02��, ������ ȣ����, �߿��� �ڵ�� �ݵ�� ȣ��Ǿ�� ��. ( ������, ���� ) 
    {
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * velocity, rbody.velocity.y);
        }
        // Debug.Log(Time.fixedDeltaTime);
        // velocity =  ������ ���ʹ��� (2���� ���� (x, y) ��ǥ�� ����)
        // 0 ���� �����ϸ� �ȵ�. �̹� �߷��� ������ �ް��ִ� ����̱⶧����
        // ���� �� �״�� �����ؾ��� = rbody.velocity.y
        // FixedUpdate�� �����ϴ� ����
        // ��µǴ� ��ǻ���� ��縶�� �޸��� �ӵ��� ���̰� ����
        // ���򼺿� ��߳��� ����

        // Ground ���̾ ������ ���� ���ִ��� �����ϴ� �ڵ�
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f),
            LayerMask.GetMask("Ground"));
        // Linecast (source, target, Ž���ؾ��� ���̾�) source�� target ���̿� �������� �߻�
        // Raycast (source, ����, �Ÿ�)

        if (onGround && goJump)
        {
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            goJump = false;
        }
    }


    public void Jump()
    {
        goJump = true; 
        // �Լ��� ���� �������� ���� : ���뼺 (Ÿ �÷��������� ���뼺�� �����)
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            Debug.Log("�������� �Ϸ�");
        }
        
    }
}
