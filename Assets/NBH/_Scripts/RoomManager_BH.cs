using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager_BH : MonoBehaviourPunCallbacks
{
    public Transform testPos;
    public Transform projectorPos;

    // Start is called before the first frame update
    void Start()
    {
        //OnPhotonSerializeView ȣ�� ��
        PhotonNetwork.SerializationRate = 60;
        PhotonNetwork.SendRate = 60;

        //�÷��̾ �����Ѵ�.
        PhotonNetwork.Instantiate("Player", testPos.position, testPos.rotation);

        PhotonNetwork.InstantiateRoomObject("Projector", projectorPos.position, projectorPos.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
