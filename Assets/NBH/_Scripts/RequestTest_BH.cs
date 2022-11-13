using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun;

public enum RequestType
{
    POST,
    GET,
    PUT
}

public class MeetingData
{
    public string date;
    public List<ConversationData> scripts = new List<ConversationData>();
}

[System.Serializable]
public class ConversationData
{
    public string time;
    public string name;
    public string text;
}


public class ProjectData
{
    // ������Ʈ �̸�
    public string projectName;
    // ������Ʈ ��ǥ
    public string projectSubject;
    // ���� �����ϴ� ��� ���
    public int representativeMemberNo;
    // ������Ʈ ���� ��¥ (YYYY-MM-DD)
    public string startDate;
    // ������Ʈ ���� ��¥
    public string endDate;
    // ������Ʈ ���� ����Ʈ
    public List<int> projectMemberList;
}
 
public class HttpRequester : MonoBehaviour
{
    public string url;
    public RequestType requestType;

    public string postData;
    public System.Action<DownloadHandler> onComplete;
}

public class RequestTest_BH : MonoBehaviour
{
    public InputField inputProjectName;
    public InputField inputCoWorkers;
    public InputField inputScheduleStart;
    public InputField inputScheduleEnd;
    public InputField inputProjectGoal;
    public Button btnPush;

    public STTTest_BH stt;

    string _projectName = "pn";
    string _projectSubject = "ps";
    int _representativeMemberNo = 1;
    string _startDate = "2022-12-12";
    string _endDate = "2022-12-13";
    List<int> _projectMemberList = new List<int>() {1,2,3 };

    // Start is called before the first frame update
    void Start()
    {
        #region Listener
        inputProjectName.onEndEdit.AddListener(ProjectName);
        inputProjectName.onSubmit.AddListener(ProjectName);

        //inputCoWorkers.onValueChanged.AddListener(CoWorkers);
        //inputCoWorkers.onEndEdit.AddListener(CoWorkers);
        //inputCoWorkers.onSubmit.AddListener(CoWorkers);

        inputScheduleStart.onEndEdit.AddListener(StartDate);
        inputScheduleStart.onSubmit.AddListener(StartDate);

        inputScheduleEnd.onEndEdit.AddListener(EndDate);
        inputScheduleEnd.onSubmit.AddListener(EndDate);

        inputProjectGoal.onEndEdit.AddListener(ProjectGoal);
        inputProjectGoal.onSubmit.AddListener(ProjectGoal);

        btnPush.onClick.AddListener(onBtnPushClicked);
        #endregion

        meeting.date = System.DateTime.Now.ToString("yyyy-MM-dd");
        conversation.name = PhotonNetwork.NickName;
    }

    public void ProjectName(string s)
    {
        _projectName = s;
        Debug.Log(s);
    }

    public void CoWorkers(string s)
    {
        //_projectMemberList.Add(s);
        _projectMemberList = new List<int> {1,2,3,4,5,6};
        Debug.Log(s);
    }

    public void StartDate(string s)
    {
        _startDate = s;
        Debug.Log(s);
    }

    public void EndDate(string s)
    {
        _endDate = s;
        Debug.Log(s);
    }

    public void ProjectLeader(string s)
    {
        _representativeMemberNo = (int)1234;//int.Parse(s);
    }

    public void ProjectGoal(string s)
    {
        _projectSubject = s;
        Debug.Log(s);
    }

    

    public void onBtnPushClicked()
    {
        OnClickSignInNET();

    }

    public HttpRequester requester = new HttpRequester();
    public MeetingData meeting = new MeetingData();
    public ConversationData conversation = new ConversationData();

    public void AddMeetingData()
    {
        conversation.time = System.DateTime.Now.ToString("HH:mm:ss");   
        conversation.text = "�ȵ巹��, ���ÿ� ���� ���� �޾Ҿ��?";
        meeting.scripts.Add(conversation);
    }

    public void PostAI()
    {
        ///user , POST, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requester.url = "http://15.165.47.243:9090/";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(meeting, true);

        //post data ����
        //ProjectData data = new ProjectData();
        //data.projectName = _projectName;
        //data.projectSubject = _projectSubject;
        //data.representativeMemberNo = _representativeMemberNo;
        //data.startDate = _startDate;
        //data.endDate = _endDate;
        //data.projectMemberList = _projectMemberList;

        //requester.postData = JsonUtility.ToJson(data, true);
        //print(requester.postData);

        requester.onComplete = OnCompleteSignIn;

        //HttpManager���� ��û
        SendRequest(requester);
    }

    public void OnClickSignInNET()
    {
        //������ �Խù� ��ȸ ��û(/posts/1 , GET)
        //HttRequester�� ����
        HttpRequester requester = new HttpRequester();

        ///user , POST, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requester.url = "http://192.168.0.13:8001/projects";
        requester.requestType = RequestType.POST;

        //post data ����
        ProjectData data = new ProjectData();
        data.projectName = _projectName;
        data.projectSubject = _projectSubject;
        data.representativeMemberNo = _representativeMemberNo;
        data.startDate = _startDate;
        data.endDate = _endDate;
        data.projectMemberList = _projectMemberList;

        requester.postData = JsonUtility.ToJson(data, true);
        print(requester.postData);

        requester.onComplete = OnCompleteSignIn;

        //HttpManager���� ��û
        SendRequest(requester);
    }

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        ////�迭 �����͸� Ű���� �ִ´�.
        //string s = "{\"data\":" + handler.text + "}";

        ////List<PostData>
        //PostDataArray array = JsonUtility.FromJson<PostDataArray>(s);
        //for (int i = 0; i < array.data.Count; i++)
        //{
        //    print(array.data[i].id + "\n" + array.data[i].title + "\n" + array.data[i].body);
        //}

        //print("��ȸ �Ϸ�");
    }



    public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        switch(requester.requestType)
        {
            case RequestType.POST:
                webRequest = UnityWebRequest.Post(requester.url, requester.postData);
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(requester.postData);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.GET:
                webRequest = UnityWebRequest.Get(requester.url);
                break;
            case RequestType.PUT:
                webRequest = UnityWebRequest.Put(requester.url, requester.postData);
                break;
        }

        //������ ��û�� ������ ������ �ö����� ��ٸ���.
        yield return webRequest.SendWebRequest();

        //���࿡ ������ �����ߴٸ�
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);

            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            requester.onComplete(webRequest.downloadHandler);
            webRequest.Dispose();


        }
        //�׷����ʴٸ�
        else
        {
            //������� ����....��
            print("��� ����" + webRequest.result + "\n" + webRequest.error);
            webRequest.Dispose();

        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
