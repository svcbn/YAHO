using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum RequestType
{
    POST,
    GET,
    PUT
}

public class HttpRequester : MonoBehaviour
{
    public string url;
    public RequestType requestType;
    public string postData;
    public System.Action<DownloadHandler> onComplete;
}

public class WebRequester_BH : MonoBehaviour
{
    public void WebRequester(string _url, RequestType _requestType)
    {
        HttpRequester requester = new HttpRequester();

        requester.url = _url;
        requester.requestType = _requestType;
        

    }
}
