using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private Transform player; // 플레이어의 Transform
    private SpriteRenderer spriteRenderer; // 몬스터의 SpriteRenderer
    public Sprite Idle; // 평소 상태
    public Sprite neSprite; // 북동쪽 스프라이트
    public Sprite nwSprite; // 북서쪽 스프라이트
    public Sprite swSprite; // 남서쪽 스프라이트
    public Sprite seSprite; // 남동쪽 스프라이트

    private bool isPlayerInRange = false; // 플레이어가 탐지 구역에 있는지 여부

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (isPlayerInRange)
        {
            // 플레이어와 몬스터 간의 방향 계산
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산

            // 각도를 0-360 범위로 변환
            if (angle < 0) angle += 360;

            // 각도에 따라 스프라이트 변경
            if (angle >= 0 && angle < 90)
            {
                spriteRenderer.sprite = neSprite; // 북동
            }
            else if (angle >= 90 && angle < 180)
            {
                spriteRenderer.sprite = nwSprite; // 북서 
            }
            else if (angle >= 180 && angle < 270)
            {
                spriteRenderer.sprite = swSprite; // 남서
            }
            else
            {
                spriteRenderer.sprite = seSprite; // 남동
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true; // 플레이어가 탐지 구역에 들어옴
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false; // 플레이어가 탐지 구역에서 나감
        }
    }
}
