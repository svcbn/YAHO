using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager_BH : MonoBehaviourPunCallbacks
{
    #region 메인화면 component

    // 아이디 InputField
    [HideInInspector]
    public InputField inputID;

    // 비밀번호 IF
    [HideInInspector]
    public InputField inputPW;

    // 로그인 버튼
    [HideInInspector]
    public Button btnLogIn;

    // 아이디 찾기 버튼
    [HideInInspector]
    public Button btnFindID;

    // 비밀번호 찾기 버튼
    [HideInInspector]
    public Button btnFindPW;

    // 회원가입 버튼
    [HideInInspector]
    public Button btnSignUp;

    #endregion

    // 아이디 찾기 패널
    [HideInInspector]
    public GameObject panelFindID;
    #region 아이디 찾기 component

    // 아이디 찾기 이름
    [HideInInspector]
    public InputField inputFindIDName;

    // 아이디 찾기 이메일 
    [HideInInspector]
    public InputField inputFindIDEmail;

    // 아이디 찾기 버튼
    [HideInInspector]
    public Button btnFindIDSubmit;
    
    // 아이디 찾기 돌아가기
    [HideInInspector]
    public Button btnFindIDReturn;

    #endregion


    // 비밀번호 찾기 패널
    [HideInInspector]
    public GameObject panelFindPW;
    #region 비밀번호 찾기 component

    // 비밀번호 찾기 아이디
    [HideInInspector]
    public InputField inputFindPWID;

    // 비밀번호 찾기 이름
    [HideInInspector]
    public InputField inputFindPWName;

    // 비밀번호 찾기 이메일
    [HideInInspector]
    public InputField inputFindPWEmail;

    // 비밀번호 찾기 버튼
    [HideInInspector]
    public Button btnFindPWSubmit;

    // 비밀번호 찾기 돌아가기
    [HideInInspector]
    public Button btnFindPWReturn;

    #endregion


    [HideInInspector]
    // 회원가입 패널
    public GameObject panelSignUp;
    #region 회원가입 component

    // 회원가입 이름
    [HideInInspector]
    public InputField inputSignUpName;

    // 회원가입 아이디
    [HideInInspector]
    public InputField inputSignUpID;

    // 회원가입 아이디 검사 버튼
    [HideInInspector]
    public Button btnSignUpIDCheck;

    // 회원가입 아이디 검사 텍스트
    [HideInInspector]
    public Text txtSignUpID;

    // 회원가입 비밀번호
    [HideInInspector]
    public InputField inputSignUpPW;

    // 회원가입 비밀번호 검사 텍스트
    [HideInInspector]
    public Text txtSignUpPWCheck;

    // 회원가입 비밀번호 확인
    [HideInInspector]
    public InputField inputSignUpPWCorrect;

    // 회원가입 비밀번호 확인 텍스트
    [HideInInspector]
    public Text txtSignUpPWCorrect;

    // 회원가입 이메일
    [HideInInspector]
    public InputField inputSignUpEmail;

    // 회원가입 버튼
    [HideInInspector]
    public Button btnSignUpSubmit;

    // 회원가입 돌아가기 버튼
    [HideInInspector]
    public Button btnSignUpReturn;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        panelFindID.SetActive(false);
        panelFindPW.SetActive(false);
        panelSignUp.SetActive(false);

        #region 버튼 listener

        // 로그인 버튼
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);

        // 아이디 찾기 버튼
        btnFindID.onClick.AddListener(OnbtnFindIDClicked);

        // 비밀번호 찾기 버튼
        btnFindPW.onClick.AddListener(OnbtnFindPWClicked);

        // 회원가입 버튼
        btnSignUp.onClick.AddListener(OnbtnSignUpClicked);

        // 아이디 찾기 제출 버튼
        //btnFindIDSubmit.onClick.AddListener();

        // 아이디 찾기에서 돌아가기
        btnFindIDReturn.onClick.AddListener(OnbtnFindIDReturnClicked);

        // 비밀번호 찾기 제출 버튼
        //btnFindPWSubmit.onClick.AddListener();

        // 비밀번호 찾기에서 돌아가기
        btnFindPWReturn.onClick.AddListener(OnbtnFindPWReturnClicked);

        // 회원가입 아이디 검사 버튼
        //btnSignUpIDCheck.onClick.AddListener();

        // 회원가입 제출 버튼
        //btnSignUpSubmit.onClick.AddListener();

        // 회원가입 에서 돌아가기
        btnSignUpReturn.onClick.AddListener(OnbtnSignUpReturnClicked);



        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 버튼들

    // 로그인 버튼 눌렀을때 호출
    void OnbtnLogInClicked()
    {
        print("로그인 버튼 누름");

        // setting inspector 에서 해도 됨 
        PhotonNetwork.GameVersion = "1";

        //NameServer 접속,(AppID, GameVersion, 지역)
        PhotonNetwork.ConnectUsingSettings();
    }

    // 아이디 찾기 눌렀을때 호출
    void OnbtnFindIDClicked()
    {
        print("아이디 찾기");

        panelFindID.SetActive(true);
    }

    // 아이디 찾기에서 돌아가기 버튼 눌렀을때 호출
    void OnbtnFindIDReturnClicked()
    {
        print("돌아가기");

        panelFindID.SetActive(false);
    }

    // 비밀번호 찾기 눌렀을때 호출
    void OnbtnFindPWClicked()
    {
        print("비밀번호 찾기");

        panelFindPW.SetActive(true);
    }

    // 비밀번호 찾기에서 돌아가기 버튼 눌렀을때 호출
    void OnbtnFindPWReturnClicked()
    {
        print("돌아가기");

        panelFindPW.SetActive(false);
    }

    // 회원가입 눌렀을때 호출
    void OnbtnSignUpClicked()
    {
        print("회원가입");

        panelSignUp.SetActive(true);
    }

    // 회원가입에서 돌아가기 버튼 눌렀을때 호출
    void OnbtnSignUpReturnClicked()
    {
        print("돌아가기");

        panelSignUp.SetActive(false);
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
        //PhotonNetwork.NickName = inputNickName.text;
        //기본 로비 진입 
        PhotonNetwork.JoinLobby();
        //특정 로비 진입
        //PhotonNetwork.JoinLobby(new TypedLobby("권영찬 로비",LobbyType.Default));
    }

    // 로비 접속 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("로비 접속 성공");
        PhotonNetwork.LoadLevel("02_MyDesk_BH");
    }
}
