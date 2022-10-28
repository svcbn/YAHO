using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_BH : MonoBehaviourPun
{
    public GameObject cam;
    public GameObject speaker;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            cam.SetActive(true);
            //speaker.SetActive(true)
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
