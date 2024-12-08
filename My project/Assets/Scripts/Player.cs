using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Player : ObjectData
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D _collider;
    private float idle_time=0.0f;
    private bool isLongJump = false;
    private Vector2 direction;
   [SerializeField] private LayerMask groundLayer; // Inspector에서 레이어 설정 가능
    private float jumpTime;
    RaycastHit2D hit;
    public GameManager Gm;
    protected override void Start()
    {
        ID = 1;           // Player의 고유 ID 설정
        IsNPC = false;
        istalkAction = false;
        talkIndex = 0;
        maxHealth = 100;     // Player의 health 설정
        strength = 10; // Player의 공격력 설정
        maxMentality = 50;
        max_speed = 12;
        max_jump = 0.2f;
        max_jumptime = 0.1f;
        first_mentality = 0;
        base.Start();

        _collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (!Gm.isAction)
        {
            HandleMovement();
            HandleJump();
            HandleIdle();
            UpdateAnimations();
        }
            EventHandler();
    }

    void FixedUpdate()
    {
        Scan();
        UpdateGravityScale();
    }

    void EventHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                Debug.Log(hit.collider);
                Gm.Action(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("상호작용할 물체가 없습니다.");
            }
        }
    }
    void Scan()
    {
        Debug.DrawRay(rb.position, direction,Color.blue);
        // 'Object' 레이어에 있는 물체만 감지하고, tag로 세부적으로 구분
        hit= Physics2D.Raycast(transform.position, direction, 2.5f, LayerMask.GetMask("Object"));       
    }

    // 중력 조절 함수
    private void UpdateGravityScale()
    {
        if (isLongJump && rb.linearVelocity.y > 0)
        {
            rb.gravityScale = 1.0f; // 점프 중에는 중력을 약하게
        }
        else
        {
            rb.gravityScale = 4.5f; // 점프가 끝나거나 하강할 때 중력을 강하게
        }
    }

    // 이동 처리 함수
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            set_run(-1); // 왼쪽으로 이동
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            set_run(1); // 오른쪽으로 이동
        }
    }

    // 점프 처리 함수
    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Is_ground())
            {
                anim.SetBool("Is_Run", false); // 점프할 때 달리는 애니메이션 중지
            

                // 위 방향으로 점프 힘을 추가, Impulse 모드를 사용해 즉시 힘을 가함
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse); ;
                isLongJump = true;
                jumpTime = 0f; // 점프 시간을 초기화
            }
            else if (isLongJump && jumpTime < MaxJumpTime)
            {

                rb.AddForce(Vector2.up * jump /1.5f, ForceMode2D.Impulse); // 누르는 동안 지속해서 힘을 가함
                jumpTime += Time.deltaTime; // 스페이스바 누름 시간 누적
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLongJump = false;
        }
    }

    // 유휴 상태 처리 함수
    private void HandleIdle()
    {

        if (!Input.anyKey)
        {
            idle_time += Time.deltaTime;
            if (idle_time > 1.0f)
            {
                set_idle(); // 유휴 상태로 전환
            }
            anim.SetFloat("Idle", idle_time);
        }
        else
        {
            idle_time = 0f; // 키 입력이 있을 때 유휴 시간을 초기화
        }
    }

    // 애니메이션 상태 업데이트 함수
    private void UpdateAnimations()
    {
        bool isGrounded = Is_ground();
        anim.SetBool("IsGround", isGrounded); // 바닥에 있는지 여부 업데이트

        if (!isGrounded)
        {
            anim.SetFloat("JumpBlend", rb.linearVelocity.normalized.y); // 점프 중일 때 점프 상태 업데이트
        }
    }

    // 유휴 상태로 전환
    void set_idle()
    {
        anim.SetBool("Is_Run", false);
    }

    // 이동 처리 함수
    void set_run(int dirX)
    {
        anim.SetBool("Is_Run", true); // 달리는 애니메이션 재생

        // 방향에 따라 스프라이트 뒤집기
        spriteRenderer.flipX = (dirX == -1);

        // 플레이어 속도가 최대 속도보다 작을 때만 힘을 추가
        float speedX = Mathf.Abs(rb.linearVelocity.x);
        if (speedX < 6.0f)
        {
            rb.AddForce(new Vector2(dirX, 0) * speed);
        }

    }

    // 바닥 감지 함수
    bool Is_ground()
    {
        Vector2 rayOrigin = _collider.bounds.center; // 레이의 시작 지점
        float rayLength = 0.8f; // 레이의 길이
        Vector2 rayDirection = Vector2.down; // 레이의 방향

        // Raycast 수행
        RaycastHit2D ray = Physics2D.Raycast(
            rayOrigin,
            rayDirection, // 레이의 방향
            rayLength,
            groundLayer // 충돌을 감지할 레이어
        );
        
        // 디버그: 레이 보이게 하기
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);


        // true라면 땅에 있고 false라면 공중에 있는 것
        return ray.collider != null;
    }
}