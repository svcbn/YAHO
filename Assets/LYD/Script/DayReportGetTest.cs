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

    //���������� ������ ���� �ؽ�Ʈ�� �־��ֱ�
    public GameObject meetingtext;
    public GameObject wfTimetext;

    public Transform S_meetingContent;
    public Transform S_WFTTimeContent;

    public Image image;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //�� ��ư�� Ŭ������ �� �ϰ�����Ʈ�� ��ȸ�ȴ�.

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
