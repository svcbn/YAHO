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
    // 메인패널
    public GameObject panelMain;
    #region 메인화면 component

    // 아이디 InputField
    public InputField inputID;

    // 비밀번호 IF
    public InputField inputPW;

    // 로그인 버튼
    public Button btnLogIn;

    // 아이디 찾기 버튼
    //public Button btnFindID;

    // 비밀번호 찾기 버튼
    //public Button btnFindPW;

    // 회원가입 버튼
    public Button btnSignUp;

    // 로그인용 정보
    string _logInMemberId;
    string _logInMemberPw;

    #endregion

    //[Header("FindID")]
    // 아이디 찾기 패널
    //public GameObject panelFindID;
    #region 아이디 찾기 component

    // 아이디 찾기 이름
    //public InputField inputFindIDName;

    // 아이디 찾기 이메일
    //public InputField inputFindIDEmail;

    // 아이디 찾기 버튼
    //public Button btnFindIDSubmit;

    // 아이디 찾기 돌아가기
    //public Button btnFindIDReturn;

    #endregion

    //[Header("FindPW")]
    // 비밀번호 찾기 패널
    //public GameObject panelFindPW;
    #region 비밀번호 찾기 component

    // 비밀번호 찾기 아이디
    //public InputField inputFindPWID;

    // 비밀번호 찾기 이름
    //public InputField inputFindPWName;

    // 비밀번호 찾기 이메일
    //public InputField inputFindPWEmail;

    // 비밀번호 찾기 버튼
    //public Button btnFindPWSubmit;

    // 비밀번호 찾기 돌아가기
    //public Button btnFindPWReturn;

    #endregion

    [Header("SignUp")]
    // 회원가입 패널
    public GameObject panelSignUp;
    #region 회원가입 component

    [Header(">ID")]
    // 회원가입 아이디
    public InputField inputSignUpID;

    // 회원가입 아이디 검사 버튼
    //public Button btnSignUpIDCheck;

    // 회원가입 아이디 검사 성공
    public GameObject imageSignUpIDGood;

    // 회원가입 아이디 검사 실패
    public GameObject imageSignUpIDBad;

    // 회원가입 아이디 검사 실패 텍스트
    public Text txtSignUpIDBad;

    [Header(">PW")]
    // 회원가입 비밀번호
    public InputField inputSignUpPW;

    // 회원가입 비밀번호 검사 텍스트
    //public Text txtSignUpPWCheck;

    // 회원가입 비밀번호 확인
    public InputField inputSignUpPWCheck;

    // 회원가입 비밀번호 검사 성공
    public GameObject imageSignUpPWGood;

    // 회원가입 비밀번호 검사 실패
    public GameObject imageSignUpPWBad;

    // 회원가입 비밀번호 확인 텍스트
    public Text txtSignUpPWCheckBad;

    [Header(">")]
    // 회원가입 이름
    public InputField inputSignUpName;

    // 회원가입 이메일
    public InputField inputSignUpEmail;

    // 회원가입 핸드폰번호
    public InputField inputSignUpPhone;

    // 회원가입 주소
    public InputField inputSignUpAdress;

    // 회원가입 버튼
    public Button btnSignUpSubmit;

    // 회원가입 돌아가기 버튼
    public Button btnSignUpReturn;

    // 회원가입용 정보
    string _memberId;
    string _memberPw;
    string _memberPwCheck;
    string _name;
    string _phone;
    string _email;
    string _address;

    #endregion

    [Header("EndSignUp")]
    // 회원가입 완료 패널
    public GameObject panelEndSignUp;
    // 회원가입 완료 버튼
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

        #region 버튼 listener

        // 로그인 버튼
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);

        // 아이디 찾기 버튼
        //btnFindID.onClick.AddListener(OnbtnFindIDClicked);

        // 비밀번호 찾기 버튼
        //btnFindPW.onClick.AddListener(OnbtnFindPWClicked);

        // 회원가입 버튼
        btnSignUp.onClick.AddListener(OnbtnSignUpClicked);

        // 아이디 찾기 제출 버튼
        //btnFindIDSubmit.onClick.AddListener();

        // 아이디 찾기에서 돌아가기
        //btnFindIDReturn.onClick.AddListener(OnbtnFindIDReturnClicked);

        // 비밀번호 찾기 제출 버튼
        //btnFindPWSubmit.onClick.AddListener();

        // 비밀번호 찾기에서 돌아가기
        //btnFindPWReturn.onClick.AddListener(OnbtnFindPWReturnClicked);

        // 회원가입 아이디 검사 버튼
        //btnSignUpIDCheck.onClick.AddListener(OnbtnSignUpIDCheckClicked);

        // 회원가입 제출 버튼
        btnSignUpSubmit.onClick.AddListener(OnbtnSignUpSubmitClicked);

        // 회원가입 에서 돌아가기
        btnSignUpReturn.onClick.AddListener(OnbtnSignUpReturnClicked);

        // 회원가입 완료 에서 로그인으로 돌아가기
        btnEndSignUp.onClick.AddListener(OnBtnSignUpEndClicked);
        #endregion


        #region InputField Listener

        // 메인화면 inputfield
        inputID.onEndEdit.AddListener(OnEndEditInputID);
        inputPW.onEndEdit.AddListener(OnEndEditInputPW);
        inputPW.onSubmit.AddListener(OnEndSubmitInputPW);

        // 회원가입 inputfield
        inputSignUpID.onEndEdit.AddListener(OnEndEditInputSignUpID);
        inputSignUpPW.onEndEdit.AddListener(OnEndEditInputSignUpPW);
        inputSignUpPWCheck.onEndEdit.AddListener(OnEndEditInputSignUpPWCorrect);
        inputSignUpName.onEndEdit.AddListener(OnEndEditInputSignUpName);
        inputSignUpPhone.onEndEdit.AddListener(OnEndEditInputSignUpPhone);
        inputSignUpEmail.onEndEdit.AddListener(OnEndEditInputSignUpEmail);
        inputSignUpAdress.onEndEdit.AddListener(OnEndEditInputSignUpAdress);

        #endregion
    }


    #region 버튼들

    // 로그인 버튼 눌렀을때 호출
    void OnbtnLogInClicked()
    {
        print("로그인 버튼 누름");

        LogIn();
    }

    // 아이디 찾기 눌렀을때 호출
    //void OnbtnFindIDClicked()
    //{
    //    print("아이디 찾기");

    //    panelFindID.SetActive(true);
    //}

    // 아이디 찾기에서 돌아가기 버튼 눌렀을때 호출
    //void OnbtnFindIDReturnClicked()
    //{
    //    print("돌아가기");

    //    panelFindID.SetActive(false);
    //}

    // 비밀번호 찾기 눌렀을때 호출
    //void OnbtnFindPWClicked()
    //{
    //    print("비밀번호 찾기");

    //    panelFindPW.SetActive(true);
    //}

    // 비밀번호 찾기에서 돌아가기 버튼 눌렀을때 호출
    //void OnbtnFindPWReturnClicked()
    //{
    //    print("돌아가기");

    //    panelFindPW.SetActive(false);
    //}

    // 회원가입 눌렀을때 호출
    void OnbtnSignUpClicked()
    {
        print("회원가입");
        StartCoroutine(WindowClose(panelMain));
        StartCoroutine(WindowPopUp(panelSignUp));

    }

    // 회원가입에서 돌아가기 버튼 눌렀을때 호출
    void OnbtnSignUpReturnClicked()
    {
        print("돌아가기");
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

    #region 인풋필드
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

    #region 창애니메이션
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
        // setting inspector 에서 해도 됨 
        PhotonNetwork.GameVersion = "1";

        //NameServer 접속,(AppID, GameVersion, 지역)
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

    // 마스터 서버에 접속 성공,  아직 로비를 만들거나 진입할 수 없는 상태
    public override void OnConnected()
    {
        base.OnConnected();
        print("마스터서버 접속 성공");
    }

    // 마스터서버에 접속, 로비생성 및 진입이 가능
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("마스터서버 접속 완료");

        //닉네임 설정
        PhotonNetwork.NickName = _name;

        //기본 로비 진입 
        PhotonNetwork.JoinLobby();
    }

    // 로비 접속 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("로비 접속 성공");
        PhotonNetwork.LoadLevel(1);
    }



}
