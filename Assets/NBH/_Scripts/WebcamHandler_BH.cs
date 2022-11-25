using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamHandler_BH : MonoBehaviour
{
    public GameObject objectTarget;

    WebCamTexture textureWebCam;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StartWebCam(); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            StopWebCam();
        }
    }

    void StartWebCam()
    {
        textureWebCam = new WebCamTexture(devices[0].name);
        if(textureWebCam != null)
        {
            textureWebCam.requestedFPS = 60;
            Renderer renderer = objectTarget.GetComponent<Renderer>();
            renderer.material.mainTexture = textureWebCam;
            textureWebCam.Play();
        }
    }

    void StopWebCam()
    {
        if(textureWebCam != null)
        {
            textureWebCam.Stop();
            //WebCamTexture.Destroy(textureWebCam);
            //textureWebCam = null;
        }


    }
}
