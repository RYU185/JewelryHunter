using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
// MonoBehaviour : ��� ������Ʈ�� �θ� : �ð����� ������Ʈ�� �ҷ���
{
    Rigidbody2D rbody; // 
    float axisH = 0.0f;
    public float velocity = 3.0f;

    public float jump = 3.0f;
    bool goJump = false; // ����Ű �������� �ȴ�������
    bool onGround = false; // �߿�! 2������������ ����ϴ� ���ӵ��� ����

    // animator
    Animator animator;
    string oldClip = "Idle";
    string newClip = "Idle";

    public static string gameState = "Playing"; // ��� �������� �� �� �ִ� gameState��� ����

    // Start is called before the first frame update
    void Start()
    // Ư�� ������Ʈ�� �����ϴ� �ʱ�ȭ �ڵ�
    {
        gameState = "Playing";
        rbody = this.GetComponent<Rigidbody2D>();
        // Application.targetFrameRate = 60;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() // 1/60�� , ��ǻ���� ��Ȳ���� ȣ������ ���� ���� ���� (ex. Ű �Է� �̺�Ʈ)
    {
        if (gameState != "Playing")
        {
            return;
        }
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


        // �ִϸ��̼� �÷��̴� Update()���� ó���ϴ� ���� ȿ������
        if (onGround)
        {
            if(axisH == 0)
            {
                newClip = "Idle";
            }
            else
            {
                newClip = "Run";
            }
        }
        else
        {
            newClip = "Jump";
        }

        if(oldClip != newClip)
        {
            oldClip = newClip;
            animator.Play(newClip);
        }
    }
    void FixedUpdate() // 0.02��, ������ ȣ����, �߿��� �ڵ�� �ݵ�� ȣ��Ǿ�� ��. ( ������, ���� ) 
    {
        if (gameState != "Playing")
        {
            return;
        }

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
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("�������� �Ϸ�");
            gameState = "gameClear";
            animator.Play("Clear");
            rbody.velocity = Vector2.zero;
        }
        //Ʈ���� ������� �浹�� ����
        else if (collision.gameObject.tag == "Dead")
        {
            gameState = "gameOver";
            animator.Play("Dead");
            rbody.velocity = Vector2.zero;
            GetComponent<CapsuleCollider2D>().enabled = false;
            // Dead Collision�� �ѹ� �浹�ϰ� ���Ĵ� �浹�� �����Ͽ� ����������
            // = CapsuleCollieder ��Ȱ��ȭ
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        }
        
    }
}
