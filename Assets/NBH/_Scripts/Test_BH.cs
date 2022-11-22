using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Test_BH : MonoBehaviour
{
    WebRequester_BH webRequester;

    private void Awake()
    {
        webRequester = GetComponent<WebRequester_BH>();
    }

    void Test()
    {
        HttpRequester requester = new HttpRequester();

        string memberId = "test1";
        requester.url = "http://43.201.58.81:8088/members/auth/" + memberId;
        requester.requestType = RequestType.GET;

        requester.postData = null;

        requester.onComplete = OnCompleteTest;

        webRequester.SendRequest(requester);
    }

    public void OnCompleteTest(DownloadHandler handler)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
