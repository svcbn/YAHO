using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviourPunCallbacks
{
    #region ����ȭ�� component

    // ���̵� InputField
    public InputField inputID;

    // ��й�ȣ IF
    public InputField inputPW;

    // �α��� ��ư
    public Button btnLogIn;

    // ��й�ȣ ã�� ��ư
    public Button btnFindPW;

    // ���̵� ã�� ��ư
    public Button btnFindID;

    // ȸ������ ��ư
    public Button btnSignUp;

    // ȸ������ �г�
    public GameObject panelSignUp;
    #endregion

    


    // Start is called before the first frame update
    void Start()
    {
        // �α��� ��ư listener
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �α��� ��ư �������� ȣ��
    void OnbtnLogInClicked()
    {
        print("�α��� ��ư ����");

        // setting inspector ���� �ص� �� 
        PhotonNetwork.GameVersion = "1";

        //NameServer ����,(AppID, GameVersion, ����)
        PhotonNetwork.ConnectUsingSettings();
    }

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
        //PhotonNetwork.NickName = inputNickName.text;
        //�⺻ �κ� ���� 
        PhotonNetwork.JoinLobby();
        //Ư�� �κ� ����
        //PhotonNetwork.JoinLobby(new TypedLobby("�ǿ��� �κ�",LobbyType.Default));
    }

    // �κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����");
        PhotonNetwork.LoadLevel("02_MyDesk_Lobby_BH");
    }
}
