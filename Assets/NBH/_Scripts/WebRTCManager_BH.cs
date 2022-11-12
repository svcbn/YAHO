using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.WebRTC;
using System;

public class WebRTCManager_BH : MonoBehaviour
{
    public static WebRTCManager_BH Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        WebRTC.Initialize();
    }

    private void OnDestroy()
    {
        WebRTC.Dispose();
    }

    private void Start()
    {
        print("WebRTCManager Start");
        StartCoroutine(WebRTC.Update());

    }

    private void Update()
    {
        
    }
}
