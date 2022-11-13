using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using uWindowCapture;
using Unity.WebRTC;

public class Projector_BH : MonoBehaviour
{
    public WebRTCBroadClient_BH webRTCBroadClient;
    public UwcWindowTexture windowTexture;
    public MeshRenderer meshRenderer;
    public RenderTexture outputTexture;
    public PhotonView photonView;

    public void Awake()
    {
        windowTexture = GetComponentInChildren<UwcWindowTexture>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        webRTCBroadClient = GetComponentInChildren<WebRTCBroadClient_BH>();
        outputTexture = new RenderTexture(1920, 1080, 24);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Execute();
        }
    }

    public void Execute()
    {
        Debug.Log("Open Link");
        windowTexture.enabled = true;
        StartCoroutine(ExecuteCoroutine());
    }

    IEnumerator ExecuteCoroutine()
    {
        yield return new WaitUntil(() => meshRenderer.material.mainTexture != null);

        if(meshRenderer.material.mainTexture == null)
        {
            webRTCBroadClient.VideoStreamTrack = new VideoStreamTrack(new Texture2D(1920, 1080));
        }
        else
        {
            webRTCBroadClient.VideoStreamTrack = new VideoStreamTrack(meshRenderer.material.mainTexture);
        }

        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

        yield break;
    }
}
