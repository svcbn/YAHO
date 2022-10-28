using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

//1. 트렐로 버튼을 눌렀을 때 트렐로처럼 화면 구성이 되어 있어야한다. (코루틴 함수로 만들고 for문ㄷ 돌리기)
//2. +add a card 버튼을 눌렀을 때 우선 trelloManager프리팹에 trelloCanvas가 켜진다.(setActive = true)
//3. send버튼을 누르면 트렐로 화면에서 카드가 생성된다. (현재 샌드버튼을 누르면 데이터값이 들어가고, 바로 이미지가 꺼져버림. 수정!)
//4. x버튼을 누르면 다시 트렐로 화면이 나온다. ->
//5. 트렐로 화면에서 카드의 연필 이미지를 클릭했을 때 이동이 된다. (액션 추가)


//1, string[] 변수 선언해서 출력되는 값을 저장해준다. 0,1,2 -> 첫번째 카드꺼. 가지고 있을 거면 
//string sum;
//string a;
//stirng b;
//a = "예담아"
//b = " 공부하자"
//sum = a + b
//print(sum)
//2. 


public class GetTest : MonoBehaviour
{
    //딕셔너리 키를 id/ 값은 transform (content)
    //딕셔너리 쓰는법
    Dictionary<string, Transform> contentCardList = new Dictionary<string, Transform>();



    //1.필요한 string 변수들을 선언
    public string boardid = "63589621f8210a014e470160";
    //public string idList = "63589621f8210a014e470168";
    public string APIKey = "54387b62ebcd333434d78ebc023baefc";
    public string APIToken = "ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844";

    List<string> idLists = new List<string>(); //여기에다가 url2번의 id값을 받아줘야한다. 

    string boardtitle;
    string desc;
    string id;
    string cardtitle;

    //프리팹으로 만들어주기!
    public GameObject boardid_text; 
    public GameObject cardid_text; 
    public GameObject descText;

    //컨텐츠 넣을 수 있도록
    public Transform boardList_content;
    public Transform cardid_content;

    //저장할 값 선언
    List<string> saveData = new List<string>();
    List<string> saveDataBoardidList = new List<string>();

    //list생성해서 TO DO/ DOING/ DONE의 ID를 기준으로 어디에 생성할 지 구분을 해준다.
    //List<string> boardidSeparation = new List<string>();
    Dictionary<string, List<string>> boardSeperation = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        
        //PUT / 1 / cards /635a08e900b3d1048058223a?idList =63589621f8210a014e470168

        //'https://api.trello.com/1/boards/63589621f8210a014e470160/cards?key=54387b62ebcd333434d78ebc023baefc&token=ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844'
        //https://api.trello.com/1/organizations/63579508506b3b00bcbf7707/boards?key=54387b62ebcd333434d78ebc023baefc&token=ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844
        //연동할 api의 url을 작성해준다. 

        //string url1 = string.Format("https://api.trello.com/boards/{0}?labels=all&key={1}&token={2}", boardid, APIKey, APIToken);
        //StartCoroutine(GetCardData(url1));

