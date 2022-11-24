using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class IndivMeetingData
{
    public List<ConversationData> conversation;
}

[System.Serializable]
public class IntegMeetingData : IndivMeetingData
{
    public int projectNo;
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


public class RoomManager_BH : MonoBehaviourPunCallbacks
{
    #region ui Component

    public Transform[] spawnPos;
    public Transform projectorPos;

    public Button btnCallUI;

    public GameObject btnMakeProject;
    public GameObject btnProjectInfo;
    public GameObject btnExitRoom;

    public GameObject panelMakeProject;
    public GameObject panelProjectInfo;

    public Button btnMakeProjectSubmit;
    public Button btnMakeProjectClose;
    public Button btnProjectInfoClose;

    public InputField inputProjectName;
    public InputField inputScheduleStart;
    public InputField inputScheduleEnd;
    public InputField inputProjectGoal;
    public Button btnPush;

    #endregion

    STTTest_BH stt;

    string _projectName; // = "pn";
    string _projectSubject; // = "ps";
    int _representativeMemberNo; // = 1;
    string _startDate; // = "2022-12-12";
    string _endDate; // = "2022-12-13";
    List<int> _projectMemberList; // = new List<int>() {1,2,3 };

    List<ConversationData> conversationList = new List<ConversationData>();
    string mergeText;

    // Start is called before the first frame update
    void Start()
    {
        //OnPhotonSerializeView ȣ�� ��
        PhotonNetwork.SerializationRate = 60;
        PhotonNetwork.SendRate = 60;

        //�÷��̾ �����Ѵ�.
        PhotonNetwork.Instantiate("Player", spawnPos[PhotonNetwork.CurrentRoom.PlayerCount-1].position, spawnPos[PhotonNetwork.CurrentRoom.PlayerCount-1].rotation);

        //PhotonNetwork.InstantiateRoomObject("Projector", projectorPos.position, projectorPos.rotation);

        stt = GetComponent<STTTest_BH>();

        #region Listner
        btnCallUI.onClick.AddListener(OnbtnCallUIClicked);
        btnMakeProject.GetComponent<Button>().onClick.AddListener(OnbtnMakeProjectClicked);
        btnProjectInfo.GetComponent<Button>().onClick.AddListener(OnbtnProjectInfoClicked);
        btnExitRoom.GetComponent<Button>().onClick.AddListener(OnbtnExitRoomClicked);
        btnMakeProjectSubmit.onClick.AddListener(OnbtnMakeProjectSubmitClicked);
        btnMakeProjectClose.onClick.AddListener(OnbtnMakeProjectCloseClicked);
        btnProjectInfoClose.onClick.AddListener(OnbtnProjectInfoCloseClicked);

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
    }

    #region Listener

    public void ProjectName(string s)
    {
        _projectName = s;
        Debug.Log(s);
    }

    public void CoWorkers(string s)
    {
        //_projectMemberList.Add(s);
        _projectMemberList = new List<int> { 1, 2, 3, 4, 5, 6 };
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

    void OnbtnCallUIClicked()
    {
        btnMakeProject.SetActive(true);
        btnProjectInfo.SetActive(true);
        btnExitRoom.SetActive(true);
    }

    void OnbtnMakeProjectClicked()
    {
        panelMakeProject.SetActive(true);
        btnMakeProject.SetActive(false);
        btnProjectInfo.SetActive(false);
        btnExitRoom.SetActive(false);

    }

    void OnbtnProjectInfoClicked()
    {
        panelProjectInfo.SetActive(true);
        btnMakeProject.SetActive(false);
        btnProjectInfo.SetActive(false);
        btnExitRoom.SetActive(false);

    }

    void OnbtnMakeProjectSubmitClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnMakeProjectCloseClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnProjectInfoCloseClicked()
    {
        panelProjectInfo.SetActive(false);
    }

    void OnbtnExitRoomClicked()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(1);
    }

    #endregion

    #region WebRequest
    public void PostMeeting()
    {
        HttpRequester requester = new HttpRequester();
        IntegMeetingData data = new IntegMeetingData();

        //data.projectNo = 0;
        //data.conversation = _conversation;

        requester.url = "";
        requester.requestType = RequestType.POST;
        requester.postData = JsonUtility.ToJson(data);
        Debug.Log(requester.postData);

        requester.onComplete = OnCompletePostMeeting;

        //HttpManager���� ��û
        WebRequester_BH.instance.SendRequest(requester);
    }

    // �̰� �����ؾߵ�
    public void OnClickSignInNET()
    {
        //������ �Խù� ��ȸ ��û(/posts/1 , GET)
        //HttRequester�� ����
        HttpRequester requester = new HttpRequester();

        ///user , POST, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requester.url = "http://192.168.0.105:8001/projects";
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

        //requester.onComplete = ;

        //HttpManager���� ��û
        WebRequester_BH.instance.SendRequest(requester);
    }

    #endregion

    #region DownloadHandler
    public void OnCompletePostMeeting(DownloadHandler handler)
    {

    }

    #endregion

    public void AddMeetingData()
    {
        ConversationData conversation = new ConversationData();

        conversation.name = PhotonNetwork.NickName;
        conversation.time = System.DateTime.Now.ToString("HH:mm:ss");
        conversation.text = stt.temp;
        conversationList.Add(conversation);

    }

    [PunRPC]
    public void RPCCollectMeetingData(string _mergeText)
    {
        mergeText = _mergeText;
        FromJsonMerge(mergeText);
    }

    void ToJsonConversion(List<ConversationData> conversationList)
    {
        mergeText = JsonUtility.ToJson(conversationList);
    }

    void FromJsonMerge(string mergeText)
    {
        ConversationData mergeData = new ConversationData();
        mergeData = JsonUtility.FromJson<ConversationData>(mergeText);
        conversationList.Add(mergeData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                ToJsonConversion(conversationList);
                photonView.RPC("RPCCollectMeetingData", RpcTarget.MasterClient, mergeText);
            }
            else
            {

            }
        }
    }

}
