using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomManager_BH : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPos;
    public Transform projectorPos;

    public Button btnCallUI;

    public GameObject btnMakeProject;
    public GameObject btnProjectInfo;
    public GameObject btnExitRoom;

    public GameObject panelMakeProject;
    public GameObject panelProjectInfo;

    public Button btnMakeProjectSubmit;
    public Button btnMakeProjectClose;
    public Button btnProjectInfoClose;

    // Start is called before the first frame update
    void Start()
    {
        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        PhotonNetwork.SendRate = 60;

        //플레이어를 생성한다.
        PhotonNetwork.Instantiate("Player", spawnPos[PhotonNetwork.CurrentRoom.PlayerCount-1].position, spawnPos[PhotonNetwork.CurrentRoom.PlayerCount-1].rotation);

        //PhotonNetwork.InstantiateRoomObject("Projector", projectorPos.position, projectorPos.rotation);

        #region Listner
        btnCallUI.onClick.AddListener(OnbtnCallUIClicked);
        btnMakeProject.GetComponent<Button>().onClick.AddListener(OnbtnMakeProjectClicked);
        btnProjectInfo.GetComponent<Button>().onClick.AddListener(OnbtnProjectInfoClicked);
        btnExitRoom.GetComponent<Button>().onClick.AddListener(OnbtnExitRoomClicked);
        btnMakeProjectSubmit.onClick.AddListener(OnbtnMakeProjectSubmitClicked);
        btnMakeProjectClose.onClick.AddListener(OnbtnMakeProjectCloseClicked);
        btnProjectInfoClose.onClick.AddListener(OnbtnProjectInfoCloseClicked);

        #endregion
    }

    void OnbtnCallUIClicked()
    {
        btnMakeProject.SetActive(true);
        btnProjectInfo.SetActive(true);
        btnExitRoom.SetActive(true);
    }

    void OnbtnMakeProjectClicked()
    {
        panelMakeProject.SetActive(true);
        btnMakeProject.SetActive(false);
        btnProjectInfo.SetActive(false);
        btnExitRoom.SetActive(false);

    }

    void OnbtnProjectInfoClicked()
    {
        panelProjectInfo.SetActive(true);
        btnMakeProject.SetActive(false);
        btnProjectInfo.SetActive(false);
        btnExitRoom.SetActive(false);

    }

    void OnbtnMakeProjectSubmitClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnMakeProjectCloseClicked()
    {
        panelMakeProject.SetActive(false);
    }

    void OnbtnProjectInfoCloseClicked()
    {
        panelProjectInfo.SetActive(false);
    }

    void OnbtnExitRoomClicked()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
