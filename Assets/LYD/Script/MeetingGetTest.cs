using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Meeting
{
    public string date;
    public List<string> keyword;
    public string summary;
    public string transcript;
    public string teamMember;
    //?팀원
    //이미지는 어떻게 받을지
    public byte[] graph;
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
    public Text summary;
    public Text transcrpit;

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
        string url = string.Format("");
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

        }
    }
}
