using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class RealMemoUI : MonoBehaviour
{
    //desc ȭ�� �ִ� ����
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
        //���� ������
        if(tagNum == "1")
        {
           // GameObject taggo = Instantiate(preDoing, TagParent);
            cardTitleText.interactable = true;
            contentText.interactable = true;
           // calenderbtn.interactable = true;
            do_i.sprite = doingIma;
            comI.SetActive(false);

        }
        //2���� �Ϸ�
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
        //�±� 0���̸� ������ ������ empty tag�� �����ǰ�
        //�±� 1���̸� �Ϸ� �±� ���� 
        //1���̸� inputfield�� interectable �������� , ��¥ ��ư�� interectable���� 

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

  
    
    //üũ ��ư�� �̷������� �־��ֱ�
    //������ ��ư�� Ŭ������ �� 
    //�̽���ư�� Ŭ������ �� 
    public void OnCompleteBtn()
    {
        frame.sprite = frame38;
        cal.text = "�Ϸ�";
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
