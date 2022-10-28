using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ScreenShareTest_BH : MonoBehaviourPun, IPunObservable
{
    MeshRenderer copiedMesh;
    Texture originMesh;

    MeshRenderer sharedMesh;

    byte[] sendTexture;
    byte[] recieveTexture;
    //Texture recieveTexture;

    // Start is called before the first frame update
    void Start()
    {
        copiedMesh = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    Texture2D sendTexture2D;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ScreenShare();
        }
        else
        {
            ByteToTexture2D(recieveTexture);
        }


        //copiedMesh.material = sharedMesh.material;
        //copiedMesh.material.mainTexture = originMesh;
    }


    
    Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        Color[] pixels = texture2D.GetPixels();
        RenderTexture.active = currentRT;

        return texture2D;

    }

    byte[] Texture2DToByte(Texture2D texture2D)
    {
        byte[] bArray = texture2D.GetRawTextureData();
        return bArray;
    }

    void ByteToTexture2D(byte[] bArray)
    {
        string texAsString = System.Convert.ToBase64String(bArray);
        Texture2D tex = new Texture2D(originMesh.width, originMesh.height, TextureFormat.RGBA32, false);

        tex.LoadRawTextureData(bArray);
        tex.Apply();
        copiedMesh.material.mainTexture = tex;
    }

    void ScreenShare()
    {
        originMesh = GameObject.Find("Desktop").GetComponent<MeshRenderer>().material.mainTexture;
        sendTexture2D = TextureToTexture2D(originMesh);
        sendTexture = Texture2DToByte(sendTexture2D);
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(sendTexture);
        }
        else if (stream.IsReading)
        {
            recieveTexture = (byte[])stream.ReceiveNext();
        }
    }

}
