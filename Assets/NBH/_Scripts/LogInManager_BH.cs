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

[System.Serializable]
public class MemberData
{
    public string memberId;
    public string memberPw;
    public string name;
    public string phone;
    public string email;
    public string address;
}

public class GetJsonData
{
    public int status;
    public string message;
    public string data;
}

public class GetUserData
{
    public MemberData data;
}

public class LogInManager_BH : MonoBehaviourPunCallbacks
{
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

    // 로딩애니메이션
    public GameObject imgLoading;

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
    public GameObject txtSignUpIDBad;

    [Header(">PW")]
    // 회원가입 비밀번호
    public InputField inputSignUpPW;

    // 회원가입 비밀번호 성공
    public GameObject imageSignUpPWGood;

    // 회원가입 비밀번호 실패
    public GameObject imageSignUpPWBad;

    // 회원가입 비밀번호 실패 텍스트
    public GameObject txtSignUpPWBad;

    // 회원가입 비밀번호 확인
    public InputField inputSignUpPWCheck;

    // 회원가입 비밀번호 검사 성공
    public GameObject imageSignUpPWCheckGood;

    // 회원가입 비밀번호 검사 실패
    public GameObject imageSignUpPWCheckBad;

    // 회원가입 비밀번호 확인 텍스트
    public GameObject txtSignUpPWCheckBad;

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
    public GameObject panelNotice;
    // 회원가입 완료 버튼
    public Button btnEndSignUp;

