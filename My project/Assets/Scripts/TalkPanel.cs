using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkPanel : MonoBehaviour
{
    public GameObject dialogueUI; // 대화창 UI
    public TMP_Text dialogueText; // 대사 텍스트
    //public GameObject scanObject;
    //public Button choice1Button; // 첫 번째 선택지 버튼
    //public Button choice2Button; // 두 번째 선택지 버튼
    // Start is called before the first frame update
    void Start()
    {
        // 선택지 버튼에 리스너 추가
        //choice1Button.onClick.AddListener(() => GameManager.Dialogue.Choice1(dialogueText));
        //choice2Button.onClick.AddListener(() => GameManager.Dialogue.Choice2(dialogueText));
        dialogueText = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
