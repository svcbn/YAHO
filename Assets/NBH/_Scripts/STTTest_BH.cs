using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaverAPI;
using System.Threading;
using System.Text;



public class STTTest_BH : MonoBehaviour
{
    
    RoomManager_BH roomManager;

    bool isLoading = false;

    public string temp = "";

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager_BH>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(isLoading)
        {
            Debug.Log("변환 완료");
            isLoading = false;
            roomManager.AddMeetingData();

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

        //temp = APIExamSTT.Main("C:\\Users\\HP\\Desktop\\4차프로젝트\\WAV\\test.wav", this);
        temp = APIExamSTT.Main(Application.streamingAssetsPath + "\\test.wav", this);
        temp = temp.Remove(0, 9);
        temp = temp.Remove(temp.Length - 2, 2);

    }

    
}
