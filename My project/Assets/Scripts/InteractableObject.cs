using UnityEngine;

public class InteractableObject : ObjectData
{
    public GameObject InduceObject;
    private Animation anim;

    protected override void Start()
    {
        //일단 true로 해둠 나중에 고치셈
        base.ID = 3000;
        IsNPC = true;
        InduceObject = transform.GetChild(0).gameObject;
        anim = InduceObject.GetComponent<Animation>();
        InduceObject.SetActive(false);
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어가 트리거에 들어오면
        {
            InduceObject.SetActive(true);
            anim.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어가 트리거에서 나가면
        {
            InduceObject.SetActive(false);
            anim.Stop();
        }
    }
}