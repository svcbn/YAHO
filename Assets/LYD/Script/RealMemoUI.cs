using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class RealMemoUI : MonoBehaviour
{
    //desc 화면 넣는 변수
    GameObject descDis;
    public string cardTitle;
    public string content;
    public string date;
    public string tagNum;
    public Image frame;
    //public Button btn;

    public Sprite frame38;

    public Sprite doingIma;
    public Sprite issueima;

    public Image do_i;
    public Image i_i;


    InputField cardTitleText;
    InputField contentText;
    Text dateText;
    Text cal;

    public int pjNum;

    public Transform TagParent;
    public Transform cardC;

    public GameObject preDoing;
    public GameObject preComplete;
    public GameObject preIssues;

    public Button calenderbtn;

    public int todo;

    public GameObject comI;

    public void Set(string s1, string s2, string s3, string s4, GameObject go ,GameObject go1, Image i1, Image i2, Image i3, Text t, Button b1, int ii1, Transform t1)
    {
        cardTitle = s1;
        content = s2;
        date = s3;
        descDis = go;
        comI = go1;
        tagNum = s4;
        frame = i1;
        do_i = i2;
        i_i = i3;
        cal = t;
        calenderbtn = b1;
        todo = ii1;
        cardC = t1;
    }


    public void OndescDisplay()
    {
        descDis.SetActive(true);
        
        cardTitleText.text = cardTitle;
        contentText.text = content;
        dateText.text = date;
        /*GameObject gogo = TagParent.transform.GetChild(0).gameObject;
        if(gogo != null)
        {
            Destroy(gogo);

        }*/
        print("3333333333333333333333333 : " + tagNum);
        //번이 진행중
        if(tagNum == "1")
        {
           // GameObject taggo = Instantiate(preDoing, TagParent);
            cardTitleText.interactable = true;
            contentText.interactable = true;
           // calenderbtn.interactable = true;
            do_i.sprite = doingIma;
            comI.SetActive(false);

        }
        //2번이 완료
        else if(tagNum == "2")
        {
            //GameObject taggo1 = Instantiate(preComplete, TagParent);
            cardTitleText.interactable = true;
            contentText.interactable = true;
            // calenderbtn.interactable = false;
            comI.SetActive(true);

        }

        else if (tagNum == "3")
        {
           // GameObject taggo2 = Instantiate(preIssues, TagParent);
            cardTitleText.interactable = true;
            contentText.interactable = true;
            //calenderbtn.interactable = true;
            i_i.sprite = issueima;
            comI.SetActive(false);


        }
        //태그 0번이면 진행중 프리팹 empty tag에 생성되고
        //태그 1번이면 완료 태그 생성 
        //1번이면 inputfield의 interectable 꺼버리기 , 날짜 버튼도 interectable끄기 

    }
    // Start is called before the first frame update
    void Start()
    {
        cardTitleText = descDis.transform.GetChild(0).GetComponent<InputField>();
       // contentText = descDis.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<InputField>();
        contentText = descDis.transform.GetChild(1).GetComponent<InputField>();
        dateText = descDis.transform.GetChild(13).GetChild(1).GetComponent<Text>();
        TagParent = descDis.transform.GetChild(8);
        calenderbtn = descDis.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        

    }

  
    
    //체크 버튼에 이런식으로 넣어주기
    //진행중 버튼을 클릭했을 때 
    //이슈버튼을 클릭했을 때 
    public void OnCompleteBtn()
    {
        frame.sprite = frame38;
        cal.text = "완료";
        GameObject gi = GameObject.Find("HttpUIManager");
        HttpUIManagerLYD hui = gi.GetComponent<HttpUIManagerLYD>();
        hui.PutTeamTag(todo);
        transform.SetSiblingIndex(cardC.childCount);
        //print("77777777777777777777777 : " + transform.GetSiblingIndex());
       // PutCheck();
        /*btn.interactable = false;
        Button s = GetComponent<Button>();
        s.interactable = false;*/
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
