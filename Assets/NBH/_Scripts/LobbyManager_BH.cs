using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class LobbyManager_BH : MonoBehaviourPunCallbacks
{
    // �����ó�� ��� UI
    public Canvas monitor;

    // ��� �̹���
    public Image bgImage;

    // ȸ�ǽ� ���� ��ư
    public Button btnEnter;


    // Start is called before the first frame update
    void Start()
    {
        #region ��ư listener

        // ���� ��ư ��������
        btnEnter.onClick.AddListener(OnbtnEnterClicked);

        #endregion
    }

    void Update()
    {

    }

    void OnbtnEnterClicked()
    {
        CreateRoom();
    }

    //�����
    public void CreateRoom()
    {
        //�� ���� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ��ο�
        //where '0' means "no limit"
        roomOptions.MaxPlayers = 4; //byte.Parse(totalNum.text);

        //�� ��Ͽ� ���̳�? ������ �ʴ���?
        roomOptions.IsVisible = true;

        // Ŀ���� ������ ����
        //ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable(); ;
        //hash["desc"] = "���� �ʺ���" + Random.Range(1, 1000);
        //hash["map_id"] = Random.Range(0, mapThumbs.Length);
        //hash["room_name"] = roomName.text;
        //hash["password"] = InputPassword.text;
        //roomOptions.CustomRoomProperties = hash;

        //print((string)hash["desc"]+ ", " + (float)hash[1]);

        //custom ������ �����ϴ� ����
        //roomOptions.CustomRoomPropertiesForLobby = new string[] { "desc", "map_id", "room_name", "password" };

        // ���� �����.
        PhotonNetwork.CreateRoom("test", roomOptions, TypedLobby.Default);
    }

    //������
    //�� ���� �Ϸ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("�� ���� �Ϸ�");
    }

    // ���� �� ������ ������ ��� 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("�� ���� ����" + returnCode + message);

        JoinRoom();
    }

    // �� ����
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ����");
        PhotonNetwork.LoadLevel(2);

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("�� ���� ����" + ", " + returnCode + ", " + message);
    }
    
    
}
