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

public class HttpRequester
{
    public string url;
    public RequestType requestType;
    public string postData;
    public System.Action<DownloadHandler> onComplete;
    public System.Action onFailed;
}

public class WebRequester_BH : MonoBehaviour
{
    public static WebRequester_BH instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        switch (requester.requestType)
        {
            case RequestType.POST:
                webRequest = UnityWebRequest.Post(requester.url, requester.postData);
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(requester.postData);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.GET:
                webRequest = UnityWebRequest.Get(requester.url);
                break;
            case RequestType.PUT:
                webRequest = UnityWebRequest.Put(requester.url, requester.postData);
                break;
        }

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);

            //완료되었다고 requester.onComplete를 실행
            requester.onComplete(webRequest.downloadHandler);

        }
        //그렇지않다면
        else
        {
            //서버통신 실패
            print("통신 실패" + webRequest.result + "\n" + webRequest.error);

            requester.onFailed();

        }

        yield return null;
        webRequest.Dispose();

    }

    public IEnumerator UploadPNG(byte[] image, string type)
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();
        form.AddField("faceType", type);
        form.AddBinaryData("image", image, type+".png", "image/png");

        WWW w = new WWW("http://43.201.58.81:8088/members/detectFace/checkFace", form);
        yield return w;
        if (!string.IsNullOrEmpty(w.error))
        {
            print(w.error);
        }
        else
        {
            print("전송완료");
        }
        Debug.Log(w.text);
        w.Dispose();
    }
}
