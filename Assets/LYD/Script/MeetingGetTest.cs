using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MeetingHistoryData
{
    public string date;
    public List<string> keyword;
    public string summary;
    public string transcripts;
    //public Liststring teamMember;
    //?����
    //�̹����� ��� ������
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
    public Text transcripts;
    public Text teamMember;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�� ��ư�� Ŭ������ �� ȭ���� ���̸鼭 ���� �ð��뿡 ��ȸ�� ��û�ȴ�.
    public void OnMeetingImage()
    {
        //��ȸ��û
        string url = string.Format("");
        StartCoroutine(MeetingGetData(url));
    }

    IEnumerator MeetingGetData(string url)
    {
        //url���� ���´�.
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"����:{req.error}");
        }

        if(req.result == UnityWebRequest.Result.Success)
        {
            string readData = req.downloadHandler.text;
            print($"���� ��: {readData}");

            MeetingHistoryData meetingHistoryData = JsonUtility.FromJson<MeetingHistoryData>(readData);
            date.text = meetingHistoryData.date;
            //keyword.text[] = meetingHistoryData.keyword;
            summary.text = meetingHistoryData.summary;
            transcripts.text = meetingHistoryData.transcripts;


        }
    }
}
