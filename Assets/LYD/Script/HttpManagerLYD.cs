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
        //만약에 instance가 null이라면
        if(instance == null)
        {
            //instance 나를 넣겠다
            instance = this;
            //씬이 전환되어도 나를 파괴되지 않게 하겠따
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    //서버에게 요청
    //ur;(/post/1).Get
    public void SendRequestLYD(HttpRequesterLYD requesterLYD)
    {
        StartCoroutine(Send(requesterLYD));
    }

    IEnumerator Send(HttpRequesterLYD requesterLYD)
    {
        //초기화 같은 느낌
        UnityWebRequest webRequestLYD = null;
        switch(requesterLYD.requestTypeLYD)
        {
            case RequestTypeLYD.POST:
                webRequestLYD = UnityWebRequest.Post(requesterLYD.url, requesterLYD.todoList);
                webRequestLYD.SetRequestHeader("Content-Type", "application/json");
                //포스트할때는 바이트 배열 만들어주고, 업로드헨들러ㄴ를 만들어줘야함.
                byte[] todoList_byte = Encoding.UTF8.GetBytes(requesterLYD.todoList);
                webRequestLYD.uploadHandler = new UploadHandlerRaw(todoList_byte);
                break;
            case RequestTypeLYD.GET:
                webRequestLYD = UnityWebRequest.Get(requesterLYD.url);
                break;
            case RequestTypeLYD.PUT:
                webRequestLYD = UnityWebRequest.Put(requesterLYD.url, requesterLYD.todoList);
                webRequestLYD.SetRequestHeader("Content-Type", "application/json");
                //포스트할때는 바이트 배열 만들어주고, 업로드헨들러ㄴ를 만들어줘야함.
                byte[] todoList_byte1 = Encoding.UTF8.GetBytes(requesterLYD.todoList);
                webRequestLYD.uploadHandler = new UploadHandlerRaw(todoList_byte1);
                break;


        }
        //서버에 요청을보내고 응답이 올때까지 기다리기
        yield return webRequestLYD.SendWebRequest();

        if(webRequestLYD.result == UnityWebRequest.Result.Success)
        {
            print(webRequestLYD.downloadHandler.text);
            requesterLYD.onComplete(webRequestLYD.downloadHandler);
        }
        else if(webRequestLYD.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"통신 실패: {webRequestLYD.result}");
            // 게시판을 날리기
            Transform[] projectParent = g1.GetComponentsInChildren<Transform>();
            if(projectParent != null)
            {
                for(int i = 1; i < projectParent.Length; i++)
                {
                    if (projectParent[i] != transform)
                        Destroy(projectParent[i].gameObject);     
                }
            }
            print("이름을 알려줘 : " + g1);
            //먼저 HttpUIManager 찾아서 거기있는 스크립트를 가져온다.
            //GameObject p = GameObject.Find("HttpUIManager");
            //HttpUIManagerLYD pr = p.GetComponent<HttpUIManagerLYD>();
           // pr.OnCompleteGetPost
        }
        else if(webRequestLYD.result == UnityWebRequest.Result.ConnectionError || webRequestLYD.result == UnityWebRequest.Result.DataProcessingError)
        {
            print($"통신 실패: {webRequestLYD.result}");
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
