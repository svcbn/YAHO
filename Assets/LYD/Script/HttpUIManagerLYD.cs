using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;

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
    public GameObject checkContent;

    //��ũ�Ѻ信 ������ ������(cardObject)�� �� ��ġ(cardContent)�� �־��ش�.
    public GameObject cardObject;
    public Transform cardContent;

    public GameObject meetingObject;
    public Transform meetingContent;

    public Dropdown dropdown_project;
    public Dropdown dropdown_meetingPro;
    public int idx;

    public Text _period1;
    public Text _period2;
    //�ɼǿ� �� ������Ʈ�̸���
    public Dropdown.OptionData m_pNamedata;
    //������Ʈ�� �̸����� ���� ����Ʈ��
    public List<Dropdown.OptionData> m_pName = new List<Dropdown.OptionData>();

    public Dropdown.OptionData meetingPNamedata;
    public List<Dropdown.OptionData> meetingPName = new List<Dropdown.OptionData>();

    public RealMemoUI memo;

    public Sprite frame32;


    //�±׺κ�
    public GameObject btnDoing;
    public GameObject btnComplete;
    public GameObject image_tag;
    public GameObject preDoing;
    public GameObject preComplete;
    public Transform empty_tag;
    public Sprite frame38;

    string _title = "���� �� ��";
    string _content = "���������Դϴ�.";
    string _checkTitle = "�� ��";
   // int _tag = 0;

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
        
        #endregion
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
        GameObject go = empty_tag.transform.GetChild(0).gameObject;
        if (go != null)
        {
            Destroy(go);
            num = 0;
        }
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
        GameObject go = Instantiate(preDoing, empty_tag);
        num = 0;
        image_tag.SetActive(false);

    }

    public void Complete()
    {
        GameObject go = Instantiate(preComplete, empty_tag);
        num = 1;
        image_tag.SetActive(false);

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

    public void OnCheckBtnAdd()
    {
        checkdis.SetActive(true);
        if(InputcheckTitle.text != null)
        {
            InputcheckTitle.text = "";
        }
    }

    public void OnCheckXBtn()
    {
        if(InputcheckTitle.text.Length < 1)
        {

        }
        else
        {

        }
        checkdis.SetActive(false);
    }
    public void OnTaskStart()
    {
        GetProject(1, "Y");
    }
    public void OnBtnCancel()
    {
        if (inputTitle.text.Length < 1 || inputContent.text.Length < 1)
        {

        }
        else
        {
            GetProject(1, "Y");

        }
        descdisplay.SetActive(false);

        //->���������� �׳� �Լ��� ������� �ʵ��� 
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
            //PostTodoList();
        //}

       /* if (Input.GetKeyDown(KeyCode.Z))
        {
            //���⼭ ���no  +������Ʈno ��ȸ�ϵ��� �ڵ带 ¥���Ѵ�.
            //xŰ�� ������ �� �̾�������
            //��ҹ�ư ���� �� 

            GetTodolist(1, 1);

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GetProject(1, "Y");

        }*/

        
    }
    

    //descPlay Ȯ�� ��ư�� �ֱ� 
    //todoList post �ϱ� 
    public void PostTodoList()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/todolist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;
        
        
        TodoListData data = new TodoListData();
        data.memberNo = 1; //�̺κ��� (�ڱ��ȣ) ����Ǿ��ִ� �κ��� �������ϴ°��� �ƴѰ�?
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


        foreach (var m in m_pName)
        {
            //drop�ٿ� �ɼǿ� �߰� - ������Ʈ�̸� (m�� ������Ʈ�� ��� �ִ� �̸� : ù ������Ʈ , pn)
            d.options.Add(new Dropdown.OptionData() { text = m.text });

        }
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
        GetTodolist(1, int.Parse(pjNum[index]));//int.Parse(pjNum[index]));
        print("11111111" + int.Parse(pjNum[index]));
        //�̷��ԵǸ� ��ȣ�� �ٲ𶧸��� ��� ������ ��. �׷��� ����ؾ��ұ�?
        //-> ���� index�� 0, 1�� ���� 0���� ù��° ������Ʈ �̸���, 1���� 2��° ������Ʈ �̸� 
        //->projectNo�� (1,2)�� �� �ٲ����� 2�� ������Ʈ�� �ƹ��� to do list�� ������ �������� �ʾ����Ѵ�.
        //��ư�� ���������� �����Ǵ� ���� �ƴ϶� �ѹ��� 
        
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
           
            HttpManagerLYD.instance.Set(cardContent);
            go.GetComponentInChildren<Text>().text = j1["title"].ToString();
            _title = go.GetComponentInChildren<Text>().text;
            
            //üũ�ڽ�
            btnImage = go.transform.GetChild(2).GetComponent<Image>();
            btn = go.transform.GetChild(2).GetComponent<Button>();
            t = go.transform.GetChild(2).GetComponentInChildren<Text>();


            //string���� �±� ��ȣ�� �޾Ƽ� 
            tagNum = j1["tagNo"]["tagNo"].ToString();
            //���� �±� ��ȣ�� 1���̸� btnImage.sprite = frame32;,  //2. ��ư ���ͷ��ͺ� ���ֱ� + ī�� 
            //�̺κ��� üũ����Ʈ���� �ϱ� 
            
            if(tagNum == "1")
            {
                btnImage.sprite = frame38;
                t.text = "�Ϸ�";
                //inputTitle.interactable = false;

                //btn.interactable = false;
                //Button s = go.GetComponent<Button>();
                //s.interactable = false;
            }
            
            //inputContent.text = j1["content"].ToString();
            _content = j1["content"].ToString();
            
            calenderT.text = j1["dueDate"].ToString();

            memo = go.GetComponent<RealMemoUI>();
            memo.Set(_title, _content, calenderT.text, tagNum, descdisplay, btnImage, t, calender);//, btn);

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


    //���⼭ ���� <ȸ�Ƿ�>
    public void OnGetMeeting()
    {
        GetMeeting(1);
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

}
