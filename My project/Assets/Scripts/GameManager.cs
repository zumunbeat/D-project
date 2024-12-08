using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //// 싱글톤 인스턴스
    //private static GameManager s_instance;
    //public static GameManager Instance { get { Init(); return s_instance; } } // 싱글톤 접근자'
    public TalkManager talkManager;
    public GameObject talkPanel;
    public TMP_Text talkText;
    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex = 0;
    public Image portraitImg;
    public GameObject choicePanel;
    public Button[] choiceButtons;
    private void Awake()
    {
    }
    
    static void Init()
    {
        //if (s_instance == null)
        //{
        //    GameObject go = GameObject.Find("@GameManager");
        //    if (go == null)
        //    {
        //        go = new GameObject { name = "@GameManager" };
        //        go.AddComponent<GameManager>();
        //    }
        //
        //    DontDestroyOnLoad(go); // 씬 전환 시에도 유지
        //    s_instance = go.GetComponent<GameManager>();
        //
        //   
        //}
    }


    
    public void Action(GameObject scanObj)
    {
        isAction = true;
        ObjectData objData = scanObj.GetComponent<ObjectData>();
        Debug.Log(objData.ID);
        Talk(objData.ID, objData.IsNPC);
        

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        // 선택지인지 여부 확인
        List<string> choices = talkManager.GetChoices(id);
        if (choices != null)
        {
            // 선택지가 있는 경우: 선택지 패널 활성화 및 선택지 텍스트 설정
            choicePanel.SetActive(true);
            for (int i = 0; i < choices.Count; i++)
            {
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i];
                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners(); // 기존 이벤트 제거
                //choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex, id));
            }
            return; // 선택지 표시 후 대화 진행 중단
        }

        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null )
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }

        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
