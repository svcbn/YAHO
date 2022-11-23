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

    HttpUIManagerLYD ui;

    InputField cardTitleText;
    InputField contentText;
    Text dateText;
    Text cal;

    public int pjNum;

    public Transform TagParent;

    public GameObject preDoing;
    public GameObject preComplete;
    public Button calenderbtn;


    public void Set(string s1, string s2, string s3, string s4, GameObject go ,Image i1, Text t, Button b1)
    {
        cardTitle = s1;
        content = s2;
        date = s3;
        descDis = go;
        tagNum = s4;
        frame = i1;
        cal = t;
        calenderbtn = b1;
    }


    public void OndescDisplay()
    {
        descDis.SetActive(true);

        cardTitleText.text = cardTitle;
        contentText.text = content;
        dateText.text = date;
        print("3333333333333333333333333 : " + tagNum);
        //2���� ������
        if(tagNum == "2")
        {
            GameObject taggo = Instantiate(preDoing, TagParent);
        }
        if(tagNum == "1")
        {
            GameObject taggo1 = Instantiate(preComplete, TagParent);
            cardTitleText.interactable = false;
            contentText.interactable = false;
            calenderbtn.interactable = false;
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
        dateText = descDis.transform.GetChild(2).GetChild(1).GetComponent<Text>();
        TagParent = descDis.transform.GetChild(8);
        calenderbtn = descDis.transform.GetChild(2).GetChild(0).GetComponent<Button>();


    }

  
    
    //üũ ��ư�� �̷������� �־��ֱ�
    //������ ��ư�� Ŭ������ �� 
    public void OnCompleteBtn()
    {
        frame.sprite = frame38;
        cal.text = "�Ϸ�";
       // PutCheck();
        /*btn.interactable = false;
        Button s = GetComponent<Button>();
        s.interactable = false;*/
        
    }

    /*public void PutCheck()
    {
        GameObject ss = GameObject.Find("HttpUIManager");
        ui = ss.GetComponent<HttpUIManagerLYD>();
        int idx = ui.index;
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //url �ٲٱ�
        requesterLYD.url = "http://43.201.58.81:8088/todolist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.PUT;

        TodoListData data = new TodoListData();
        data.memberNo = 1; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        data.projectNo = int.Parse(ui.pjNum[idx]);
        data.dueDate = dateText.text;
        data.title = cardTitleText.text;
        data.content = contentText.text;
        data.tagNo = 1;
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompleteSignIn;
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);

        //data�� list<TodoListdata>�� �ֱ�
        TodolistDataArray array = JsonUtility.FromJson<TodolistDataArray>(s);
        for (int i = 0; i < array.data.Count; i++)
        {
            print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].dueDate + "\n" + array.data[i].title + "\n" + array.data[i].content + "\n" + array.data[i].tagNo);


        }
        print("��ȸ�Ϸ�");
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
