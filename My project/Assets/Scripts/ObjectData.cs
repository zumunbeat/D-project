using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    //ID����
    [SerializeField]
    private int _id;
    public int ID { get; protected set; } 
    // NPC���� �ƴ��� ����
    public bool IsNPC { get; protected set; }

    // ��� ����
    public bool istalkAction;
    public int talkIndex;

    // �⺻ ����
    protected int maxHealth;
    public int currentHealth { get; private set; }

    protected int maxMentality;
    protected int first_mentality;
    public int currentMental { get; private set; }

    protected int max_speed;
    public int speed { get; private set; }

    protected float max_jump;
    public float jump { get; private set; }

    protected float max_jumptime;// �ִ� ���� �ð�
    
    public float MaxJumpTime {  get; private set; }
    protected int strength;

    // ���� �ʱ�ȭ
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentMental = first_mentality;
        speed = max_speed;
        jump = max_jump;
        MaxJumpTime = max_jumptime;
        _id = ID;
    }

    // ü�� ȸ��
    public void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    // ���� ȸ��
    public void RestoreMental(int value)
    {
        currentMental += value;
        if (currentMental > maxMentality)
            currentMental = maxMentality;
    }

    // ������ �ޱ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ĳ���� ��� ó��
    void Die()
    {
        Debug.Log("Character has died.");
        // ��� ���� �߰� ����
    }

    // ���ŷ� ���
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
    //���� value��ŭ ���ǵ带 �����մϴ�.
    public void change_speed(int value)
    {
        speed = value;
    }
    //���� value��ŭ �������� �����մϴ�.
    public void change_jump(int value)
    {
        jump = value;
    }
    public void change_maxjumptime(float value)
    {
        MaxJumpTime = value;
    }
}
