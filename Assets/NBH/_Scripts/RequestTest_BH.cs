using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun;




public class RequestTest_BH : MonoBehaviour
{
     public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        switch(requester.requestType)
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

        //������ ��û�� ������ ������ �ö����� ��ٸ���.
        yield return webRequest.SendWebRequest();

        //���࿡ ������ �����ߴٸ�
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);

            //�Ϸ�Ǿ��ٰ� requester.onComplete�� ����
            requester.onComplete(webRequest.downloadHandler);
            webRequest.Dispose();


        }
        //�׷����ʴٸ�
        else
        {
            //������� ����....��
            print("��� ����" + webRequest.result + "\n" + webRequest.error);
            webRequest.Dispose();

        }
        yield return null;
    }

}
