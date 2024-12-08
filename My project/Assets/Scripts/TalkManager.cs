using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    //데이터 구조가 키와 키에 연결된 밸류가 들어간다.(타입 두개 필요)
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, List<string>> choiceData = new Dictionary<int, List<string>>();
    public Sprite[] portraitArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>(); // portraitData 초기화
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(3000, new string[] { "여기는 어디야 대체?:0","!@#$!@누구!@#%:2","뭐야 저건 뭐라는거야:1","!@#!$%침입!@$^@:2","제거:3","어?:0" });
        //선택지가 있는 지문은 +100을 처리해주는건 어떨까?
        talkData.Add(4000, new string[] { "어느쪽을 고를것이냐?:2" });

        choiceData.Add(4000, new List<string> { "왼쪽 길로 간다", "오른쪽 길로 간다" }); // 선택지 추가

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