        string url2 = string.Format("https://api.trello.com/1/boards/{0}/lists?key={1}&token={2}", boardid, APIKey, APIToken);
        //-> To do, Doing, Done이 출력된다.
        StartCoroutine(GetCardData(url2));
        //여기서 나오는 아이디값 (url2에서 나오는 id값을 idLists에 넣어줘야한다. ) 


    }




    //OnBtnTrello버튼을 클릭했을 때 트렐로 idBoard값 & idList들이 출력되야한다. 
    //버튼을 클릭했을 때 코루틴 호출 및 데이터값도 저장되서 화면에 띄어져야한다. -> 로드만하게 
    //saveData는 
    /*public void OnBtnTrello()
    {


        

        BoardName();
        CardName();


    }

    //url2는 name만 보이게 하고
    public void BoardName()
    {
        nameText_board.text = name;

    }

    //url3은 name, desc만 보이게 한다.
    public void CardName()
    {
        nameText_card.text = name;
        descText.text = desc;
    }*/


    //boardid를 넣어 To Do / Doing/ Done 출력 및 id 값 idList에 넣어주기.
    IEnumerator GetCardData(string url)
    {
        //url의 값을 얻어온다.
        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SetRequestHeader("Accept", "application / json");

        yield return req.SendWebRequest(); //SendWebRequest 답장이 올 때까지 가만히 있는데, 코루틴을 사용하면 그동안 다른일을 한다.
        //코루틴으로 안하면 프로그램이 멈춘것처럼 보일 것임!

        //
        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"에러: {req.error}");
        }

        //값 받아오는것이 성공하면
        if (req.result == UnityWebRequest.Result.Success)
        {
            //for(int i = 0; i < idLists.Length; i++)
            //{

            //데이터값을 다 가져온다.
            string readData = req.downloadHandler.text;
            print($"읽은 값: {readData}");

            //제이슨배열 안에 제이슨이 있을 때  원하는 값만 가져올 수 있도록 만들어준다. 
            JArray jRead = JArray.Parse(readData);
            foreach (JObject jtoken in jRead)
            {
                //
                boardtitle = jtoken.GetValue("name").ToString();
              //  print($"Json 파싱한 결과 1: {boardtitle}");
                //nameText.text = name;

                //desc를 가져와서 JToken 변수에 넣어준다.
                JToken exist = jtoken.GetValue("desc");
                //만약 값이 있다면 desc값을 넣어준다. 
                if (exist != null)
                {
                    desc = exist.ToString();
                  //  print($"Json 파싱한 결과 2: {desc}");

                }

                //
                //jtoken.GetValue("id");
                id = jtoken.GetValue("id").ToString();
                //print($"Json 파싱한 결과 3: {id}");

                //아이디리스트 값을 저장한다. -> id값을 idLists에 넣어주는것!
                saveDataBoardidList.Add(boardtitle + "," + id);
                Debug.LogWarning(saveDataBoardidList.Count);
                Debug.LogWarning(saveDataBoardidList[0]);
                print(boardtitle + "," + id);


                //-> id값이 저장됨 

                //새로만든 코루틴함수에다가 실행
                /*for(int i = 0; i < idLists.Count; i++)
                {
                    idLists[i] = id[i].ToString();
                    print($" 결과 : {idLists[i]}");
                    *//*string url3 = string.Format("https://api.trello.com/1/lists/{0}/cards?key={1}&token={2}", idLists[i], APIKey, APIToken);
                    //카드리스트 중에 리스트들만 나옴 ex. "To Do" 에 적힌 "name"과  "desc"만)
                    StartCoroutine(GetCardData(url3));*//*

                }*/
                //데이터저장할때 
                //saveData.Add(name+ "," + desc + "," + id);
                //print(name + "," + desc + "," + id);
            }
            //}
            //byte[] downData = req.downloadHandler.text;





            //TrelloCardJson jsonRead = JsonConvert.DeserializeObject<TrelloCardJson>(readData);



        }
        // 코루틴이 끝나고 함수를 실행시키기 위해서는 코루틴 마지막 부분에 넣는다. *
        SaveDataBoardidList();

        for (int i = 0; i < idLists.Count; i++)
        {
            //idLists[i] = id[i].ToString();
            //print("1");
            string url3 = string.Format("https://api.trello.com/1/lists/{0}/cards?key={1}&token={2}", idLists[i], APIKey, APIToken);
            //카드리스트 중에 리스트들만 나옴 ex. "To Do" 에 적힌 "name"과  "desc"만)
            
            //코루틴 안에 코루틴 실행방법!
            yield return StartCoroutine(GetidListData(url3, idLists[i])); //앞에다가  yield return 안써주고 코루틴 실행한다는 의미는 언제가 완료되는지를 모름. 
            //앞에서는 코드가 순서대로 실행되는데 yield return써주면 실행 순서를 제어하는것 

        }



    }

    //1. boardid에서 저장한 값(To Do / Doing/ Done)에서 name 과 id를 나눈다.
    //2. id를 idLists에 다시 넣어준다. 
    //3. 여기에서 나온 name은 텍스트로 화면에 띄어준다. 
    public void SaveDataBoardidList()
    {
        for(int i = 0; i < saveDataBoardidList.Count; i++)
        {
            //1. boardid에서 저장한 값(To Do / Doing/ Done)에서 name 과 id를 나눈다.
            Debug.LogWarning(saveDataBoardidList[i] + "하하핳" + i);
            string[] b = saveDataBoardidList[i].Split(",");
            
            boardtitle = b[0];

            GameObject nameText_b = Instantiate(boardid_text, boardList_content); //, 찍고 뒤에 적어주면 부모 밑으로 생성됨
            //print(boardtitle);
            nameText_b.GetComponentInChildren<Text>().text = boardtitle;
            id = b[1];
            //카드의 부모가 될 컨텐츠의 transform을 저장
            contentCardList[id] = nameText_b.transform.Find("Scroll View/Viewport/Content1"); //자식의 자식의 자식을 찾는함수
            print($"읽고싶은 값 : {contentCardList[id]} id = {id}");
            //2. id를 idLists에 다시 넣어준다. -> 코루틴 GetidListData가 실행할 수 있도록
            idLists.Add(id);
            //boardSeperation[id] = new List<string>();
            //print(id);
        }
    }

   

