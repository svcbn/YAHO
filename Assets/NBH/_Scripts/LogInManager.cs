using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviourPunCallbacks
{
    #region 메인화면 component

    // 아이디 InputField
    public InputField inputID;

    // 비밀번호 IF
    public InputField inputPW;

    // 로그인 버튼
    public Button btnLogIn;

    // 비밀번호 찾기 버튼
    public Button btnFindPW;

    // 아이디 찾기 버튼
    public Button btnFindID;

    // 회원가입 버튼
    public Button btnSignUp;

    // 회원가입 패널
    public GameObject panelSignUp;
    #endregion

    


    // Start is called before the first frame update
    void Start()
    {
        // 로그인 버튼 listener
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 로그인 버튼 눌렀을때 호출
    void OnbtnLogInClicked()
    {
        print("로그인 버튼 누름");

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
        PhotonNetwork.LoadLevel("02_MyDesk_Lobby_BH");
    }
}
