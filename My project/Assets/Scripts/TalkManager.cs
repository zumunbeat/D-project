using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    //������ ������ Ű�� Ű�� ����� ����� ����.(Ÿ�� �ΰ� �ʿ�)
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, List<string>> choiceData = new Dictionary<int, List<string>>();
    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>(); // portraitData �ʱ�ȭ
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(3000, new string[] { "����� ���� ��ü?:0","!@#$!@����!@#%:2","���� ���� ����°ž�:1","!@#!$%ħ��!@$^@:2","����:3","��?:0" });
        //�������� �ִ� ������ +100�� ó�����ִ°� ���?
        talkData.Add(4000, new string[] { "������� �����̳�?:2" });

        choiceData.Add(4000, new List<string> { "���� ��� ����", "������ ��� ����" }); // ������ �߰�

        portraitData.Add(3000 + 0, portraitArr[0]);
        portraitData.Add(3000 + 1, portraitArr[1]);
        portraitData.Add(3000 + 2, portraitArr[2]);
        portraitData.Add(3000 + 3, portraitArr[3]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);      //Get First Talk
            else
                return GetTalk(id - id % 100, talkIndex);        //Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
        
            return null;
        
        else
            return talkData[id][talkIndex];
    }
    public List<string> GetChoices(int id)
    {
        if (choiceData.ContainsKey(id))
            return choiceData[id];

        return null;
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
