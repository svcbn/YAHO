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
    //descPlay에 있는 inputField들임.
    public InputField inputTitle;
    public InputField inputContent;
    public Button calender;
    public Text calenderT;
    public Button btnOk;
    public Button btnCancel;
    public GameObject descdisplay;

    //체크리스트 한테 있는것들
    public GameObject checkdis;
    public InputField InputcheckTitle;
    public GameObject checkObject;
    public Transform checkContent;

    //스크롤뷰에 생성될 프리팹(cardObject)와 그 위치(cardContent)를 넣어준다.
    public GameObject cardObject;
    public Transform cardContent;

    public GameObject meetingObject;
    public Transform meetingContent;

    public GameObject dayReportMeetingText; //텍스트 프리팹
    public Transform dayReportMeetingContent;

    public Dropdown dropdown_project;
    public Dropdown dropdown_meetingPro;
    public Dropdown dropdown_dayreport;
    public int idx;
    public int idx1;

    public Text _period1;
    public Text _period2;
    //옵션에 들어갈 프로젝트이름들
    public Dropdown.OptionData m_pNamedata;
    //프로젝트의 이름들을 담을 리스트들
    public List<Dropdown.OptionData> m_pName = new List<Dropdown.OptionData>();

    public Dropdown.OptionData meetingPNamedata;
    public List<Dropdown.OptionData> meetingPName = new List<Dropdown.OptionData>();

    public Dropdown.OptionData dayreportData;
    public List<Dropdown.OptionData> dayreportName = new List<Dropdown.OptionData>();

    public RealMemoUI memo;

    public Sprite frame32;

    public GameObject completeImage;
   
   // public int memNo = 1;

    //태그부분
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
    public Image oriImage; //원래진행중
    public Image oriIssuesImage; //원래 이슈
    public Sprite ori;

    public GameObject preDI;
    

    string _title = "오늘 할 일";
    string _content = "무슨내용입니다.";
    string _checkTitle = "할 일";


    //일간리포트
    public Image todoImage;
    public Text t_date;
    public Text t_loginTime;
    public Text t_logoutTime;

    string yy = System.DateTime.Now.ToString("yyyy");
    string mm = System.DateTime.Now.ToString("MM");
    string dd = System.DateTime.Now.ToString("dd");

    public GameObject remindPre;
    public Transform remindContent;

    //일간리포트
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
        //병한오빠 스크립트.Find("UserInfo").getcomponent<스크립트>.memberNo(); ->멤버넘버찾기
        #endregion
        memNo = GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().MemberNo;
     commuteNum = GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().CommutingManagementNo;

    print("제바라라라라라랄라라라라  " + memNo);
        /*System.DateTime sDate = new System.DateTime(2022, 11, 30);
        TimeSpan resultTime = sDate - DateTime.Now;
        print("result Time 띠용~ : " + resultTime.Days);*/
        //GetWorkTime();
        print("mmmmmmmmmmmmmmmmmmmmmmmmmmmm" + GameObject.Find("UserInfo").GetComponent<UserInformation_BH>().MemberNo);

    }

    /* public void tags(int i)
     {
         _tag = i;
         Debug.Log(i);
     }*/

    public int num;
   //태그 add 누르면 image_tag창이 뜬다. 
   public void TagAdd()
   {
        image_tag.SetActive(true);
        
    }

    /*public void TagMinus()
    {
        //먼저 empty_tag의 자식을 찾는다.
        
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
        //대신 수정하지 않았을 때는 눌러도 보내지지 않도록 만들어 놓을 것!

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

    //checkDis - 확인 버튼
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

    //겟버튼 
    public void OnCheckXBtn()
    {
        if(InputcheckTitle.text.Length < 1)
        {

        }
        else
        {
            GetCheckList(memNo, int.Parse(pjNum[index]));
            //함수발동
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

        //->안적을때는 그냥 함수가 실행되지 않도록 
    }
    // Update is called once per frame
    void Update()
    {
        

        
    }
    

    //descPlay 확인 버튼에 넣기 
    //todoList post 하기 
    public void PostTodoList()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/todolist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;
        
        
        TodoListData data = new TodoListData();
        data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
        data.projectNo = int.Parse(pjNum[index]);
        print("프젝넘버인덱스번호 : " + int.Parse(pjNum[index])); //이부분은 (병한오빠가 프젝명을 적으면 그걸로 갖고와서 하는것)
        data.dueDate = calenderT.text; //calender.ShowCalendar(target); 여기에 target이 안나옴// //내가 원하는 날짜 / 달력에서 체크한 부분 Text_Select_Data를 넣어주기  Text_Select_Data.text넣어줘야하는데 button클릭하고나서의 text값을 넣어줘야함.
        data.title = _title; //inputField 에 넣기
        data.content = _content; //inputField에 넣기
        data.tagNo = num; //0,1 로 가게 하면 되는데, 체크리스트가 클릭되면 이제 1로 되면서 태그가 변경되는것으로 보내져야한다. 
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

        //data를 list<TodoListdata>에 넣기
        TodolistDataArray array = JsonUtility.FromJson<TodolistDataArray>(s);
        for(int i = 0; i < array.data.Count; i++)
        {
            print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].dueDate + "\n" + array.data[i].title + "\n" + array.data[i].content + "\n" + array.data[i].tagNo);


        }
        print("조회완료");
    }

    //버튼에 넣을 함수 하나 더 만들기

    //드롭다운에 프로젝트가 떠야한다. //프로젝트 정보리스트 조회  //버튼에 넣을 함수를 하나 더 만들어줘서 거기 안에다가 GetProject값을 넣어주면 됨. 
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
        

        //옵션을 클리어해주는게 맞나??? < 이부분은 물어보기>
        var d = dropdown_project.GetComponent<Dropdown>(); 
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //프로젝트의 이름을 dropdown options에 담아준다. 
            m_pNamedata = new Dropdown.OptionData();
            
            m_pNamedata.text = j1["projectName"].ToString();
           // print("111111111111" + m_pNamedata.text);
            m_pName.Add(m_pNamedata);
            //여기에서 생기는 프로젝트 넘버가 리스트? 안에 담겨서 gettodolist에 담겨야함. 1, 2
           // pjNum.Add(j1["projectNo"].ToString());
            // string s = dropdown_project.options[].text;
            //dropdown을 눌렀을 때마다 날짜가 바뀌어야한다. 
            startdate.Add(j1["projectPeriod"]["startDate"].ToString());
            endDate.Add(j1["projectPeriod"]["endDate"].ToString());
            // print("111111111111111111111" + j1["projectMemberLists"].ToString());

            pjNum.Add(j1["projectNo"].ToString());
           //이제 여기서 프로젝트 넘버랑 멤버 넘버를 꼽아가지고 gettodolist랑 연결해야됨.
           
           var jp = j1["projectMemberLists"];
           foreach(var j2 in jp)
           {
                print("프로젝트넘버 : " + j2["projectNo"].ToString());
                print("멤버 넘버 : " + j2["memberNo"].ToString());
           }

        }

        d.AddOptions(m_pName);

        //foreach (var m in m_pName)
        //{
        //    //drop다운 옵션에 추가 - 프로젝트이름 (m이 프로젝트가 담겨 있는 이름 : 첫 프로젝트 , pn)
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

        //게시판 날리기 (게시판 안에 있는 카드를 날리기)
        GetTodolist(memNo, int.Parse(pjNum[index]));//int.Parse(pjNum[index]));
        GetCheckList(memNo, int.Parse(pjNum[index]));

        

    }

    //getTodolist를 하기 위해서는 

    public void GetTodolist(int memberNum, int projectNum)
    {
        
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
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
        //제이슨 배열 안에 제이슨이 있을 때 원하는 값만 가져올 수 있도록
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        var jsonkey = jobject["data"];
        /*for (int i = 0; i < ; i++)
        {
        
        }*/

        RemoveCard();

        foreach(var j1 in jsonkey)
        {
            
            //1. 스크롤뷰 content안에 title button이 (cardObject)생성되야함. 
            GameObject go = Instantiate(cardObject, cardContent);
            //이걸로 인해 맨 위로 생성됨.
            go.transform.SetSiblingIndex(0);
            HttpManagerLYD.instance.Set(cardContent);
            go.GetComponentInChildren<Text>().text = j1["title"].ToString();
            _title = go.GetComponentInChildren<Text>().text;
            
            //체크박스
            btnImage = go.transform.GetChild(2).GetComponent<Image>();
            btn = go.transform.GetChild(2).GetComponent<Button>();
            t = go.transform.GetChild(2).GetComponentInChildren<Text>();

            memo = go.GetComponent<RealMemoUI>();


            todoint = int.Parse(j1["todolistNo"].ToString());
            //string으로 태그 번호를 받아서 
            tagNum = j1["tagNo"]["tagNo"].ToString();
            //만약 태그 번호가 1번이면 btnImage.sprite = frame32;,  //2. 버튼 인터렉터블 꺼주기 + 카드 
            //이부분은 체크리스트에서 하기 
            
            if(tagNum == "2")
            {
                btnImage.sprite = frame38;
                t.text = "완료";
                /*tag2.Add(go);
                //tag2 list가 밑으로 내려가도록
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
                t.text = "이슈";
            }
            
            //inputContent.text = j1["content"].ToString();
            _content = j1["content"].ToString();
            
            calenderT.text = j1["dueDate"].ToString();

            memo.Set(_title, _content, calenderT.text, tagNum, descdisplay, completeImage, btnImage, oriImage, oriIssuesImage, t, calender, todoint, cardContent);//, btn);

            //타이틀이 8자리 이상이면 8자리 이후에 ...을 더해준다. 
            if (_title.Length > 8)
            {
                string a = _title.Substring(0, 8);
                _title =  a + "...";
            }
            //텍스트를 바꿨으므로 다시 _title에 적용을 시켜주는것 
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

    
    //번호찾는것. (getsiblingIndex)
    //버튼 누르면 풋 함수 발동
    /*public void TestPut(int a)
    {
        print("777777777777777777777" + a);
    }*/

     public void PutTeamTag(int a)
     {
         GameObject ss = GameObject.Find("HttpUIManager");
         HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

         //url 바꾸기
         requesterLYD.url = "http://43.201.58.81:8088/todolist";
         requesterLYD.requestTypeLYD = RequestTypeLYD.PUT;

         TodoListData data = new TodoListData();
         data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
                                //data.projectNo = int.Parse(ui.pjNum[idx]);
                                //이렇게 index찾아지는지 먼저 확인해보기
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

         //data를 list<TodoListdata>에 넣기
         TodolistDataArray array = JsonUtility.FromJson<TodolistDataArray>(s);
         for (int i = 0; i < array.data.Count; i++)
         {
             print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].dueDate + "\n" + array.data[i].title + "\n" + array.data[i].content + "\n" + array.data[i].tagNo);


         }
         print("조회완료");
     }
    //여기서 부터 <회의록>
    public void OnGetMeeting()
    {
        GetMeeting(memNo);
    }
    public void GetMeeting(int memberNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/projects?memberNo={memberNum}&isProcess=Y";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetMeeting;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    List<string> MpNum = new List<string>();
    //멤버 넘버 조회해서 이름 생성
   public List<int> mmmNum = new List<int>();
    public void OnCompleteGetMeeting(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        //옵션을 클리어해주는게 맞나??? < 이부분은 물어보기>
        var d = dropdown_meetingPro.GetComponent<Dropdown>();
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //프로젝트의 이름을 dropdown options에 담아준다. 
            meetingPNamedata = new Dropdown.OptionData();

            meetingPNamedata.text = j1["projectName"].ToString();
            // print("111111111111" + m_pNamedata.text);
            meetingPName.Add(meetingPNamedata);

            MpNum.Add(j1["projectNo"].ToString());
            print("2222222222222 : " +MpNum);

            var jp = j1["projectMemberLists"];
            foreach (var j2 in jp)
            {
                //print("프로젝트넘버 : " + j2["projectNo"].ToString());
                print("멤버 넘버 : " + j2["memberNo"].ToString());
                mmmNum.Add(int.Parse(j2["memberNo"].ToString()));
            }


        }

        foreach (var m in meetingPName)
        {
            //drop다운 옵션에 추가 - 프로젝트이름 (m이 프로젝트가 담겨 있는 이름 : 첫 프로젝트 , pn)
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

        //게시판 날리기 (게시판 안에 있는 카드를 날리기)
        GetMeetingData(int.Parse(MpNum[idx]));//int.Parse(pjNum[index]));
        print("11111111 : " + int.Parse(MpNum[idx]));
        

    }

    public void GetMeetingData(int proNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
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

         //post/1, get, 완료되었을 때 호출되는 함수
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
    //체크리스트 
    public void PostCheckList()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/checklist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        CheckListData data = new CheckListData();
        data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
        data.projectNo = int.Parse(pjNum[index]);
        print("프젝넘버인덱스번호 : " + int.Parse(pjNum[index])); //이부분은 (병한오빠가 프젝명을 적으면 그걸로 갖고와서 하는것)
        data.title = _checkTitle;//inputField 에 넣기
        //data.checkNo =  //0,1 로 가게 하면 되는데, 체크리스트가 클릭되면 이제 1로 되면서 태그가 변경되는것으로 보내져야한다. 
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

        //data를 list<TodoListdata>에 넣기
        CheckListDatArray array = JsonUtility.FromJson<CheckListDatArray>(s);
        for (int i = 0; i < array.data.Count; i++)
        {
            print(array.data[i].memberNo + "\n" + array.data[i].projectNo + "\n" + array.data[i].title);


        }
        print("조회완료");
    }

    //체크 겟
    public Image btnImage1;
    public Button btn1;
    public int checkint;
    public string chCheked;
    public void GetCheckList(int memberNum, int projectNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/checklist?memberNo={memberNum}&projectNo={projectNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetCheckList;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public List<GameObject> g3 = new List<GameObject>();
    public void OnCompleteGetCheckList(DownloadHandler handler)
    {
        //제이슨 배열 안에 제이슨이 있을 때 원하는 값만 가져올 수 있도록
        JObject jobject = JObject.Parse(handler.text);
        print("6666666666666666666666666" + jobject.ToString());

        var jsonkey = jobject["data"];

        //RemoveCard();
       RemoveCheck();

       foreach (var j1 in jsonkey)
        {

            //1. 스크롤뷰 content안에 title button이 (cardObject)생성되야함. 
            GameObject go = Instantiate(checkObject, checkContent);
            //a맨위로 올리기 
            go.transform.SetSiblingIndex(0);
            //HttpManagerLYD.instance.Set(cardContent);
            go.GetComponentInChildren<Text>().text = j1["title"].ToString();
            _title = go.GetComponentInChildren<Text>().text;

            //체크박스
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

        //url 바꾸기
        requesterLYD.url = "http://43.201.58.81:8088/checklist";
        requesterLYD.requestTypeLYD = RequestTypeLYD.PUT;

        CheckListData data = new CheckListData();
        data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
                               //data.projectNo = int.Parse(ui.pjNum[idx]);
                               //이렇게 index찾아지는지 먼저 확인해보기
        data.checklistNo = a;

        print("ssssssssssssssssssss : " + a);
        requesterLYD.todoList = JsonUtility.ToJson(data, true);
        print(requesterLYD.todoList);

        requesterLYD.onComplete = OnCompleteTeamTag;
        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);

    }

    //////////////////////////////////////////////////////////////////////////////////
    

    public string todayDate = "20221125";

    //일간리포트 겟하는 버튼 여기서
    //1..출퇴근 조회함수, -> post는 됨. 
    //1.회의록 함수, 아마 겟을해도 날짜때문에 뜨지는 않을것이다...???
    //3.TO DO 달성률 이미지 함수(체크리스트에 값이 담겨야함),
    //4.업무집중 함수,
    //5.팀업무리스트(posttodo)함수,
    //6. 오늘의 날짜
    public string tDdate;
    public void OnDayReportBtn()
    {
        //GetDayReport(memNo, todayDate);
        tDdate = yy + "-" + mm + "-" + dd;
        t_date.text = tDdate;
        //to do 달성률 이미지 포스트
        // PostDayReportImage();

        //이버튼을 누르면 퇴근이 되야함.
        PostLeave();
       // GetMemberCommute();
        //1. 멤버넘버로 프로젝트 조회하기 -> 2. 회의록, TO do 달성률 이미지, 리마인더 
        GetDayReportProject(memNo);
        GetWorkTime();
    }

    public void GetDayReportProject(int memberNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
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

        //옵션을 클리어해주는게 맞나??? < 이부분은 물어보기>
        var d = dropdown_dayreport.GetComponent<Dropdown>();
        //d.ClearOptions();
        //d.value = 1;
        var jsonP = jobject["data"];
        print("1111111" + jsonP);
        foreach (var j1 in jsonP)
        {
            //프로젝트의 이름을 dropdown options에 담아준다. 
            dayreportData = new Dropdown.OptionData();

            dayreportData.text = j1["projectName"].ToString();
            // print("111111111111" + m_pNamedata.text);
            dayreportName.Add(dayreportData);

            dpNum.Add(j1["projectNo"].ToString());
            print("2222222222222 : " + dpNum);

           

        }

        foreach (var m in dayreportName)
        {
            //drop다운 옵션에 추가 - 프로젝트이름 (m이 프로젝트가 담겨 있는 이름 : 첫 프로젝트 , pn)
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

        //TODO달성률
        //프로젝트의 번호에 따라 이미지가 떠야함 
        //PostDayReportImage();
        //
        //리마인더
        GetRemind(memNo, int.Parse(dpNum[idx1]));
        //회의기록 
        GetMeetingDayReportData(int.Parse(dpNum[idx1]));
        PostDayReportImage(int.Parse(dpNum[idx1]));

    }
    //회의기록 함수
    public void GetMeetingDayReportData(int proNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
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

    //출퇴근 조회함수
    /*public void PostCome()
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/commutingManagement";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        CommutingManagementData data = new CommutingManagementData();
        data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
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

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/commutingManagement/number?memberNo=" + memeNum; //만약 안되면 
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

        //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
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

        //data를 list<TodoListdata>에 넣기
        //CommutingManagementDataArray array = JsonUtility.FromJson<CommutingManagementDataArray>(s);
       /*for (int i = 0; i < array.data.Count; i++)
       {
            print(array.data[i].commutingManagementNo);


       }*/
        print("조회완료");
        GetCommute();
    }

   /*public void Btn()
    {
        GetDayReport(memNo, todayDate, 1);

    }*/
    


    //여기 되는지 확인!!
    public string memeNum;
    public void GetCommute()
    {
        memeNum = memNo.ToString();
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/commutingManagement?memberNo=" + memeNum; //만약 안되면 
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

    
    // TO DO 달성률 이미지 함수(체크리스트에 값이 담겨야함)
    public void PostDayReportImage(int x)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        requesterLYD.url = "http://43.201.58.81:8088/checklist/dailyGraph";
        requesterLYD.requestTypeLYD = RequestTypeLYD.POST;


        DailyGraphData data = new DailyGraphData();
        data.memberNo = memNo; //이부분은 (자기번호) 저장되어있는 부분을 보내야하는것이 아닌가?
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

        //data를 list<TodoListdata>에 넣기
        //DailyGraphDataArray array = JsonUtility.FromJson<DailyGraphDataArray>(s);
        //print("55555555555555555" + array.data.Count);

        /* for (int i = 0; i < array.data.Count; i++)
         {
             print(array.data[i].memberNo);


         }*/
        //print("조회완료");
        //이미지
        GetDayReport(memNo, todayDate, projNum);

    }



    public void GetDayReport(int memberNum, string todayDate, int projectNum)
    {

        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
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

    
    //리마인더 
    public void GetRemind(int memberNum, int projectNum)
    {
        HttpRequesterLYD requesterLYD = new HttpRequesterLYD();

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/todolist?memberNo={memberNum}&projectNo={projectNum}";
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetRemind;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);
    }

    public void OnCompleteGetRemind(DownloadHandler handler)
    {
        //제이슨 배열 안에 제이슨이 있을 때 원하는 값만 가져올 수 있도록
        JObject jobject = JObject.Parse(handler.text);
        print(jobject.ToString());

        var jsonkey = jobject["data"];
        /*for (int i = 0; i < ; i++)
        {
        
        }*/

        RemoveRemind();

        foreach (var j1 in jsonkey)
        {

            //1. 스크롤뷰 content안에 title button이 (cardObject)생성되야함. 
            GameObject go = Instantiate(remindPre, remindContent);
            //Title
            go.transform.GetChild(0).GetComponent<Text>().text = j1["title"].ToString();

            //마감기한
            
            go.transform.GetChild(2).GetComponent<Text>().text = j1["dueDate"].ToString();
            string[] dateTime = go.transform.GetChild(2).GetComponent<Text>().text.Split("-");
            int yyyy = int.Parse(dateTime[0]);
            int mm = int.Parse(dateTime[1]);
            int dd = int.Parse(dateTime[2]);
            //특정날짜에서 현재날짜 뺴기
            System.DateTime sDate = new System.DateTime(yyyy, mm, dd);
            TimeSpan resultTime = sDate - DateTime.Now;
            int resultDay = resultTime.Days;
           // print("result Time 띠용~ : "+resultTime.Days);
            //TimeSpan r = 3;
            go.transform.GetChild(4).GetComponent<Text>().text = j1["content"].ToString();


            //string으로 태그 번호를 받아서 
            tagNum = j1["tagNo"]["tagNo"].ToString();


            //1번 진행중 + 3일이 남았으면 마감 임박 태그가 뜬다. 
            if(tagNum == "1" &&resultDay >= 1 && resultDay <= 3)
            {
                GameObject g1 = Instantiate(preDI, go.transform.GetChild(5));

            }
            if(tagNum == "1" && resultDay < 1)
            {
                Destroy(go);
            }
            //2번 완료
            if (tagNum == "2")
            {
                Destroy(go);
                //inputTitle.interactable = false;

                //btn.interactable = false;
                //Button s = go.GetComponent<Button>();
                //s.interactable = false;
            }

            //3번 이슈
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

        //post/1, get, 완료되었을 때 호출되는 함수
        requesterLYD.url = $@"http://43.201.58.81:8088/workTime?memberNo=" + memeNum;
        requesterLYD.requestTypeLYD = RequestTypeLYD.GET;
        requesterLYD.onComplete = OnCompleteGetWorkTime;

        HttpManagerLYD.instance.SendRequestLYD(requesterLYD);


    }

    public void OnCompleteGetWorkTime(DownloadHandler handler)
    {
        JObject jobject = JObject.Parse(handler.text);
        print("ㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑㅑ " + jobject.ToString());
    }



}
