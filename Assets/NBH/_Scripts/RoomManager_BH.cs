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

[System.Serializable]
public class RoomManager_BH : MonoBehaviourPunCallbacks
{
    public SlidiingUI_BH uiAnim;

    [Header("Main")]
    public Transform[] spawnPos;
    public Transform projectorPos;

    public Button btnCallUI;
    public Button btnExitRoom;

    public Transform panelCoWorker;
    GameObject coWorkerPrefab;
    public Button btnSubmit;

    [Header("MakeProject")]
    #region ������Ʈ ���� component

    public Button btnMakeProject;
    public GameObject panelMakeProject;

    public Button btnMakeProjectSubmit;
    public Button btnMakeProjectClose;

    public InputField inputProjectName;
    public GameObject DateStart;
    public GameObject DateEnd;
    public InputField inputProjectGoal;

    public Button btnFindMember;
    public Transform panelCoWorkers;
    public GameObject panelFindMember;
    public InputField inputFindMember;
    public Transform SearchResultContent;
    public Button btnFindMemberClose;
    GameObject findMemberPrefab;

    public Button btnPush;
    #endregion

    [Header("SelectProject")]
    #region ������Ʈ ���� component

    public Button btnSelectProject;
    public GameObject panelSelectProject;
    public Button btnSelectProjectClose;

    #endregion

    [Header("TeamTask")]
    #region �� ���� Component

    public Button btnTeamTask;
    public GameObject panelTeamTask;
    public Button btnTeamTaskClose;
    #endregion

    string _projectName;
    string _projectSubject;
    int _representativeMemberNo;
    string _startDate;
    string _endDate;
    List<int> _projectMemberList;

    STTTest_BH stt;

    List<ConversationData> conversationList = new List<ConversationData>();
    string mergeText;

    public GameObject imgLoading;

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
        coWorkerPrefab = (GameObject)Resources.Load("BtnCoworker");
        findMemberPrefab = (GameObject)Resources.Load("BtnFindMember");

        #region Listner
        //btnCallUI.onClick.AddListener(OnbtnCallUIClicked);
        btnMakeProject.onClick.AddListener(OnbtnMakeProjectClicked);
        btnTeamTask.onClick.AddListener(OnbtnTeamTaskClicked);
        btnExitRoom.onClick.AddListener(OnbtnExitRoomClicked);
        btnMakeProjectSubmit.onClick.AddListener(OnbtnMakeProjectSubmitClicked);
        btnMakeProjectClose.onClick.AddListener(OnbtnMakeProjectCloseClicked);
        btnTeamTaskClose.onClick.AddListener(OnbtnTeamTaskCloseClicked);
        btnFindMember.onClick.AddListener(OnbtnFindMemberClicked);

        //inputFindMember.onEndEdit.AddListener();
        //inputFindMember.OnSubmit.AddListener();


        inputProjectName.onEndEdit.AddListener(ProjectName);
        //inputProjectName.onSubmit.AddListener(ProjectName);

        //inputCoWorkers.onValueChanged.AddListener(CoWorkers);
        //inputCoWorkers.onEndEdit.AddListener(CoWorkers);
        //inputCoWorkers.onSubmit.AddListener(CoWorkers);

        //inputScheduleStart.onEndEdit.AddListener(StartDate);
        //inputScheduleStart.onSubmit.AddListener(StartDate);

        //inputScheduleEnd.onEndEdit.AddListener(EndDate);
        //inputScheduleEnd.onSubmit.AddListener(EndDate);

        inputProjectGoal.onEndEdit.AddListener(ProjectGoal);
        //inputProjectGoal.onSubmit.AddListener(ProjectGoal);

        #endregion

        _representativeMemberNo = GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().MemberNo;

        panelFindMember.SetActive(false);
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


    //void OnbtnCallUIClicked()
    //{
    //    btnMakeProject.SetActive(true);
    //    btnTeamTask.SetActive(true);
    //    btnExitRoom.SetActive(true);
    //}

    void OnbtnMakeProjectClicked()
    {
        panelMakeProject.SetActive(true);
        StartCoroutine(uiAnim.SlideClose());

        //btnMakeProject.SetActive(false);
        //btnTeamTask.SetActive(false);
        //btnExitRoom.SetActive(false);

    }

    void OnbtnTeamTaskClicked()
    {
        panelTeamTask.SetActive(true);
        StartCoroutine(uiAnim.SlideClose());

        //btnMakeProject.SetActive(false);
        //btnTeamTask.SetActive(false);
        //btnExitRoom.SetActive(false);

    }

    void OnbtnMakeProjectSubmitClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnMakeProjectCloseClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnTeamTaskCloseClicked()
    {
        panelTeamTask.SetActive(false);
    }

    void OnbtnExitRoomClicked()
    {
        imgLoading.SetActive(true);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(1);
    }

    void OnbtnFindMemberClicked()
    {
        panelFindMember.SetActive(true);
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
        Debug.Log( "��ȯ��� : " + conversation.text);
        

    }

    [PunRPC]
    public void RPCCollectMeetingData(string _mergeText)
    {
        FromJsonMerge(_mergeText);
    }

    void ToJsonConversion(List<ConversationData> conversationList)
    {
        IndivMeetingData data = new IndivMeetingData();
        data.conversation = conversationList;
        mergeText = JsonUtility.ToJson(data);
    }

    void FromJsonMerge(string mergeText)
    {
        IndivMeetingData data = new IndivMeetingData();

        data = JsonUtility.FromJson<IndivMeetingData>(mergeText);
        foreach(ConversationData conversation in data.conversation)
        {
            conversationList.Add(conversation);
        }
        

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
            
        }
    }

}
