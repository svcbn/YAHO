using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class DayReportGetTest : MonoBehaviour
{
    public Text date;
    public Text loginTime;
    public Text logoutTime;
    public Text totalTime;
    public Text workFocus;
    public Text todoAchive;
    public Text todoPercent;

    //프리팹으로 생성한 미팅 텍스트를 넣어주기
    public GameObject meetingtext;
    public GameObject wfTimetext;

    public Transform S_meetingContent;
    public Transform S_WFTTimeContent;

    public Image image;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //이 버튼을 클릭했을 때 일간리포트가 조회된다.

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
