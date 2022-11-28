using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class HttpUIManagerLYD : MonoBehaviour
{
    //descPlay�� �ִ� inputField����.
    public InputField inputTitle;
    public InputField inputContent;
    public Button calender;
    public Text calenderT;
    public Button btnOk;
    public Button btnCancel;
    public GameObject descdisplay;

    //üũ����Ʈ ���� �ִ°͵�
    public GameObject checkdis;
    public InputField InputcheckTitle;
    public GameObject checkObject;
    public Transform checkContent;

    //��ũ�Ѻ信 ������ ������(cardObject)�� �� ��ġ(cardContent)�� �־��ش�.
    public GameObject cardObject;
    public Transform cardContent;

    public GameObject meetingObject;
    public Transform meetingContent;

    public GameObject dayReportMeetingText; //�ؽ�Ʈ ������
    public Transform dayReportMeetingContent;

    public Dropdown dropdown_project;
    public Dropdown dropdown_meetingPro;
    public Dropdown dropdown_dayreport;
    public int idx;
    public int idx1;

    public Text _period1;
    public Text _period2;
    //�ɼǿ� �� ������Ʈ�̸���
    public Dropdown.OptionData m_pNamedata;
    //������Ʈ�� �̸����� ���� ����Ʈ��
    public List<Dropdown.OptionData> m_pName = new List<Dropdown.OptionData>();

    public Dropdown.OptionData meetingPNamedata;
    public List<Dropdown.OptionData> meetingPName = new List<Dropdown.OptionData>();

    public Dropdown.OptionData dayreportData;
    public List<Dropdown.OptionData> dayreportName = new List<Dropdown.OptionData>();

    public RealMemoUI memo;

    public Sprite frame32;

    public GameObject completeImage;
   
   // public int memNo = 1;

    //�±׺κ�
    public GameObject btnDoing;
    public GameObject btnComplete;
    public GameObject image_tag;
    public GameObject preDoing;
    public GameObject preComplete;
    public GameObject preIssues;
    public Transform empty_tag;
    public Sprite frame38;
    public Sprite frame39;


    public Sprite doingImage;
    public Sprite issuesImage;
    public Image oriImage; //����������
    public Image oriIssuesImage; //���� �̽�
    public Sprite ori;

    public GameObject preDI;
    

    string _title = "���� �� ��";
    string _content = "���������Դϴ�.";
    string _checkTitle = "�� ��";


    //�ϰ�����Ʈ
    public Image todoImage;
    public Text t_date;
    public Text t_loginTime;
    public Text t_logoutTime;

    string yy = System.DateTime.Now.ToString("yyyy");
    string mm = System.DateTime.Now.ToString("MM");
    string dd = System.DateTime.Now.ToString("dd");

    public GameObject remindPre;
    public Transform remindContent;

    //�ϰ�����Ʈ
    // int _tag = 0;
    public int commuteNum = 0;
    public int memNo = 0;

    // string _date = "2022-11-20";
    // Start is called before the first frame update
    void Start()
    {
        #region Listener
        inputTitle.onEndEdit.AddListener(Title);
        inputTitle.onSubmit.AddListener(Title);

        inputContent.onEndEdit.AddListener(Content);
        inputContent.onSubmit.AddListener(Content);

        InputcheckTitle.onEndEdit.AddListener(CheckTitle);
        InputcheckTitle.onSubmit.AddListener(CheckTitle);

        btnOk.onClick.AddListener(OnBtnOk);
        btnCancel.onClick.AddListener(OnBtnCancel);
        //���ѿ��� ��ũ��Ʈ.Find("UserInfo").getcomponent<��ũ��Ʈ>.memberNo(); ->����ѹ�ã��
        #endregion
        memNo = GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().MemberNo;
     commuteNum = GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().CommutingManagementNo;

    print("���ٶ�����������  " + memNo);
        /*System.DateTime sDate = new System.DateTime(2022, 11, 30);
        TimeSpan resultTime = sDate - DateTime.Now;
        print("result Time ���~ : " + resultTime.Days);*/
        //GetWorkTime();
        print("mmmmmmmmmmmmmmmmmmmmmmmmmmmm" + GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().MemberNo);

    }

    /* public void tags(int i)
     {
         _tag = i;
         Debug.Log(i);
     }*/

    public int num;
   //�±� add ������ image_tagâ�� ���. 
   public void TagAdd()
   {
        image_tag.SetActive(true);
        
    }

    /*public void TagMinus()
    {
        //���� empty_tag�� �ڽ��� ã�´�.
        
        GameObject go = empty_tag.transform.GetChild(0).gameObject;
        if(go != null)
        {
            Destroy(go);
            num = 0;
        }
    }*/
    public void Doing()
    {
      //  GameObject go = Instantiate(preDoing, empty_tag);
        num = 1;
        //image_tag.SetActive(false);
        oriImage.sprite = doingImage;
    }

    public void Complete()
    {
        //GameObject go = Instantiate(preComplete, empty_tag);
        num = 2;
        //image_tag.SetActive(false);

    }
    
    public void Issues()
    {
        //GameObject go = Instantiate(preIssues, empty_tag);
        num = 3;
        //image_tag.SetActive(false);
        oriIssuesImage.sprite = issuesImage;
    }

    public void TagBtnX()
    {
        image_tag.SetActive(false);
    }

    public void Title(string s)
    {
        _title = s;
        Debug.Log(s);
    }

    public void Content(string s)
    {
        _content = s;
        Debug.Log(s);
    }

    public void CheckTitle(string s)
    {
        _checkTitle = s;
        Debug.Log(s);
    }
    public void OnBtnOk()
    {
        PostTodoList();
        //��� �������� �ʾ��� ���� ������ �������� �ʵ��� ����� ���� ��!

    }

    public void OnBtnAdd()
    {
        descdisplay.SetActive(true);
        if(inputTitle.text != null && inputContent.text != null)
        {
            inputTitle.text = "";
            inputContent.text = "";
        }
        calenderT.text = "0000-00-00";
        
    }

    //checkDis - Ȯ�� ��ư
    public void OnCheckOk()
    {
        PostCheckList();
    }

    public void OnCheckBtnAdd()
    {
        checkdis.SetActive(true);
        if(InputcheckTitle.text != null)
        {
            InputcheckTitle.text = "";
        }
    }

    //�ٹ�ư 
    public void OnCheckXBtn()
    {
        if(InputcheckTitle.text.Length < 1)
        {

        }
        else
        {
            GetCheckList(memNo, int.Parse(pjNum[index]));
            //�Լ��ߵ�
        }
        checkdis.SetActive(false);
    }
    public void OnTaskStart()
    {
        GetProject(memNo, "Y");
    }
    public void OnBtnCancel()
    {
        if (inputTitle.text.Length < 1 || inputContent.text.Length < 1)
        {
            /*GameObject go = empty_tag.transform.GetChild(0).gameObject;
            print("5555555555555555 : " + go);
            if (go != null)
            {
                Destroy(go);
                num = 0;
            }*/
        }
        else
        {
            GetProject(memNo, "Y");
            /*GameObject go = empty_tag.transform.GetChild(0).gameObject;
            print("5555555555555555 : " + go);
            if (go != null)
            {
                Destroy(go);
                num = 0;
            }*/
            oriImage.sprite = ori;
            oriIssuesImage.sprite = ori;
            completeImage.SetActive(false);
        }
        descdisplay.SetActive(false);

        //->���������� �׳� �Լ��� ������� �ʵ��� 
    }
    // Update is called once per frame
    void Update()
    {
        

        
    }
    

    //descPlay Ȯ�� ��ư�� �ֱ� 
    //todoList post �ϱ� 
    public void PostTodoList()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/todolist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;
        
        
        TodoListData data = new TodoListData();
        data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        data.projectNo = int.Parse(pjNum[index]);
        print("�����ѹ��ε�����ȣ : " + int.Parse(pjNum[index])); //�̺κ��� (���ѿ����� �������� ������ �װɷ� ����ͼ� �ϴ°�)
        data.dueDate = calenderT.text; //calender.ShowCalendar(target); ���⿡ target�� �ȳ���// //���� ���ϴ� ��¥ / �޷¿��� üũ�� �κ� Text_Select_Data�� �־��ֱ�  Text_Select_Data.text�־�����ϴµ� buttonŬ���ϰ����� text���� �־������.
        data.title = _title; //inputField �� �ֱ�
        data.content = _content; //inputField�� �ֱ�
        data.tagNo = num; //0,1 �� ���� �ϸ� �Ǵµ�, üũ����Ʈ�� Ŭ���Ǹ� ���� 1�� �Ǹ鼭 �±װ� ����Ǵ°����� ���������Ѵ�. 
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompleteSignIn;
        print("11111111111111");
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);

        //data�� list<TodoListdata>�� �ֱ�
        TodolistDataArray array = JsonUtility.FromJson<TodolistDataArray>(s);
        for(int i = 0; i < array.data.Count; i++)
        {
            print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].dueDate + "\n" + array.data[i].title + "\n" + array.data[i].content + "\n" + array.data[i].tagNo);


        }
        print("��ȸ�Ϸ�");
    }

    //��ư�� ���� �Լ� �ϳ� �� �����

    //��Ӵٿ ������Ʈ�� �����Ѵ�. //������Ʈ ��������Ʈ ��ȸ  //��ư�� ���� �Լ��� �ϳ� �� ������༭ �ű� �ȿ��ٰ� GetProject���� �־��ָ� ��. 
    public void GetProject(int memberNum, string Y)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = $@"http://43.201.58.81:8088/projects?memberNo={memberNum}&isProcess={Y}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnComepleteProject;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);



    }

    public void OnComepleteProject(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());
        

        //�ɼ��� Ŭ�������ִ°� �³�??? < �̺κ��� �����>
        var d = dropdown_project.GetComponent<Dropdown>(); 
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //������Ʈ�� �̸��� dropdown options�� ����ش�. 
            m_pNamedata = new Dropdown.OptionData();
            
            m_pNamedata.text = j1["projectName"].ToString();
           // print("111111111111" + m_pNamedata.text);
            m_pName.Add(m_pNamedata);
            //���⿡�� ����� ������Ʈ �ѹ��� ����Ʈ? �ȿ� ��ܼ� gettodolist�� ��ܾ���. 1, 2
           // pjNum.Add(j1["projectNo"].ToString());
            // string s = dropdown_project.options[].text;
            //dropdown�� ������ ������ ��¥�� �ٲ����Ѵ�. 
            startdate.Add(j1["projectPeriod"]["startDate"].ToString());
            endDate.Add(j1["projectPeriod"]["endDate"].ToString());
            // print("111111111111111111111" + j1["projectMemberLists"].ToString());

            pjNum.Add(j1["projectNo"].ToString());
           //���� ���⼭ ������Ʈ �ѹ��� ��� �ѹ��� �žư����� gettodolist�� �����ؾߵ�.
           
           var jp = j1["projectMemberLists"];
           foreach(var j2 in jp)
           {
                print("������Ʈ�ѹ� : " + j2["projectNo"].ToString());
                print("��� �ѹ� : " + j2["memberNo"].ToString());
           }

        }

        d.AddOptions(m_pName);

        //foreach (var m in m_pName)
        //{
        //    //drop�ٿ� �ɼǿ� �߰� - ������Ʈ�̸� (m�� ������Ʈ�� ��� �ִ� �̸� : ù ������Ʈ , pn)
        //    //d.options.Add(new Dropdown.OptionData() { text = m.text });


        //}
        d.value = index;
        DropdwonpNameSelected(index);

        d.onValueChanged.RemoveAllListeners();
        d.onValueChanged.AddListener(ChangeDropDown);
        
    }

    List<string> startdate = new List<string>();
    List<string> endDate = new List<string>();
   public List<string> pjNum = new List<string>();
    public int index;

    void ChangeDropDown(int index)
    {
        DropdwonpNameSelected(index);
    }


    void DropdwonpNameSelected(int value)
    {

        index = value;

        _period1.text = startdate[index];
        _period2.text = endDate[index];

        //�Խ��� ������ (�Խ��� �ȿ� �ִ� ī�带 ������)
        GetTodolist(memNo, int.Parse(pjNum[index]));//int.Parse(pjNum[index]));
        GetCheckList(memNo, int.Parse(pjNum[index]));

        

    }

    //getTodolist�� �ϱ� ���ؼ��� 

    public void GetTodolist(int memberNum, int projectNum)
    {
        
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/todolist?memberNo={memberNum}&projectNo={projectNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetPost;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
        
            
    }

    public Image btnImage;
    public Button btn;
    public Text t;
    public string tagNum;
    public List<GameObject> tag2 = new List<GameObject>();
    public int todoint;
    public void OnCompleteGetPost(DownloadHandler handler)
    {
        //���̽� �迭 �ȿ� ���̽��� ���� �� ���ϴ� ���� ������ �� �ֵ���
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        var jsonkey = jobject["data"];
        /*for (int i = 0; i < ; i++)
        {
        
        }*/

        RemoveCard();

        foreach(var j1 in jsonkey)
        {
            
            //1. ��ũ�Ѻ� content�ȿ� title button�� (cardObject)�����Ǿ���. 
            GameObject go = Instantiate(cardObject, cardContent);
            //�̰ɷ� ���� �� ���� ������.
            go.transform.SetSiblingIndex(0);
            HttpManagerLYD.instance.Set(cardContent);
            go.GetComponentInChildren<Text>().text = j1["title"].ToString();
            _title = go.GetComponentInChildren<Text>().text;
            
            //üũ�ڽ�
            btnImage = go.transform.GetChild(2).GetComponent<Image>();
            btn = go.transform.GetChild(2).GetComponent<Button>();
            t = go.transform.GetChild(2).GetComponentInChildren<Text>();

            memo = go.GetComponent<RealMemoUI>();


            todoint = int.Parse(j1["todolistNo"].ToString());
            //string���� �±� ��ȣ�� �޾Ƽ� 
            tagNum = j1["tagNo"]["tagNo"].ToString();
            //���� �±� ��ȣ�� 1���̸� btnImage.sprite = frame32;,  //2. ��ư ���ͷ��ͺ� ���ֱ� + ī�� 
            //�̺κ��� üũ����Ʈ���� �ϱ� 
            
            if(tagNum == "2")
            {
                btnImage.sprite = frame38;
                t.text = "�Ϸ�";
                /*tag2.Add(go);
                //tag2 list�� ������ ����������
                for(int i = 0; i < tag2.Count; i++)
                {
                   tag2[i].transform.SetSiblingIndex(cardContent.childCount);
                }*/
                //inputTitle.interactable = false;

                //btn.interactable = false;
                //Button s = go.GetComponent<Button>();
                //s.interactable = false;
            }

            if(tagNum == "3")
            {
                btnImage.sprite = frame39;
                t.text = "�̽�";
            }
            
            //inputContent.text = j1["content"].ToString();
            _content = j1["content"].ToString();
            
            calenderT.text = j1["dueDate"].ToString();

            memo.Set(_title, _content, calenderT.text, tagNum, descdisplay, completeImage, btnImage, oriImage, oriIssuesImage, t, calender, todoint, cardContent);//, btn);

            //Ÿ��Ʋ�� 8�ڸ� �̻��̸� 8�ڸ� ���Ŀ� ...�� �����ش�. 
            if (_title.Length > 8)
            {
                string a = _title.Substring(0, 8);
                _title =  a + "...";
            }
            //�ؽ�Ʈ�� �ٲ����Ƿ� �ٽ� _title�� ������ �����ִ°� 
            go.GetComponentInChildren<Text>().text = _title;
            // print(j1["projectNo"].ToString());
            //  print(j1["tagNo"]["tagName"].ToString());

        }
    }


    public void RemoveCard()
    {
         
        Transform[] projectParent = cardContent.GetComponentsInChildren<Transform>();
        if (projectParent != null)
        {
            for (int i = 1; i < projectParent.Length; i++)
            {
                if (projectParent[i] != transform)
                    Destroy(projectParent[i].gameObject);
            }
        }
    }

    
    //��ȣã�°�. (getsiblingIndex)
    //��ư ������ ǲ �Լ� �ߵ�
    /*public void TestPut(int a)
    {
        print("777777777777777777777" + a);
    }*/

     public void PutTeamTag(int a)
     {
         GameObject ss = GameObject.Find("HttpUIManager");
         HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

         //url �ٲٱ�
         requesterLYD.url = "http://43.201.58.81:8088/todolist";
         requesterLYD.requestTypeLYD = RequestTypeLYD.PUT;

         TodoListData data = new TodoListData();
         data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
                                //data.projectNo = int.Parse(ui.pjNum[idx]);
                                //�̷��� indexã�������� ���� Ȯ���غ���
        data.todolistNo = a;
         requesterLYD.todoList = JsonUtility.ToJson(data, true);
         print(requesterLYD.todoList);

         requesterLYD.onComplete = OnCompleteTeamTag;
         HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


     }

     public void OnCompleteTeamTag(DownloadHandler handler)
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
     }
    //���⼭ ���� <ȸ�Ƿ�>
    public void OnGetMeeting()
    {
        GetMeeting(memNo);
    }
    public void GetMeeting(int memberNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/projects?memberNo={memberNum}&isProcess=Y";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetMeeting;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    List<string> MpNum = new List<string>();
    //��� �ѹ� ��ȸ�ؼ� �̸� ����
   public List<int> mmmNum = new List<int>();
    public void OnCompleteGetMeeting(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        //�ɼ��� Ŭ�������ִ°� �³�??? < �̺κ��� �����>
        var d = dropdown_meetingPro.GetComponent<Dropdown>();
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //������Ʈ�� �̸��� dropdown options�� ����ش�. 
            meetingPNamedata = new Dropdown.OptionData();

            meetingPNamedata.text = j1["projectName"].ToString();
            // print("111111111111" + m_pNamedata.text);
            meetingPName.Add(meetingPNamedata);

            MpNum.Add(j1["projectNo"].ToString());
            print("2222222222222 : " +MpNum);

            var jp = j1["projectMemberLists"];
            foreach (var j2 in jp)
            {
                //print("������Ʈ�ѹ� : " + j2["projectNo"].ToString());
                print("��� �ѹ� : " + j2["memberNo"].ToString());
                mmmNum.Add(int.Parse(j2["memberNo"].ToString()));
            }


        }

        foreach (var m in meetingPName)
        {
            //drop�ٿ� �ɼǿ� �߰� - ������Ʈ�̸� (m�� ������Ʈ�� ��� �ִ� �̸� : ù ������Ʈ , pn)
            d.options.Add(new Dropdown.OptionData() { text = m.text });
            

        }
        d.value = idx;
        DropdwonmeetingPNameSelected(idx);

        d.onValueChanged.RemoveAllListeners();
        d.onValueChanged.AddListener(ChangeDropDown1);
    }

    void ChangeDropDown1(int idx)
    {
        DropdwonmeetingPNameSelected(idx);
    }


    void DropdwonmeetingPNameSelected(int value)
    {

        idx = value;

        //�Խ��� ������ (�Խ��� �ȿ� �ִ� ī�带 ������)
        GetMeetingData(int.Parse(MpNum[idx]));//int.Parse(pjNum[index]));
        print("11111111 : " + int.Parse(MpNum[idx]));
        

    }

    public void GetMeetingData(int proNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/meetings?projectNo={proNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetMeetingData;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }

    //http://43.201.58.81:8088/members/name/1,2,3,4
    
    public void OnCompleteGetMeetingData(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        RemoveMeetingCard();

        var Jm = jobject["data"];
        foreach(var j1 in Jm)
        {
           GameObject startTime = Instantiate(meetingObject, meetingContent);
           
            string mn = j1["meetingNo"].ToString();
            startTime.GetComponent<MeetingGetTest>().a = mn;
            string st = j1["meetingStartTime"].ToString().Substring(0, 10);
            //print(st);
            startTime.GetComponentInChildren<Text>().text = st;

        }
    }

    public void RemoveMeetingCard()
    {

        Transform[] projectParent = meetingContent.GetComponentsInChildren<Transform>();
        if (projectParent != null)
        {
            for (int i = 1; i < projectParent.Length; i++)
            {
                if (projectParent[i] != transform)
                    Destroy(projectParent[i].gameObject);
            }
        }
    }

    /* public void GetMeetingImage(int meetingNo)
     {
         HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

         //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
         requesterLYD.url = $@"http://43.201.58.81:8088/meetings/{meetingNo}";
         requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
         requesterLYD.onComplete = OnCompleteGetMeetingImageData;

         HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
     }

     public void OnCompleteGetMeetingImageData(DownloadHandler handler)
     {

     }
 */

    ////////////////////////////////////
    //üũ����Ʈ 
    public void PostCheckList()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/checklist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        CheckListData data = new CheckListData();
        data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        data.projectNo = int.Parse(pjNum[index]);
        print("�����ѹ��ε�����ȣ : " + int.Parse(pjNum[index])); //�̺κ��� (���ѿ����� �������� ������ �װɷ� ����ͼ� �ϴ°�)
        data.title = _checkTitle;//inputField �� �ֱ�
        //data.checkNo =  //0,1 �� ���� �ϸ� �Ǵµ�, üũ����Ʈ�� Ŭ���Ǹ� ���� 1�� �Ǹ鼭 �±װ� ����Ǵ°����� ���������Ѵ�. 
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompleteCheckList;
        print("11111111111111");
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }

    public void OnCompleteCheckList(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);

        //data�� list<TodoListdata>�� �ֱ�
        CheckListDatArray array = JsonUtility.FromJson<CheckListDatArray>(s);
        for (int i = 0; i < array.data.Count; i++)
        {
            print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].title);


        }
        print("��ȸ�Ϸ�");
    }

    //üũ ��
    public Image btnImage1;
    public Button btn1;
    public int checkint;
    public string chCheked;
    public void GetCheckList(int memberNum, int projectNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/checklist?memberNo={memberNum}&projectNo={projectNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetCheckList;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public List<GameObject> g3 = new List<GameObject>();
    public void OnCompleteGetCheckList(DownloadHandler handler)
    {
        //���̽� �迭 �ȿ� ���̽��� ���� �� ���ϴ� ���� ������ �� �ֵ���
        JObject jobject = JObject.Parse(handler.text);
        print("6666666666666666666666666" + jobject.ToString());

        var jsonkey = jobject["data"];

        //RemoveCard();
       RemoveCheck();

       foreach (var j1 in jsonkey)
        {

            //1. ��ũ�Ѻ� content�ȿ� title button�� (cardObject)�����Ǿ���. 
            GameObject go = Instantiate(checkObject, checkContent);
            //a������ �ø��� 
            go.transform.SetSiblingIndex(0);
            //HttpManagerLYD.instance.Set(cardContent);
            go.GetComponentInChildren<Text>().text = j1["title"].ToString();
            _title = go.GetComponentInChildren<Text>().text;

            //üũ�ڽ�
            btnImage1 = go.transform.GetChild(1).GetComponent<Image>();
            btn1 = go.transform.GetChild(1).GetComponent<Button>();

            checkint = int.Parse(j1["checklistNo"].ToString());
            CheckUI check = go.GetComponent<CheckUI>();
            chCheked = j1["isChecked"].ToString();
            print("fffffffffffffffffffffff : " + chCheked);
            if(chCheked == "Y")
            {
                btnImage1.sprite = frame32;
                btn1.interactable = false;
                go.GetComponent<Button>().interactable = false;
                g3.Add(go);
               /* for(int i = 0; i < g3.Count; i++)
                {
                    g3[i].transform.SetSiblingIndex(checkContent.childCount);

                }*/
            }

            


            check.Set(btnImage1, btn1, checkint, checkContent);//, btn);

           
            // print(j1["projectNo"].ToString());
            //  print(j1["tagNo"]["tagName"].ToString());

        }

    }

    public void RemoveCheck()
    {

        Transform[] projectParent = checkContent.GetComponentsInChildren<Transform>();
        if (projectParent != null)
        {
            for (int i = 1; i < projectParent.Length; i++)
            {
                if (projectParent[i] != transform)
                    Destroy(projectParent[i].gameObject);
            }
        }
    }

    public void PutCheck(int a)
    {
        GameObject ss = GameObject.Find("HttpUIManager");
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //url �ٲٱ�
        requesterLYD.url = "http://43.201.58.81:8088/checklist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.PUT;

        CheckListData data = new CheckListData();
        data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
                               //data.projectNo = int.Parse(ui.pjNum[idx]);
                               //�̷��� indexã�������� ���� Ȯ���غ���
        data.checklistNo = a;

        print("ssssssssssssssssssss : " + a);
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompleteTeamTag;
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);

    }

    //////////////////////////////////////////////////////////////////////////////////
    

    public string todayDate = "20221125";

    //�ϰ�����Ʈ ���ϴ� ��ư ���⼭
    //1..����� ��ȸ�Լ�, -> post�� ��. 
    //1.ȸ�Ƿ� �Լ�, �Ƹ� �����ص� ��¥������ ������ �������̴�...???
    //3.TO DO �޼��� �̹��� �Լ�(üũ����Ʈ�� ���� ��ܾ���),
    //4.�������� �Լ�,
    //5.����������Ʈ(posttodo)�Լ�,
    //6. ������ ��¥
    public string tDdate;
    public void OnDayReportBtn()
    {
        //GetDayReport(memNo, todayDate);
        tDdate = yy + "-" + mm + "-" + dd;
        t_date.text = tDdate;
        //to do �޼��� �̹��� ����Ʈ
        // PostDayReportImage();

        //�̹�ư�� ������ ����� �Ǿ���.
        PostLeave();
       // GetMemberCommute();
        //1. ����ѹ��� ������Ʈ ��ȸ�ϱ� -> 2. ȸ�Ƿ�, TO do �޼��� �̹���, �����δ� 
        GetDayReportProject(memNo);
        GetWorkTime();
    }

    public void GetDayReportProject(int memberNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/projects?memberNo={memberNum}&isProcess=Y";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetDayReportProject;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    List<string> dpNum = new List<string>();
   
    public void OnCompleteGetDayReportProject(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        //�ɼ��� Ŭ�������ִ°� �³�??? < �̺κ��� �����>
        var d = dropdown_dayreport.GetComponent<Dropdown>();
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //������Ʈ�� �̸��� dropdown options�� ����ش�. 
            dayreportData = new Dropdown.OptionData();

            dayreportData.text = j1["projectName"].ToString();
            // print("111111111111" + m_pNamedata.text);
            dayreportName.Add(dayreportData);

            dpNum.Add(j1["projectNo"].ToString());
            print("2222222222222 : " + dpNum);

           

        }

        foreach (var m in dayreportName)
        {
            //drop�ٿ� �ɼǿ� �߰� - ������Ʈ�̸� (m�� ������Ʈ�� ��� �ִ� �̸� : ù ������Ʈ , pn)
            d.options.Add(new Dropdown.OptionData() { text = m.text });


        }
        d.value = idx1;
        DropdwondayReportSelected(idx1);

        d.onValueChanged.RemoveAllListeners();
        d.onValueChanged.AddListener(ChangeDropDown2);
    }

    void ChangeDropDown2(int idx)
    {
        DropdwondayReportSelected(idx);
    }


    void DropdwondayReportSelected(int value)
    {

        idx1 = value;

        //TODO�޼���
        //������Ʈ�� ��ȣ�� ���� �̹����� ������ 
        //PostDayReportImage();
        //
        //�����δ�
        GetRemind(memNo, int.Parse(dpNum[idx1]));
        //ȸ�Ǳ�� 
        GetMeetingDayReportData(int.Parse(dpNum[idx1]));
        PostDayReportImage(int.Parse(dpNum[idx1]));

    }
    //ȸ�Ǳ�� �Լ�
    public void GetMeetingDayReportData(int proNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/meetings?projectNo={proNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetMeetingDayReportData;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }


    public void OnCompleteGetMeetingDayReportData(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        RemoveMeetingDayReportCard();

        var Jm = jobject["data"];
        foreach (var j1 in Jm)
        {
            GameObject startTime = Instantiate(dayReportMeetingText, dayReportMeetingContent);
            if(j1["meetingStartTime"].ToString().Substring(0, 10) == tDdate)
            {
                string st = j1["meetingStartTime"].ToString().Substring(11, 7);
                startTime.GetComponent<Text>().text = st;

            }
            //print(st);

        }
    }

    public void RemoveMeetingDayReportCard()
    {

        Transform[] projectParent = dayReportMeetingContent.GetComponentsInChildren<Transform>();
        if (projectParent != null)
        {
            for (int i = 1; i < projectParent.Length; i++)
            {
                if (projectParent[i] != transform)
                    Destroy(projectParent[i].gameObject);
            }
        }
    }
    /*public void BtnTest()
    {
        PostCome();
    }*/
    public void BtnTest1()
    {
    }

    //����� ��ȸ�Լ�
    /*public void PostCome()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/commutingManagement";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        CommutingManagementData data = new CommutingManagementData();
        data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompletePostCome;
        print("11111111111111");
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }
    public void OnCompletePostCome(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);
       *//* CommutingManagementDataArray array = JsonUtility.FromJson<CommutingManagementDataArray>(s);
        for (int i = 0; i < array.data.Count; i++)
        {
             print(array.data[i].commutingManagementNo);


        }*//*
    }*/
    /*public void GetMemberCommute()
    {
        memeNum = memNo.ToString();

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/commutingManagement/number?memberNo=" + memeNum; //���� �ȵǸ� 
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetMemberCommute;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }*/
    //public int commuteNum = 1;
    /*public void OnCompleteGetMemberCommute(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print("6666666666666666666666666" + jobject.ToString());
        JToken JK = jobject["data"];
        commuteNum = JK.ToString();
        PostLeave();

    }*/
    public void PostLeave()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/commutingManagement/leave";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        CommutingManagementData data = new CommutingManagementData();
        data.commutingManagementNo = commuteNum;

        //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompletePostLeave;
        print("11111111111111");
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);

    }
    public void OnCompletePostLeave(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);

        //data�� list<TodoListdata>�� �ֱ�
        //CommutingManagementDataArray array = JsonUtility.FromJson<CommutingManagementDataArray>(s);
       /*for (int i = 0; i < array.data.Count; i++)
       {
            print(array.data[i].commutingManagementNo);


       }*/
        print("��ȸ�Ϸ�");
        GetCommute();
    }

   /*public void Btn()
    {
        GetDayReport(memNo, todayDate, 1);

    }*/
    


    //���� �Ǵ��� Ȯ��!!
    public string memeNum;
    public void GetCommute()
    {
        memeNum = memNo.ToString();
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/commutingManagement?memberNo=" + memeNum; //���� �ȵǸ� 
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetCommute;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }
    //List<string> login = new List<string>();
    //List<string> logout = new List<string>();

    public void OnCompleteGetCommute(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print("6666666666666666666666666" + jobject.ToString());

        JToken jk = jobject["data"];
        print("8888888888888888888888888" + jk);

        print("gggggggggggggggggggg " + jk.Last);

        
       // JToken j1 = jk[jk.Count()];
            t_loginTime.text = jk.Last["attendanceTime"].ToString().Substring(11,7);


            t_logoutTime.text = jk.Last["leaveTime"].ToString().Substring(11, 7);
        
        //DateTime startDate = Convert.ToDateTime()


    }

    
    // TO DO �޼��� �̹��� �Լ�(üũ����Ʈ�� ���� ��ܾ���)
    public void PostDayReportImage(int x)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/checklist/dailyGraph";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        DailyGraphData data = new DailyGraphData();
        data.memberNo = memNo; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
        data.projectNo = x;
        projNum = x;
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompletePostDayReportImage;
        print("11111111111111");
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);

    }

    int projNum;

    public void OnCompletePostDayReportImage(DownloadHandler handler)
    {
        string s = "{\"data\":" + handler.text + "}";
        print(s);

        //data�� list<TodoListdata>�� �ֱ�
        //DailyGraphDataArray array = JsonUtility.FromJson<DailyGraphDataArray>(s);
        //print("55555555555555555" + array.data.Count);

        /* for (int i = 0; i < array.data.Count; i++)
         {
             print(array.data[i].memberNo);


         }*/
        //print("��ȸ�Ϸ�");
        //�̹���
        GetDayReport(memNo, todayDate, projNum);

    }



    public void GetDayReport(int memberNum, string todayDate, int projectNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/checklist/dailyGraph?memberNo={memberNum}&createDate={todayDate}&projectNo={projectNum}";
        print("dddddddddddddddddddd : " + requesterLYD.url);
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetDayReport;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public void OnCompleteGetDayReport(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print("6666666666666666666666666" + jobject.ToString());

        JToken jk = jobject["data"];
        string ww = jk.ToString();
        print("333333333333333333" + ww);
        StartCoroutine(GetImage(ww));

    }

    IEnumerator GetImage(string url)
    {
        UnityWebRequest ww = UnityWebRequestTexture.GetTexture(url);
        yield return ww.SendWebRequest();

        if (ww.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(ww.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)ww.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
           todoImage.sprite = sprite;
        }
    }

    
    //�����δ� 
    public void GetRemind(int memberNum, int projectNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/todolist?memberNo={memberNum}&projectNo={projectNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetRemind;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }

    public void OnCompleteGetRemind(DownloadHandler handler)
    {
        //���̽� �迭 �ȿ� ���̽��� ���� �� ���ϴ� ���� ������ �� �ֵ���
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        var jsonkey = jobject["data"];
        /*for (int i = 0; i < ; i++)
        {
        
        }*/

        RemoveRemind();

        foreach (var j1 in jsonkey)
        {

            //1. ��ũ�Ѻ� content�ȿ� title button�� (cardObject)�����Ǿ���. 
            GameObject go = Instantiate(remindPre, remindContent);
            //Title
            go.transform.GetChild(0).GetComponent<Text>().text = j1["title"].ToString();

            //��������
            
            go.transform.GetChild(2).GetComponent<Text>().text = j1["dueDate"].ToString();
            string[] dateTime = go.transform.GetChild(2).GetComponent<Text>().text.Split("-");
            int yyyy = int.Parse(dateTime[0]);
            int mm = int.Parse(dateTime[1]);
            int dd = int.Parse(dateTime[2]);
            //Ư����¥���� ���糯¥ ����
            System.DateTime sDate = new System.DateTime(yyyy, mm, dd);
            TimeSpan resultTime = sDate - DateTime.Now;
            int resultDay = resultTime.Days;
           // print("result Time ���~ : "+resultTime.Days);
            //TimeSpan r = 3;
            go.transform.GetChild(4).GetComponent<Text>().text = j1["content"].ToString();


            //string���� �±� ��ȣ�� �޾Ƽ� 
            tagNum = j1["tagNo"]["tagNo"].ToString();


            //1�� ������ + 3���� �������� ���� �ӹ� �±װ� ���. 
            if(tagNum == "1" &&resultDay >= 1 && resultDay <= 3)
            {
                GameObject g1 = Instantiate(preDI, go.transform.GetChild(5));

            }
            if(tagNum == "1" && resultDay < 1)
            {
                Destroy(go);
            }
            //2�� �Ϸ�
            if (tagNum == "2")
            {
                Destroy(go);
                //inputTitle.interactable = false;

                //btn.interactable = false;
                //Button s = go.GetComponent<Button>();
                //s.interactable = false;
            }

            //3�� �̽�
            if (tagNum == "3")
            {
                GameObject g1 = Instantiate(preIssues, go.transform.GetChild(5));
            }

            //inputContent.text = j1["content"].ToString();



            

        }
    }

   public void RemoveRemind()
    {
        Transform[] projectParent = remindContent.GetComponentsInChildren<Transform>();
        if (projectParent != null)
        {
            for (int i = 1; i < projectParent.Length; i++)
            {
                if (projectParent[i] != transform)
                    Destroy(projectParent[i].gameObject);
            }
        }
    }

    public void GetWorkTime( )
    {
        memeNum = memNo.ToString();
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
        requesterLYD.url = $@"http://43.201.58.81:8088/workTime?memberNo=" + memeNum;
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetWorkTime;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public void OnCompleteGetWorkTime(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print("���������������������������������������� " + jobject.ToString());
    }



}
