using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class LobbyManager_BH : MonoBehaviourPunCallbacks
{
    // 모니터처럼 띄울 UI
    public Canvas monitor;

    // 배경 이미지
    public Image bgImage;

    // 회의실 입장 버튼
    public Button btnEnter;


    // Start is called before the first frame update
    void Start()
    {
        #region 버튼 listener

        // 입장 버튼 눌렀을때
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

    //방생성
    public void CreateRoom()
    {
        //방 정보 셋팅
        RoomOptions roomOptions = new RoomOptions();

        //최대인원
        //where '0' means "no limit"
        roomOptions.MaxPlayers = 4; //byte.Parse(totalNum.text);

        //룸 목록에 모이나? 보이지 않느냐?
        roomOptions.IsVisible = true;

        // 커스텀 정보를 셋팅
        //ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable(); ;
        //hash["desc"] = "여긴 초보방" + Random.Range(1, 1000);
        //hash["map_id"] = Random.Range(0, mapThumbs.Length);
        //hash["room_name"] = roomName.text;
        //hash["password"] = InputPassword.text;
        //roomOptions.CustomRoomProperties = hash;

        //print((string)hash["desc"]+ ", " + (float)hash[1]);

        //custom 정보를 공개하는 설정
        //roomOptions.CustomRoomPropertiesForLobby = new string[] { "desc", "map_id", "room_name", "password" };

        // 방을 만든다.
        PhotonNetwork.CreateRoom("test", roomOptions, TypedLobby.Default);
    }

    //방입장
    //방 생성 완료
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 만약 방 생성이 실패할 경우 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("방 생성 실패" + returnCode + message);

        JoinRoom();
    }

    // 방 참가
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 참가");
        PhotonNetwork.LoadLevel(2);

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("방 참가 실패" + ", " + returnCode + ", " + message);
    }
    
    
}
