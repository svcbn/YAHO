using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamHandler_BH : MonoBehaviour
{
    //public GameObject objectTarget;
    public RawImage display;

    WebCamTexture camTexture;

    WebCamDevice[] devices;

    // Start is called before the first frame update
    void Start()
    {
        devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
    }

    public void StartWebCam()
    {
        if (camTexture != null)
        {
            display.GetComponent<RawImage>().material.mainTexture = null;
            camTexture.Stop();
            camTexture = null;
        }
        camTexture = new WebCamTexture(devices[0].name);
        display.texture = camTexture;
        camTexture.Play();
    }

    public void StopWebCam()
    {
        if (camTexture != null)
        {
            display.GetComponent<RawImage>().material.mainTexture = null;
            camTexture.Stop();
            //camTexture = null;
        }
    }
}
