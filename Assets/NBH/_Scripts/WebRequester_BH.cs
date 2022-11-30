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
    public LogInManager_BH logInManager;

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

            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            requester.onComplete(webRequest.downloadHandler);

        }
        //�׷����ʴٸ�
        else
        {
            //������� ����
            print("��� ����" + webRequest.result + "\n" + webRequest.error);

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
        form.AddBinaryData("image", image, type + ".png", "image/png");

        UnityWebRequest webRequest = UnityWebRequest.Post("http://43.201.58.81:8088/detectFace/checkFace", form);


        //WWW w = new WWW("http://43.201.58.81:8088/detectFace/checkFace", form);
        //WWW w = new WWW("http://192.168.0.25:8001/detectFace/checkFace", form);

        yield return webRequest.SendWebRequest();

        OnUploadPngCompleted(webRequest.downloadHandler);
        
        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print(webRequest.error);
        }
        else
        {
            print("���ۿϷ�");
        }
        webRequest.Dispose();
    }

    public IEnumerator UploadPNG(byte[] image, int memberNo, int commutingManagementNo)
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();

        form.AddField("memberNo", memberNo);
        form.AddField("commutingManagementNo", commutingManagementNo);
        form.AddBinaryData("image", image, image + ".jpg", "image/jpg");

        UnityWebRequest webRequest = UnityWebRequest.Post("http://43.201.58.81:8088/detectFace", form);

        yield return webRequest.SendWebRequest();

        OnAFKCheckCompleted(webRequest.downloadHandler);
        Debug.Log(webRequest.downloadHandler.text);

        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print(webRequest.error);
        }
        else
        {
            print("���ۿϷ�");
        }
        webRequest.Dispose();
    }

    void OnUploadPngCompleted(DownloadHandler handler)
    {
        logInManager.OnCompleteFaceCheck(handler);
    }

    void OnAFKCheckCompleted(DownloadHandler handler)
    {
        AFKCheck_BH.instance.OnCompleteAFKCheck(handler);
    }
}
