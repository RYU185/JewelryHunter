using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
// MonoBehaviour : 모든 컴포넌트의 부모 : 시간마다 업데이트를 불러옴
{
    Rigidbody2D rbody; // 
    float axisH = 0.0f;
    public float velocity = 3.0f;

    public float jump = 3.0f;
    bool goJump = false; // 점프키 눌렀는지 안눌렀는지
    bool onGround = false; // 중요! 2단점프까지는 허용하는 게임들이 많음

    // animator
    Animator animator;
    string oldClip = "Idle";
    string newClip = "Idle";

    public static string gameState = "Playing"; // 모든 영역에서 볼 수 있는 gameState라는 변수

    // Start is called before the first frame update
    void Start()
    // 특정 컴포넌트를 세팅하는 초기화 코드
    {
        gameState = "Playing";
        rbody = this.GetComponent<Rigidbody2D>();
        // Application.targetFrameRate = 60;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() // 1/60초 , 컴퓨터의 상황마다 호출하지 않을 수도 있음 (ex. 키 입력 이벤트)
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

        // 수평의 값을 받는 변수 = Input안에 특정한 키의 입력값을 받아오기
        // <-, ->를 -, +로 받아서 처리함
        /*        if (axisH != 0)
                {
                    Debug.Log(axisH);
                }*/


        // 애니메이션 플레이는 Update()에서 처리하는 것이 효율적임
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
    void FixedUpdate() // 0.02초, 무조건 호출함, 중요한 코드는 반드시 호출되어야 함. ( 움직임, 동작 ) 
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
        // velocity =  무조건 벡터단위 (2차원 벡터 (x, y) 좌표계 단위)
        // 0 으로 세팅하면 안됨. 이미 중력의 영향을 받고있는 대상이기때문에
        // 원래 값 그대로 세팅해야함 = rbody.velocity.y
        // FixedUpdate가 존재하는 이유
        // 출력되는 컴퓨터의 사양마다 달리는 속도가 차이가 나면
        // 형평성에 어긋나기 때문

        // Ground 레이어를 설정한 블럭에 서있는지 판정하는 코드
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f),
            LayerMask.GetMask("Ground"));
        // Linecast (source, target, 탐지해야할 레이어) source와 target 사이에 레이저를 발사
        // Raycast (source, 방향, 거리)

        if (onGround && goJump)
        {
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            goJump = false;
        }
    }


    public void Jump()
    {
        goJump = true; 
        // 함수를 따로 만들어놓는 이유 : 재사용성 (타 플랫폼에서의 재사용성을 고려함)
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("스테이지 완료");
            gameState = "gameClear";
            animator.Play("Clear");
            rbody.velocity = Vector2.zero;
        }
        //트리거 방식으로 충돌을 감지
        else if (collision.gameObject.tag == "Dead")
        {
            gameState = "gameOver";
            animator.Play("Dead");
            rbody.velocity = Vector2.zero;
            GetComponent<CapsuleCollider2D>().enabled = false;
            // Dead Collision에 한번 충돌하고 이후는 충돌을 무시하여 떨어져야함
            // = CapsuleCollieder 비활성화
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        }
        
    }
}
