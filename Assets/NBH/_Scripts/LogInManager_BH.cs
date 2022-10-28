using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager_BH : MonoBehaviourPunCallbacks
{
    #region ����ȭ�� component

    // ���̵� InputField
    [HideInInspector]
    public InputField inputID;

    // ��й�ȣ IF
    [HideInInspector]
    public InputField inputPW;

    // �α��� ��ư
    [HideInInspector]
    public Button btnLogIn;

    // ���̵� ã�� ��ư
    [HideInInspector]
    public Button btnFindID;

    // ��й�ȣ ã�� ��ư
    [HideInInspector]
    public Button btnFindPW;

    // ȸ������ ��ư
    [HideInInspector]
    public Button btnSignUp;

    #endregion

    // ���̵� ã�� �г�
    [HideInInspector]
    public GameObject panelFindID;
    #region ���̵� ã�� component

    // ���̵� ã�� �̸�
    [HideInInspector]
    public InputField inputFindIDName;

    // ���̵� ã�� �̸��� 
    [HideInInspector]
    public InputField inputFindIDEmail;

    // ���̵� ã�� ��ư
    [HideInInspector]
    public Button btnFindIDSubmit;
    
    // ���̵� ã�� ���ư���
    [HideInInspector]
    public Button btnFindIDReturn;

    #endregion


    // ��й�ȣ ã�� �г�
    [HideInInspector]
    public GameObject panelFindPW;
    #region ��й�ȣ ã�� component

    // ��й�ȣ ã�� ���̵�
    [HideInInspector]
    public InputField inputFindPWID;

    // ��й�ȣ ã�� �̸�
    [HideInInspector]
    public InputField inputFindPWName;

    // ��й�ȣ ã�� �̸���
    [HideInInspector]
    public InputField inputFindPWEmail;

    // ��й�ȣ ã�� ��ư
    [HideInInspector]
    public Button btnFindPWSubmit;

    // ��й�ȣ ã�� ���ư���
    [HideInInspector]
    public Button btnFindPWReturn;

    #endregion


    [HideInInspector]
    // ȸ������ �г�
    public GameObject panelSignUp;
    #region ȸ������ component

    // ȸ������ �̸�
    [HideInInspector]
    public InputField inputSignUpName;

    // ȸ������ ���̵�
    [HideInInspector]
    public InputField inputSignUpID;

    // ȸ������ ���̵� �˻� ��ư
    [HideInInspector]
    public Button btnSignUpIDCheck;

    // ȸ������ ���̵� �˻� �ؽ�Ʈ
    [HideInInspector]
    public Text txtSignUpID;

    // ȸ������ ��й�ȣ
    [HideInInspector]
    public InputField inputSignUpPW;

    // ȸ������ ��й�ȣ �˻� �ؽ�Ʈ
    [HideInInspector]
    public Text txtSignUpPWCheck;

    // ȸ������ ��й�ȣ Ȯ��
    [HideInInspector]
    public InputField inputSignUpPWCorrect;

    // ȸ������ ��й�ȣ Ȯ�� �ؽ�Ʈ
    [HideInInspector]
    public Text txtSignUpPWCorrect;

    // ȸ������ �̸���
    [HideInInspector]
    public InputField inputSignUpEmail;

    // ȸ������ ��ư
    [HideInInspector]
    public Button btnSignUpSubmit;

    // ȸ������ ���ư��� ��ư
    [HideInInspector]
    public Button btnSignUpReturn;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        panelFindID.SetActive(false);
        panelFindPW.SetActive(false);
        panelSignUp.SetActive(false);

        #region ��ư listener

        // �α��� ��ư
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);

        // ���̵� ã�� ��ư
        btnFindID.onClick.AddListener(OnbtnFindIDClicked);

        // ��й�ȣ ã�� ��ư
        btnFindPW.onClick.AddListener(OnbtnFindPWClicked);

        // ȸ������ ��ư
        btnSignUp.onClick.AddListener(OnbtnSignUpClicked);

        // ���̵� ã�� ���� ��ư
        //btnFindIDSubmit.onClick.AddListener();

        // ���̵� ã�⿡�� ���ư���
        btnFindIDReturn.onClick.AddListener(OnbtnFindIDReturnClicked);

        // ��й�ȣ ã�� ���� ��ư
        //btnFindPWSubmit.onClick.AddListener();

        // ��й�ȣ ã�⿡�� ���ư���
        btnFindPWReturn.onClick.AddListener(OnbtnFindPWReturnClicked);

        // ȸ������ ���̵� �˻� ��ư
        //btnSignUpIDCheck.onClick.AddListener();

        // ȸ������ ���� ��ư
        //btnSignUpSubmit.onClick.AddListener();

        // ȸ������ ���� ���ư���
        btnSignUpReturn.onClick.AddListener(OnbtnSignUpReturnClicked);



        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ��ư��

    // �α��� ��ư �������� ȣ��
    void OnbtnLogInClicked()
    {
        print("�α��� ��ư ����");

        // setting inspector ���� �ص� �� 
        PhotonNetwork.GameVersion = "1";

        //NameServer ����,(AppID, GameVersion, ����)
        PhotonNetwork.ConnectUsingSettings();
    }

    // ���̵� ã�� �������� ȣ��
    void OnbtnFindIDClicked()
    {
        print("���̵� ã��");

        panelFindID.SetActive(true);
    }

    // ���̵� ã�⿡�� ���ư��� ��ư �������� ȣ��
    void OnbtnFindIDReturnClicked()
    {
        print("���ư���");

        panelFindID.SetActive(false);
    }

    // ��й�ȣ ã�� �������� ȣ��
    void OnbtnFindPWClicked()
    {
        print("��й�ȣ ã��");

        panelFindPW.SetActive(true);
    }

    // ��й�ȣ ã�⿡�� ���ư��� ��ư �������� ȣ��
    void OnbtnFindPWReturnClicked()
    {
        print("���ư���");

        panelFindPW.SetActive(false);
    }

    // ȸ������ �������� ȣ��
    void OnbtnSignUpClicked()
    {
        print("ȸ������");

        panelSignUp.SetActive(true);
    }

    // ȸ�����Կ��� ���ư��� ��ư �������� ȣ��
    void OnbtnSignUpReturnClicked()
    {
        print("���ư���");

        panelSignUp.SetActive(false);
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
        PhotonNetwork.LoadLevel("02_MyDesk_BH");
    }
}
