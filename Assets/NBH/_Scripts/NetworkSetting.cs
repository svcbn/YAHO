using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.WebRTC;

namespace Data
{
    public static class NetworkSetting
    {
        public static RTCConfiguration rtcConfiguration = new RTCConfiguration
        {
            iceServers = new RTCIceServer[]
            {
                new RTCIceServer
                {
                    urls = new string[] {"stun:stun.l.google.com:19302"}
                }
            }
        };
    }
}