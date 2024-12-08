using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private Transform player; // �÷��̾��� Transform
    private SpriteRenderer spriteRenderer; // ������ SpriteRenderer
    public Sprite Idle; // ��� ����
    public Sprite neSprite; // �ϵ��� ��������Ʈ
    public Sprite nwSprite; // �ϼ��� ��������Ʈ
    public Sprite swSprite; // ������ ��������Ʈ
    public Sprite seSprite; // ������ ��������Ʈ

    private bool isPlayerInRange = false; // �÷��̾ Ž�� ������ �ִ��� ����

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (isPlayerInRange)
        {
            // �÷��̾�� ���� ���� ���� ���
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ���� ���

            // ������ 0-360 ������ ��ȯ
            if (angle < 0) angle += 360;

            // ������ ���� ��������Ʈ ����
            if (angle >= 0 && angle < 90)
            {
                spriteRenderer.sprite = neSprite; // �ϵ�
            }
            else if (angle >= 90 && angle < 180)
            {
                spriteRenderer.sprite = nwSprite; // �ϼ� 
            }
            else if (angle >= 180 && angle < 270)
            {
                spriteRenderer.sprite = swSprite; // ����
            }
            else
            {
                spriteRenderer.sprite = seSprite; // ����
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true; // �÷��̾ Ž�� ������ ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false; // �÷��̾ Ž�� �������� ����
        }
    }
}
