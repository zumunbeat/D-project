using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class flashlight : MonoBehaviour
{
    private SpriteRenderer parent_spriterenderer;
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        parent_spriterenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   //spriteRenderer.flipX = (dirX == -1);
        //right = false , left = true
        if (parent_spriterenderer.flipX == false)
        {
            //transform.position = parent.position + new Vector3(0.15f, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            //transform.position = parent.position + new Vector3(-0.15f, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
