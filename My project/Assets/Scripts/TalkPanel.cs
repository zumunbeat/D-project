using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkPanel : MonoBehaviour
{
    public GameObject dialogueUI; // ��ȭâ UI
    public TMP_Text dialogueText; // ��� �ؽ�Ʈ
    //public GameObject scanObject;
    //public Button choice1Button; // ù ��° ������ ��ư
    //public Button choice2Button; // �� ��° ������ ��ư
    // Start is called before the first frame update
    void Start()
    {
        // ������ ��ư�� ������ �߰�
        //choice1Button.onClick.AddListener(() => GameManager.Dialogue.Choice1(dialogueText));
        //choice2Button.onClick.AddListener(() => GameManager.Dialogue.Choice2(dialogueText));
        dialogueText = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