    int _memberNo;

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
        btnEndSignUp.onClick.AddListener(OnBtnNoticeSubmitClicked);
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
        if (_memberId != null && signUpIdCheck && _memberPw != null && signUpPwCheck && _name != null && _email != null && _phone != null && _address != null)
        {
            SignUp();
        }
        else
        {
            panelNotice.GetComponentInChildren<Text>().text = "항목을 다시 확인해주세요!";
            StartCoroutine(WindowPopUp(panelNotice));
        }
    }

    bool signUpSuccess = false;
    void OnBtnNoticeSubmitClicked()
    {
        StartCoroutine(WindowClose(panelNotice));
        if (signUpSuccess)
        {
            StartCoroutine(WindowClose(panelSignUp));
            StartCoroutine(WindowPopUp(panelMain));
            signUpSuccess = false;
        }
    }

    #endregion

    #region 인풋필드
    void OnEndEditInputID(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberId = s;
        }
    }

    void OnEndEditInputPW(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberPw = s;
        }
    }

    void OnEndSubmitInputPW(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberPw = s;
            LogIn();
        }
    }

    void OnEndEditInputSignUpID(string s)
    {
        if (s.Length > 0)
        {
            _memberId = s;
            IdCheck();
        }
    }

    void OnEndEditInputSignUpPW(string s)
    {
        imageSignUpPWGood.SetActive(false);
        imageSignUpPWBad.SetActive(false);
        txtSignUpPWBad.SetActive(false);
        if (s.Length > 0)
        {
            if (s.Length > 9 && s.Length < 17)
            {
                imageSignUpPWGood.SetActive(true);
                _memberPw = s;
            }
            else
            {
                imageSignUpPWBad.SetActive(true);
                txtSignUpPWBad.SetActive(true);
            }
        }
    }

    bool signUpPwCheck = false;
    void OnEndEditInputSignUpPWCorrect(string s)
    {
        imageSignUpPWCheckGood.SetActive(false);
        imageSignUpPWCheckBad.SetActive(false);
        txtSignUpPWCheckBad.SetActive(false);
        if (s.Length > 0)
        {
            if (s == _memberPw)
            {
                imageSignUpPWCheckGood.SetActive(true);
                _memberPwCheck = s;
                signUpPwCheck = true;

            }
            else
            {
                imageSignUpPWCheckBad.SetActive(true);
                txtSignUpPWCheckBad.SetActive(true);
                signUpPwCheck = false;
            }
        }
    }

    void OnEndEditInputSignUpName(string s)
    {
        if (s.Length > 0)
        {
            _name = s;
        }
    }

    void OnEndEditInputSignUpPhone(string s)
    {
        if (s.Length > 0)
        {
            _phone = s;
        }
    }

    void OnEndEditInputSignUpEmail(string s)
    {
        if (s.Length > 0)
        {
            _email = s;
        }
    }

    void OnEndEditInputSignUpAdress(string s)
    {
        if (s.Length > 0)
        {
            _address = s;
        }
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
        HttpRequester requester = new HttpRequester();
        SignInData data = new SignInData();
        data.memberId = _logInMemberId;
        data.memberPw = _logInMemberPw;

        requester.url = "http://43.201.58.81:8088/members/login";
        requester.requestType = RequestType.POST;
        requester.postData = JsonUtility.ToJson(data);
        requester.onComplete = OnCompleteSignIn;
        requester.onFailed = OnFailedSignIn;

        WebRequester_BH.instance.SendRequest(requester);
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
        requester.postData = JsonUtility.ToJson(data);
        requester.onComplete = OnCompleteSignUp;

        WebRequester_BH.instance.SendRequest(requester);
    }

    bool signUpIdCheck = false;
    void IdCheck()
    {
        HttpRequester requester = new HttpRequester();
        string Id = _memberId;       

        requester.url = "http://43.201.58.81:8088/members/checkId/" + Id;
        requester.requestType = RequestType.GET;

        requester.onComplete = OnCompleteIdCheck;

        WebRequester_BH.instance.SendRequest(requester);
    }

    void Identify()
    {
        HttpRequester requester = new HttpRequester();
        string Id = _logInMemberId;

        requester.url = "http://43.201.58.81:8088/members/auth/" + Id;
        requester.requestType = RequestType.GET;

        requester.onComplete = OnCompleteIdentify;

        WebRequester_BH.instance.SendRequest(requester);
    }

    #endregion

    #region WebDownloadHandler

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);
        
        if(jsonData.status == 200)
        {
            Identify();
            imgLoading.SetActive(true);
        }
        else
        {
            Debug.Log("로그인 실패");
            
        }
    }

    public void OnFailedSignIn()
    {
        Debug.Log("로그인 실패");
        panelNotice.GetComponentInChildren<Text>().text = "ID와 PW를 확인해주세요!";
        StartCoroutine(WindowPopUp(panelNotice));
    }

    public void OnCompleteSignUp(DownloadHandler handler)
    {

        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);
        if(jsonData.status == 400)
        {
            Debug.Log("회원가입 실패");
            panelNotice.GetComponentInChildren<Text>().text = jsonData.message;
            StartCoroutine(WindowPopUp(panelNotice));
        }
        else if(jsonData.status == 200 && signUpIdCheck && signUpPwCheck)
        {
            Debug.Log("회원가입 성공");
            panelNotice.GetComponentInChildren<Text>().text = "회원가입 성공!";
            StartCoroutine(WindowPopUp(panelNotice));
            signUpSuccess = true;
        }
        else
        {
            Debug.Log("회원가입 실패");
            panelNotice.GetComponentInChildren<Text>().text = "";
            StartCoroutine(WindowPopUp(panelNotice));
        }

    }

    public void OnCompleteIdCheck(DownloadHandler handler)
    {
        imageSignUpIDGood.SetActive(false);
        imageSignUpIDBad.SetActive(false);
        txtSignUpIDBad.SetActive(false);

        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);
        if(jsonData.data == "true")
        {
            imageSignUpIDBad.SetActive(true);
            txtSignUpIDBad.SetActive(true);
            signUpIdCheck = false;

        }
        else
        {
            imageSignUpIDGood.SetActive(true);
            signUpIdCheck = true;
        }
    }

    public void OnCompleteIdentify(DownloadHandler handler)
    {
        GetUserData userData = new GetUserData();
        
        userData = JsonUtility.FromJson<GetUserData>(handler.text);

        Debug.Log(userData);
        PhotonNetwork.NickName = userData.data.name;

        UserInformation_BH.instance.MemberId = userData.data.memberId;
        UserInformation_BH.instance.Name = userData.data.name;
        
        ConnectMasterServer();
    }

    #endregion

    public void ConnectMasterServer()
    {
        // setting inspector 에서 해도 됨 
        PhotonNetwork.GameVersion = "1";

        //NameServer 접속,(AppID, GameVersion, 지역)
        PhotonNetwork.ConnectUsingSettings();
    }

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
