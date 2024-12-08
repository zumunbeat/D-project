using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //// �̱��� �ν��Ͻ�
    //private static GameManager s_instance;
    //public static GameManager Instance { get { Init(); return s_instance; } } // �̱��� ������'
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
        //    DontDestroyOnLoad(go); // �� ��ȯ �ÿ��� ����
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
        // ���������� ���� Ȯ��
        List<string> choices = talkManager.GetChoices(id);
        if (choices != null)
        {
            // �������� �ִ� ���: ������ �г� Ȱ��ȭ �� ������ �ؽ�Ʈ ����
            choicePanel.SetActive(true);
            for (int i = 0; i < choices.Count; i++)
            {
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i];
                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
                //choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex, id));
            }
            return; // ������ ǥ�� �� ��ȭ ���� �ߴ�
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
