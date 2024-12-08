using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    //ID생성
    [SerializeField]
    private int _id;
    public int ID { get; protected set; } 
    // NPC인지 아닌지 여부
    public bool IsNPC { get; protected set; }

    // 대사 관련
    public bool istalkAction;
    public int talkIndex;

    // 기본 스탯
    protected int maxHealth;
    public int currentHealth { get; private set; }

    protected int maxMentality;
    protected int first_mentality;
    public int currentMental { get; private set; }

    protected int max_speed;
    public int speed { get; private set; }

    protected float max_jump;
    public float jump { get; private set; }

    protected float max_jumptime;// 최대 점프 시간
    
    public float MaxJumpTime {  get; private set; }
    protected int strength;

    // 스탯 초기화
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentMental = first_mentality;
        speed = max_speed;
        jump = max_jump;
        MaxJumpTime = max_jumptime;
        _id = ID;
    }

    // 체력 회복
    public void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    // 마나 회복
    public void RestoreMental(int value)
    {
        currentMental += value;
        if (currentMental > maxMentality)
            currentMental = maxMentality;
    }

    // 데미지 받기
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 캐릭터 사망 처리
    void Die()
    {
        Debug.Log("Character has died.");
        // 사망 관련 추가 로직
    }

    // 정신력 사용
    public void UseMental(int amount)
    {
        if (currentMental >= amount)
        {
            currentMental -= amount;
        }
        else
        {
            Debug.Log("Not enough mental.");
        }
    }
    //받은 value만큼 스피드를 수정합니다.
    public void change_speed(int value)
    {
        speed = value;
    }
    //받은 value만큼 점프값을 수정합니다.
    public void change_jump(int value)
    {
        jump = value;
    }
    public void change_maxjumptime(float value)
    {
        MaxJumpTime = value;
    }
}
