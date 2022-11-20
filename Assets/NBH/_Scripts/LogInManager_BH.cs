using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SignInData
{
    public string memberId;
    public string memberPw;
}

public class MemberData
{
    public string memberId;
    public string memberPw;
    public string name;
    public string phone;
    public string email;
    public string address;
}

public class LogInManager_BH : MonoBehaviourPunCallbacks
{
    WebRequester_BH webRequester;

    [Header("Main")]
    // �����г�
    public GameObject panelMain;
    #region ����ȭ�� component

    // ���̵� InputField
    public InputField inputID;

    // ��й�ȣ IF
    public InputField inputPW;

    // �α��� ��ư
    public Button btnLogIn;

    // ���̵� ã�� ��ư
    //public Button btnFindID;

    // ��й�ȣ ã�� ��ư
    //public Button btnFindPW;

    // ȸ������ ��ư
    public Button btnSignUp;

    // �α��ο� ����
    string _logInMemberId;
    string _logInMemberPw;

    #endregion

    //[Header("FindID")]
    // ���̵� ã�� �г�
    //public GameObject panelFindID;
    #region ���̵� ã�� component

    // ���̵� ã�� �̸�
    //public InputField inputFindIDName;

    // ���̵� ã�� �̸���
    //public InputField inputFindIDEmail;

    // ���̵� ã�� ��ư
    //public Button btnFindIDSubmit;

    // ���̵� ã�� ���ư���
    //public Button btnFindIDReturn;

    #endregion

    //[Header("FindPW")]
    // ��й�ȣ ã�� �г�
    //public GameObject panelFindPW;
    #region ��й�ȣ ã�� component

    // ��й�ȣ ã�� ���̵�
    //public InputField inputFindPWID;

    // ��й�ȣ ã�� �̸�
    //public InputField inputFindPWName;

    // ��й�ȣ ã�� �̸���
    //public InputField inputFindPWEmail;

    // ��й�ȣ ã�� ��ư
    //public Button btnFindPWSubmit;

    // ��й�ȣ ã�� ���ư���
    //public Button btnFindPWReturn;

    #endregion

    [Header("SignUp")]
    // ȸ������ �г�
    public GameObject panelSignUp;
    #region ȸ������ component

    [Header(">ID")]
    // ȸ������ ���̵�
    public InputField inputSignUpID;

    // ȸ������ ���̵� �˻� ��ư
    //public Button btnSignUpIDCheck;

    // ȸ������ ���̵� �˻� ����
    public GameObject imageSignUpIDGood;

    // ȸ������ ���̵� �˻� ����
    public GameObject imageSignUpIDBad;

    // ȸ������ ���̵� �˻� ���� �ؽ�Ʈ
    public Text txtSignUpIDBad;

    [Header(">PW")]
    // ȸ������ ��й�ȣ
    public InputField inputSignUpPW;

    // ȸ������ ��й�ȣ �˻� �ؽ�Ʈ
    //public Text txtSignUpPWCheck;

    // ȸ������ ��й�ȣ Ȯ��
    public InputField inputSignUpPWCheck;

    // ȸ������ ��й�ȣ �˻� ����
    public GameObject imageSignUpPWGood;

    // ȸ������ ��й�ȣ �˻� ����
    public GameObject imageSignUpPWBad;

    // ȸ������ ��й�ȣ Ȯ�� �ؽ�Ʈ
    public Text txtSignUpPWCheckBad;

    [Header(">")]
    // ȸ������ �̸�
    public InputField inputSignUpName;

    // ȸ������ �̸���
    public InputField inputSignUpEmail;

    // ȸ������ �ڵ�����ȣ
    public InputField inputSignUpPhone;

    // ȸ������ �ּ�
    public InputField inputSignUpAdress;

    // ȸ������ ��ư
    public Button btnSignUpSubmit;

    // ȸ������ ���ư��� ��ư
    public Button btnSignUpReturn;

    // ȸ�����Կ� ����
    string _memberId;
    string _memberPw;
    string _memberPwCheck;
    string _name;
    string _phone;
    string _email;
    string _address;

    #endregion

    [Header("EndSignUp")]
    // ȸ������ �Ϸ� �г�
    public GameObject panelEndSignUp;
    // ȸ������ �Ϸ� ��ư
    public Button btnEndSignUp;


