using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaverAPI;
using System.Threading;
using System.Text;


public class STTTest_BH : MonoBehaviour
{

    bool isLoading = false;
    string meetingMinutes = "";
    public string temp = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Thread thread = new Thread(Run);
            thread.Start();
        }
        if(isLoading)
        {
            Debug.Log("변환 완료");
            isLoading = false;
            WriteMeeting();
        }
    }

    public void End()
    {
        isLoading = true;
    }

    void Run()
    {

        temp = APIExamSTT.Main("C:\\Users\\HP\\Desktop\\4차프로젝트\\WAV\\test.wav", this);
        temp = temp.Remove(0, 9);
        temp = temp.Remove(temp.Length - 2, 2);

    }

    void WriteMeeting()
    {
        string realTime = System.DateTime.Now.ToString("HH:mm:ss");
        meetingMinutes += realTime + " " + Photon.Pun.PhotonNetwork.NickName + " : " + temp + "\n";
        print(meetingMinutes);
    }
}
