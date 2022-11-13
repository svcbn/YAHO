using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class MeetingHistoryData
{
    public string date;
    public List<string> keyword;
    public List<string> summary;
    public string transcripts;
    public List<string> teamMember; //아이디, 멤버이름이랑 시퀀스번호
    //?팀원
    //이미지는 어떻게 받을지
    public byte graph;
}

public enum MeetingRequestType
{
    POST,
    GET
}
public class MeetingGetTest : MonoBehaviour
{
    public Text date;
    public Text keyword;

    public Material image;

    public Text[] summary;
    public Text transcripts;
    public Text[] teamMember;

    public GameObject keywordGameobject;
    public GameObject teamMemberGameobject;

    public byte[] byteTexture;

    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    //이 버튼을 클릭했을 때 화면이 보이면서 같은 시간대에 조회가 요청된다.
    public void OnMeetingImage()
    {
        //조회요청
        string url = string.Format("http://15.165.47.243:9090/send");
        StartCoroutine(MeetingGetData(url));
    }

    
    IEnumerator MeetingGetData(string url)
    {
        //url값을 얻어온다.
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"에러:{req.error}");
        }

        
        if(req.result == UnityWebRequest.Result.Success)
        {
            string readData = req.downloadHandler.text;
            print($"읽은 값: {readData}");

            //MeetingHistoryData meetingHistoryData = JsonUtility.FromJson<MeetingHistoryData>(readData);

            JObject jObject = JObject.Parse(readData);
            print(jObject.ToString());

            date.text = jObject["date"].ToString();
            transcripts.text = jObject["text"].ToString();
            print(date.text);
            JToken jToken = jObject["members"];
            foreach(JToken x in jToken)
            {
                
                print(x["members"]);
            }
           // keyword[i].text = jObject["Keyword"].ToString();


            //date.text = meetingHistoryData.date;

            //transcripts.text = meetingHistoryData.transcripts;
           // print(meetingHistoryData.transcripts);
            
            //transcripts.text = Encoding.Default.GetString(meetingHistoryData.transcripts);


            //keyword.text = Encoding.Default.GetString(meetingHistoryData.keyword);
            /*keyword.text = meetingHistoryData.keyword.ToString();
            print(keyword.text);
                
            
            for(int j = 0; j < meetingHistoryData.summary.Length; j++)
            {
                summary[j].text = Encoding.Default.GetString(meetingHistoryData.summary);
            }
            for(int i = 0; i < meetingHistoryData.teamMember.Length; i++)
            {
                teamMember[i].text = Encoding.Default.GetString(meetingHistoryData.teamMember);
            }

            //summary.text = meetingHistoryData.summary;*/


/*
            for(int j = 0; j < meetingHistoryData.teamMember.Count; j++)
            {
                teamMember[j].text = meetingHistoryData.teamMember[j];
            }*/
            //Texture2D tex = new Texture2D(64, 64, textureFormat.)
            
            

        }
        
    }
}