    private void Awake()
    {
        webRequester = GetComponent<WebRequester_BH>();
    }

    void Start()
    {
        //panelFindID.SetActive(false);
        //panelFindPW.SetActive(false);
        panelSignUp.SetActive(false);

        #region ��ư listener

        // �α��� ��ư
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);

        // ���̵� ã�� ��ư
        //btnFindID.onClick.AddListener(OnbtnFindIDClicked);

        // ��й�ȣ ã�� ��ư
        //btnFindPW.onClick.AddListener(OnbtnFindPWClicked);

        // ȸ������ ��ư
        btnSignUp.onClick.AddListener(OnbtnSignUpClicked);

        // ���̵� ã�� ���� ��ư
        //btnFindIDSubmit.onClick.AddListener();

        // ���̵� ã�⿡�� ���ư���
        //btnFindIDReturn.onClick.AddListener(OnbtnFindIDReturnClicked);

        // ��й�ȣ ã�� ���� ��ư
        //btnFindPWSubmit.onClick.AddListener();

        // ��й�ȣ ã�⿡�� ���ư���
        //btnFindPWReturn.onClick.AddListener(OnbtnFindPWReturnClicked);

        // ȸ������ ���̵� �˻� ��ư
        //btnSignUpIDCheck.onClick.AddListener(OnbtnSignUpIDCheckClicked);

        // ȸ������ ���� ��ư
        btnSignUpSubmit.onClick.AddListener(OnbtnSignUpSubmitClicked);

        // ȸ������ ���� ���ư���
        btnSignUpReturn.onClick.AddListener(OnbtnSignUpReturnClicked);

        // ȸ������ �Ϸ� ���� �α������� ���ư���
        btnEndSignUp.onClick.AddListener(OnBtnSignUpEndClicked);
        #endregion


        #region InputField Listener

        // ����ȭ�� inputfield
        inputID.onEndEdit.AddListener(OnEndEditInputID);
        inputPW.onEndEdit.AddListener(OnEndEditInputPW);
        inputPW.onSubmit.AddListener(OnEndSubmitInputPW);

        // ȸ������ inputfield
        inputSignUpID.onEndEdit.AddListener(OnEndEditInputSignUpID);
        inputSignUpPW.onEndEdit.AddListener(OnEndEditInputSignUpPW);
        inputSignUpPWCheck.onEndEdit.AddListener(OnEndEditInputSignUpPWCorrect);
        inputSignUpName.onEndEdit.AddListener(OnEndEditInputSignUpName);
        inputSignUpPhone.onEndEdit.AddListener(OnEndEditInputSignUpPhone);
        inputSignUpEmail.onEndEdit.AddListener(OnEndEditInputSignUpEmail);
        inputSignUpAdress.onEndEdit.AddListener(OnEndEditInputSignUpAdress);

