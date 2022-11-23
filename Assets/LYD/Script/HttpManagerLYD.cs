using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class HttpManagerLYD : MonoBehaviour
{
    public static HttpManagerLYD instance;

    Transform g1;

    private void Awake()
    {
        //���࿡ instance�� null�̶��
        if(instance == null)
        {
            //instance ���� �ְڴ�
            instance = this;
            //���� ��ȯ�Ǿ ���� �ı����� �ʰ� �ϰڵ�
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    //�������� ��û
    //ur;(/post/1).Get
    public void SendRequestLYD(HttpRequesterLYD requesterLYD)
    {
        StartCoroutine(Send(requesterLYD));
    }

    IEnumerator Send(HttpRequesterLYD requesterLYD)
    {
        //�ʱ�ȭ ���� ����
        UnityWebRequest webRequestLYD = null;
        switch(requesterLYD.requestTypeLYD)
        {
            case RequestTypeLYD.POST:
                webRequestLYD = UnityWebRequest.Post(requesterLYD.url, requesterLYD.todoList);
                webRequestLYD.SetRequestHeader("Content-Type", "application/json");
                //����Ʈ�Ҷ��� ����Ʈ �迭 ������ְ�, ���ε���鷯���� ����������.
                byte[] todoList_byte = Encoding.UTF8.GetBytes(requesterLYD.todoList);
                webRequestLYD.uploadHandler = new UploadHandlerRaw(todoList_byte);
                break;
            case RequestTypeLYD.GET:
                webRequestLYD = UnityWebRequest.Get(requesterLYD.url);
                break;
            case RequestTypeLYD.PUT:
                webRequestLYD = UnityWebRequest.Put(requesterLYD.url, requesterLYD.todoList);
                webRequestLYD.SetRequestHeader("Content-Type", "application/json");
                //����Ʈ�Ҷ��� ����Ʈ �迭 ������ְ�, ���ε���鷯���� ����������.
                byte[] todoList_byte1 = Encoding.UTF8.GetBytes(requesterLYD.todoList);
                webRequestLYD.uploadHandler = new UploadHandlerRaw(todoList_byte1);
                break;


        }
        //������ ��û�������� ������ �ö����� ��ٸ���
        yield return webRequestLYD.SendWebRequest();

        if(webRequestLYD.result == UnityWebRequest.Result.Success)
        {
            print(webRequestLYD.downloadHandler.text);
            requesterLYD.onComplete(webRequestLYD.downloadHandler);
        }
        else if(webRequestLYD.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"��� ����: {webRequestLYD.result}");
            // �Խ����� ������
            Transform[] projectParent = g1.GetComponentsInChildren<Transform>();
            if(projectParent != null)
            {
                for(int i = 1; i < projectParent.Length; i++)
                {
                    if (projectParent[i] != transform)
                        Destroy(projectParent[i].gameObject);     
                }
            }
            print("�̸��� �˷��� : " + g1);
            //���� HttpUIManager ã�Ƽ� �ű��ִ� ��ũ��Ʈ�� �����´�.
            //GameObject p = GameObject.Find("HttpUIManager");
            //HttpUIManagerLYD pr = p.GetComponent<HttpUIManagerLYD>();
           // pr.OnCompleteGetPost
        }
        else if(webRequestLYD.result == UnityWebRequest.Result.ConnectionError || webRequestLYD.result == UnityWebRequest.Result.DataProcessingError)
        {
            print($"��� ����: {webRequestLYD.result}");
        }
        webRequestLYD.Dispose();
        yield return null;
    }

    public void Set(Transform s)
    {
        g1 = s;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
