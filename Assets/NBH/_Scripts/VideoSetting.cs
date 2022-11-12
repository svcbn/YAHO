using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.WebRTC;

namespace Data
{
    
    public static class VideoSetting
    {
        [Header("Video Select")]
        public static WebCamDevice SelectedDevice;
        public static int DefaultStreamWidth = 1280;
        public static int DefaultStreamHeight = 720;

        private static bool s_limitTextureSize = true;
        private static Vector2Int s_StreamSize = new Vector2Int(DefaultStreamWidth, DefaultStreamHeight);
        private static RTCRtpCodecCapability s_useVideoCodec = null;

        
        public static bool LimitTextureSize
        {
            get { return s_limitTextureSize; }
            set { s_limitTextureSize = value; }
        }

        public static Vector2Int StreamSize
        {
            get { return s_StreamSize; }
            set { s_StreamSize = value; }
        }

        public static RTCRtpCodecCapability UseVideoCodec
        {
            get { return s_useVideoCodec; }
            set { s_useVideoCodec = value; }
        }
    }
}