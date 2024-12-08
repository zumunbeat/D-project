using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : ObjectData
{
    public GameManager Gm;
    Rigidbody2D rigid;
    public int nextMove;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public bool sensor_on = false;
    private GameObject player = null;
    public float chase_speed = 1.2f;
    private Vector2 ptomdirection;
    protected override void Start()
    {
        ID = 1000;
        IsNPC = true;
        istalkAction = false;
        talkIndex = 0;
        maxHealth = 100;     
        strength = 3;
        maxMentality = 0;
        max_speed = 1;
        max_jump = 0f;
        max_jumptime = 0f;
        first_mentality = 0;
        base.Start();


        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 3);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (anim.GetBool("Chase")) anim.SetBool("Chase", false);
        set_vector();
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 0.2f, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null && !Gm.isAction) Turn();
    }
    void set_vector()
    {
        if (!Gm.isAction)
        {
            if (sensor_on == true) Chase();
            else rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);
            
        }
        else rigid.linearVelocity = Vector2.zero;
    }
    void Turn()
    {
        sensor_on = false;
        player = null;
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;
        CancelInvoke();
        Invoke("Think", 3);
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
        if (!Gm.isAction && sensor_on == false) { Invoke("Think", nextThinkTime); }
        anim.SetInteger("State", nextMove);
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == -1;
        }
    }
    void Chase()
    {
        if(player == null)
        {
            Debug.Log("player is null");
            return;
        }
        anim.SetBool("Chase", true);
        // ���ΰ� �������� �̵�
        ptomdirection = (player.transform.position - transform.position).normalized;
        rigid.linearVelocity = new Vector2(ptomdirection.x*chase_speed, rigid.linearVelocity.y);

        // ���Ͱ� ���ΰ��� �ٶ󺸰� �ϱ�
        if (ptomdirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (ptomdirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    public void set_player(GameObject obj)
    {
        if (obj.tag != "Player")
        {
            Debug.Log("parameter isn't player");
            return;
        }
        player = obj;
    }
}