//idList를 얻어온 것으로 To Do / Doing/ Done(boardId값에서 나온 id를 출력해주기!) -> url2값에서 나온 idList넣어 준 것을 이름, desc, id? 출력
    IEnumerator GetidListData(string url3, string Bid)
    {
        //url의 값을 얻어온다.
        UnityWebRequest req = UnityWebRequest.Get(url3);
        req.SetRequestHeader("Accept", "application / json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"에러: {req.error}");
        }

        //값 받아오는것이 성공하면
        if (req.result == UnityWebRequest.Result.Success)
        {
            //데이터값을 다 가져온다.
            string readData = req.downloadHandler.text;
            print($"읽은 값: {readData}");

            //제이슨배열 안에 제이슨이 있을 때  원하는 값만 가져올 수 있도록 만들어준다. 
            JArray jRead = JArray.Parse(readData);
            foreach (JObject jtoken in jRead)
            {
                //이름 가져오고
                cardtitle = jtoken.GetValue("name").ToString();
                print($"Json 파싱한 결과 1: {cardtitle}");
                //nameText.text = name;

                //desc를 가져와서 JToken 변수에 넣어준다.
                JToken exist = jtoken.GetValue("desc");
                //만약 값이 있다면 desc값을 넣어준다. 
                if (exist != null)
                {
                    desc = exist.ToString();
                   // print($"Json 파싱한 결과 2: {desc}");

                }

                //
                jtoken.GetValue("id");
                id = jtoken.GetValue("id").ToString();
               // print($"Json 파싱한 결과 3: {id}");

                //cardtitle와 desc의 값 저장
                saveData.Add(cardtitle + "," + desc + "," +id + "," + Bid);
                print(cardtitle + "," + desc + "," + id + Bid);

                //


            }


        }
        SaveData();
    }

    //버튼을 클릭했을 때 버튼마다 있는

    public void SaveData()
    {
        for (int i = 0; i < saveData.Count; i++)
        {
            //get할 때 키 값으로 불러오는데, 한 묶음에서 get 할 때 분리해서 얻어오는거지 
            string[] c = saveData[i].Split(",");
            cardtitle = c[0];
            //print(cardtitle);


            //보드에 따라 생성하도록 바꿔줘야함. 
            print(contentCardList[c[3]]);
            GameObject nameText_b = Instantiate(cardid_text, contentCardList[c[3]]); //, 찍고 뒤에 적어주면 부모 밑으로 생성됨
            nameText_b.GetComponentInChildren<Text>().text = cardtitle;

            id = c[2];
            desc = c[1];
           // print(desc);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    
}

