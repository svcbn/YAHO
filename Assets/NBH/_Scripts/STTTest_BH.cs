using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaverAPI;
using System.Threading;
using System.Text;



public class STTTest_BH : MonoBehaviour
{
    
    RequestTest_BH request;

    bool isLoading = false;

    public string temp = "";

    // Start is called before the first frame update
    void Start()
    {
        request = GetComponent<RequestTest_BH>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(isLoading)
        {
            Debug.Log("��ȯ �Ϸ�");
            isLoading = false;
            request.AddMeetingData();

        }
    }

    public void SendWav()
    {
        Thread thread = new Thread(Run);
        thread.Start();
    }

    public void End()
    {
        isLoading = true;
    }

    void Run()
    {

        temp = APIExamSTT.Main("C:\\Users\\HP\\Desktop\\4��������Ʈ\\WAV\\test.wav", this);
        temp = temp.Remove(0, 9);
        temp = temp.Remove(temp.Length - 2, 2);

    }

    
}
