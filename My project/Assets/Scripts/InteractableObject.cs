using UnityEngine;

public class InteractableObject : ObjectData
{
    public GameObject InduceObject;
    private Animation anim;

    protected override void Start()
    {
        //�ϴ� true�� �ص� ���߿� ��ġ��
        base.ID = 3000;
        IsNPC = true;
        InduceObject = transform.GetChild(0).gameObject;
        anim = InduceObject.GetComponent<Animation>();
        InduceObject.SetActive(false);
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾ Ʈ���ſ� ������
        {
            InduceObject.SetActive(true);
            anim.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾ Ʈ���ſ��� ������
        {
            InduceObject.SetActive(false);
            anim.Stop();
        }
    }
}