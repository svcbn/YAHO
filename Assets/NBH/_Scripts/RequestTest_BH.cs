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

        //서버에 요청을 보내고 응답이 올때까지 기다린다.
        yield return webRequest.SendWebRequest();

        //만약에 응답이 성공했다면
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);

            //완료되었다고 requester.onComplete를 실행
            requester.onComplete(webRequest.downloadHandler);
            webRequest.Dispose();


        }
        //그렇지않다면
        else
        {
            //서버통신 실패....ㅠ
            print("통신 실패" + webRequest.result + "\n" + webRequest.error);
            webRequest.Dispose();

        }
        yield return null;
    }

}