        #endregion
    }


    #region ��ư��

    // �α��� ��ư �������� ȣ��
    void OnbtnLogInClicked()
    {
        print("�α��� ��ư ����");

        LogIn();
    }

    // ���̵� ã�� �������� ȣ��
    //void OnbtnFindIDClicked()
    //{
    //    print("���̵� ã��");

    //    panelFindID.SetActive(true);
    //}

    // ���̵� ã�⿡�� ���ư��� ��ư �������� ȣ��
    //void OnbtnFindIDReturnClicked()
    //{
    //    print("���ư���");

    //    panelFindID.SetActive(false);
    //}

    // ��й�ȣ ã�� �������� ȣ��
    //void OnbtnFindPWClicked()
    //{
    //    print("��й�ȣ ã��");

    //    panelFindPW.SetActive(true);
    //}

    // ��й�ȣ ã�⿡�� ���ư��� ��ư �������� ȣ��
    //void OnbtnFindPWReturnClicked()
    //{
    //    print("���ư���");

    //    panelFindPW.SetActive(false);
    //}

    // ȸ������ �������� ȣ��
    void OnbtnSignUpClicked()
    {
        print("ȸ������");
        StartCoroutine(WindowClose(panelMain));
        StartCoroutine(WindowPopUp(panelSignUp));

    }

    // ȸ�����Կ��� ���ư��� ��ư �������� ȣ��
    void OnbtnSignUpReturnClicked()
    {
        print("���ư���");
        StartCoroutine(WindowClose(panelSignUp));
        StartCoroutine(WindowPopUp(panelMain));

    }

    void OnbtnSignUpSubmitClicked()
    {
        SignUp();
        //if ()
        //{
        StartCoroutine(WindowPopUp(panelEndSignUp));
        //}

    }

    void OnBtnSignUpEndClicked()
    {
        StartCoroutine(WindowClose(panelEndSignUp));
        StartCoroutine(WindowClose(panelSignUp));
        StartCoroutine(WindowPopUp(panelMain));
    }

    #endregion

    #region ��ǲ�ʵ�
    void OnEndEditInputID(string s)
    {
        _logInMemberId = s;
    }

    void OnEndEditInputPW(string s)
    {
        _logInMemberPw = s;
    }

    void OnEndSubmitInputPW(string s)
    {
        _logInMemberPw = s;
        LogIn();

    }

    void OnEndEditInputSignUpID(string s)
    {
        _memberId = s;
        IdCheck();
    }

    void OnEndEditInputSignUpPW(string s)
    {
        _memberPw = s;
    }

    void OnEndEditInputSignUpPWCorrect(string s)
    {
        _memberPwCheck = s;
    }

    void OnEndEditInputSignUpName(string s)
    {
        _name = s;
    }

    void OnEndEditInputSignUpPhone(string s)
    {
        _phone = s;
    }

    void OnEndEditInputSignUpEmail(string s)
    {
        _email = s;
    }

    void OnEndEditInputSignUpAdress(string s)
    {
        _address = s;
    }

    #endregion

    #region â�ִϸ��̼�
    IEnumerator WindowPopUp(GameObject window)
    {
        float scale = 0f;
        window.transform.localScale = Vector3.one * scale;
        window.SetActive(true);

        while (scale < 1f)
        {
            scale = Mathf.Lerp(scale, 1.05f, 10 * Time.deltaTime);
            window.transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }

    IEnumerator WindowClose(GameObject window)
    {
        float scale = 1f;
        window.transform.localScale = Vector3.one * scale;

        while (scale > 0f)
        {
            scale = Mathf.LerpUnclamped(scale, -0.05f, 10 * Time.deltaTime);
            window.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        window.SetActive(false);
    }

    #endregion


    #region WebRequest
    void LogIn()
    {
        // setting inspector ���� �ص� �� 
        PhotonNetwork.GameVersion = "1";

        //NameServer ����,(AppID, GameVersion, ����)
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.NickName = _name;
    }

    void SignUp()
    {
        HttpRequester requester = new HttpRequester();
        MemberData data = new MemberData();
        data.memberId = _memberId;
        data.memberPw = _memberPw;
        data.name = _name;
        data.phone = _phone;
        data.email = _email;
        data.address = _address;

        requester.url = "http://43.201.58.81:8088/members";
        requester.requestType = RequestType.POST;

        requester.postData = JsonUtility.ToJson(data, true);

        requester.onComplete = OnCompleteSignUp;

        webRequester.SendRequest(requester);
    }

    void IdCheck()
    {
        HttpRequester requester = new HttpRequester();
        string Id = _memberId;       

        requester.url = "http://43.201.58.81:8088/members/checkId/" + Id;
        requester.requestType = RequestType.GET;

        requester.postData = null;

        requester.onComplete = OnCompleteIdCheck;

        webRequester.SendRequest(requester);
    }

    #endregion

    #region WebDownloadHandler

    public void OnCompleteSignIn(DownloadHandler handler)
    {

    }

    public void OnCompleteSignUp(DownloadHandler handler)
    {

    }

    public void OnCompleteIdCheck(DownloadHandler handler)
    {

    }

    #endregion

    // ������ ������ ���� ����,  ���� �κ� ����ų� ������ �� ���� ����
    public override void OnConnected()
    {
        base.OnConnected();
        print("�����ͼ��� ���� ����");
    }

    // �����ͼ����� ����, �κ���� �� ������ ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("�����ͼ��� ���� �Ϸ�");

        //�г��� ����
        PhotonNetwork.NickName = _name;

        //�⺻ �κ� ���� 
        PhotonNetwork.JoinLobby();
    }

    // �κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����");
        PhotonNetwork.LoadLevel(1);
    }



}
