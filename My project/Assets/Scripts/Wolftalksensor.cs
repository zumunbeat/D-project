using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolftalksensor : MonoBehaviour
{
    private Wolf parent;
    private SpriteRenderer parent_spriterenderer;
    public bool _IsTalkSensor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<Wolf>();
        parent_spriterenderer = parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sensor_flipX();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _IsTalkSensor == false)
        {
            if (parent.istalkAction == false) // 대화 중이 아닐 때만 호출
            {
            }
            _IsTalkSensor =true;
        }
    }
    
    public void sensor_flipX()
    {
        if (parent_spriterenderer.flipX == false)
        {
            transform.position = parent.transform.position + new Vector3(2.5f, 0, 0);
        }
        else if (parent_spriterenderer.flipX == true)
        {
            transform.position = parent.transform.position + new Vector3(-2.5f, 0, 0);
        }
    }
}
