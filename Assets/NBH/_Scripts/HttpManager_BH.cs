using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager_BH : MonoBehaviour
{
    public static HttpManager_BH instance;

    private void Awake()
    {
        //���࿡ instance�� null�̶��
        if (instance == null)
        {
            //instance�� ���� �ְڴ�.
            instance = this;
            //���� ��ȯ�� �Ǿ ���� �ı����� �ʰ� �ϰڴ�.
            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            //���� �ı��ϰڴ�.
            Destroy(gameObject);
        }
    }

    //�������� ��û
    //url(posts/1), GET
    public void SendRequest(HttpRequester requester)
    {
        StartCoroutine(Send(requester));
    }

    IEnumerator Send(HttpRequester requester)
    {
        UnityWebRequest webRequest = null;
        //requestType �� ���� ȣ��������Ѵ�.
        switch (requester.requestType)
        {
            case RequestType.POST:
                webRequest = UnityWebRequest.Post(requester.url, requester.postData);
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
