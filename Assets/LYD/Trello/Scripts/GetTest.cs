using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.Linq;

//1. Ʈ���� ��ư�� ������ �� Ʈ����ó�� ȭ�� ������ �Ǿ� �־���Ѵ�. (�ڷ�ƾ �Լ��� ����� for���� ������)
//2. +add a card ��ư�� ������ �� �켱 trelloManager�����տ� trelloCanvas�� ������.(setActive = true)
//3. send��ư�� ������ Ʈ���� ȭ�鿡�� ī�尡 �����ȴ�. (���� �����ư�� ������ �����Ͱ��� ����, �ٷ� �̹����� ��������. ����!)
//4. x��ư�� ������ �ٽ� Ʈ���� ȭ���� ���´�. ->
//5. Ʈ���� ȭ�鿡�� ī���� ���� �̹����� Ŭ������ �� �̵��� �ȴ�. (�׼� �߰�)


//1, string[] ���� �����ؼ� ��µǴ� ���� �������ش�. 0,1,2 -> ù��° ī�岨. ������ ���� �Ÿ� 
//string sum;
//string a;
//stirng b;
//a = "�����"
//b = " ��������"
//sum = a + b
//print(sum)
//2. 


public class GetTest : MonoBehaviour
{

    public GameObject scrollview_Boardlist;
    public GameObject btn_trello;
    public GameObject btn_trelloX;

    //��ųʸ� Ű�� id/ ���� transform (content)
    //��ųʸ� ���¹�
    //list�����ؼ� TO DO/ DOING/ DONE�� ID�� �������� ��� ������ �� ������ ���ش�.
    Dictionary<string, Transform> contentCardList = new Dictionary<string, Transform>();



    //1.�ʿ��� string �������� ����
    public string boardid = "63589621f8210a014e470160";
    //public string idList = "63589621f8210a014e470168";
    public string APIKey = "54387b62ebcd333434d78ebc023baefc";
    public string APIToken = "ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844";

    List<string> idLists = new List<string>(); //���⿡�ٰ� url2���� id���� �޾�����Ѵ�. -> TO DO/ DOING/ DONE

    string boardtitle;
    string desc;
    string b_id;
    string c_id;
    string cardtitle;

    //���������� ������ֱ�!
    public GameObject boardid_text;
    public GameObject cardid_text;

    //desc ȭ�� �ִ� ����
    public Desc description;

    public GameObject descDisplay;

    //������ ���� �� �ֵ���
    public Transform boardList_content;
    public Transform cardid_content;

    //������ �� ����
    List<string> saveData = new List<string>();
    List<string> saveDataBoardidList = new List<string>();

    //List<string> boardidSeparation = new List<string>();
    // Dictionary<string, List<string>> boardSeperation = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {


        //PUT / 1 / cards /635a08e900b3d1048058223a?idList =63589621f8210a014e470168

        //'https://api.trello.com/1/boards/63589621f8210a014e470160/cards?key=54387b62ebcd333434d78ebc023baefc&token=ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844'
        //https://api.trello.com/1/organizations/63579508506b3b00bcbf7707/boards?key=54387b62ebcd333434d78ebc023baefc&token=ca7703272e711a73476264962b1c983cbc8734de0c4237912e52fe148c9b9844
        //������ api�� url�� �ۼ����ش�. 

        //string url1 = string.Format("https://api.trello.com/boards/{0}?labels=all&key={1}&token={2}", boardid, APIKey, APIToken);
        //StartCoroutine(GetCardData(url1));


        /*string url2 = string.Format("https://api.trello.com/1/boards/{0}/lists?key={1}&token={2}", boardid, APIKey, APIToken);
        //-> To do, Doing, Done�� ��µȴ�.
        StartCoroutine(GetCardData(url2));*/
        //���⼭ ������ ���̵� (url2���� ������ id���� idLists�� �־�����Ѵ�. ) 

    }




    //OnBtnTrello��ư�� Ŭ������ �� Ʈ���� idBoard�� & idList���� ��µǾ��Ѵ�. 
    //��ư�� Ŭ������ �� �ڷ�ƾ ȣ�� �� �����Ͱ��� ����Ǽ� ȭ�鿡 ��������Ѵ�. -> �ε常�ϰ� 
    // ��, Ʈ���ο� �ִ� ȭ�鱸������ ȭ�鿡 �Ȱ��� ��������ֵ��� 
    public void OnBtnTrello()
    {
        
        //ȭ�鱸�� on
        scrollview_Boardlist.SetActive(true);
        //������ư on
        btn_trelloX.SetActive(true);


        string url2 = string.Format("https://api.trello.com/1/boards/{0}/lists?key={1}&token={2}", boardid, APIKey, APIToken);
        //-> To do, Doing, Done�� ��µȴ�.
        StartCoroutine(GetCardData(url2));
        //���⼭ ������ ���̵� (url2���� ������ id���� idLists�� �־�����Ѵ�. )
        //trello�� ��ư�̹����� �ڽ��� text�� �����Ѵ�.
        btn_trello.GetComponent<Image>().enabled = false;
        btn_trello.GetComponentInChildren<Text>().enabled = false;

    }

    //�� ��ư�� ������ scrollview�� �����ؼ� ȭ�鿡 Ʈ���� ȭ���� ������.
    public void TrelloXBtn()
    {
        scrollview_Boardlist.SetActive(false);
        btn_trello.GetComponent<Image>().enabled = true;
        btn_trello.GetComponentInChildren<Text>().enabled = true;
        btn_trelloX.SetActive(false);

        //�����ʱ�ȭ
        b_id = "";
        boardtitle = "";
        cardtitle = "";
        desc = "";
        c_id = "";

        //2. contentã�Ƽ� �ؿ� �ڽĵ� �� ����
        Transform[] boardContent = boardList_content.GetComponentsInChildren<Transform>();
        if (boardContent != null)
        {
            for (int i = 1; i < boardContent.Length; i++)
            {
                if (boardContent[i] != transform)
                    Destroy(boardContent[i].gameObject);


            }
        }

        //3. list �� �ʱ�ȭ
        idLists.Clear();
        saveDataBoardidList.Clear();
        saveData.Clear();
        contentCardList = new Dictionary<string, Transform>();
    }

    //boardid�� �־� To Do / Doing/ Done ��� �� id �� idList�� �־��ֱ�.
    IEnumerator GetCardData(string url)
    {
        //url�� ���� ���´�.
        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SetRequestHeader("Accept", "application / json");

        yield return req.SendWebRequest(); //SendWebRequest ������ �� ������ ������ �ִµ�, �ڷ�ƾ�� ����ϸ� �׵��� �ٸ����� �Ѵ�.
        //�ڷ�ƾ���� ���ϸ� ���α׷��� �����ó�� ���� ����!

        //
        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"����: {req.error}");
        }

        //�� �޾ƿ��°��� �����ϸ� (���� �����Ҷ� �ް�)
        if (req.result == UnityWebRequest.Result.Success)
        {
            //for(int i = 0; i < idLists.Length; i++)
            //{

            //�����Ͱ��� �� �����´�.
            string readData = req.downloadHandler.text;
            print($"���� ��: {readData}");

            //���̽��迭 �ȿ� ���̽��� ���� ��  ���ϴ� ���� ������ �� �ֵ��� ������ش�. 
            JArray jRead = JArray.Parse(readData);
            foreach (JObject jtoken in jRead)
            {
                //
                boardtitle = jtoken.GetValue("name").ToString();
                //  print($"Json �Ľ��� ��� 1: {boardtitle}");
                //nameText.text = name;

                //desc�� �����ͼ� JToken ������ �־��ش�.
                JToken exist = jtoken.GetValue("desc");
                //���� ���� �ִٸ� desc���� �־��ش�. 
                if (exist != null)
                {
                    desc = exist.ToString();
                    //  print($"Json �Ľ��� ��� 2: {desc}");

                }

                //
                //jtoken.GetValue("id");
                b_id = jtoken.GetValue("id").ToString();
                print($"Json b_id �Ľ��� ��� 3: {b_id}");

                //���̵𸮽�Ʈ ���� �����Ѵ�. -> id���� idLists�� �־��ִ°�!
                saveDataBoardidList.Add(boardtitle + "," + b_id); //��� ���� ���ص���.
                //Debug.LogWarning(saveDataBoardidList.Count);
                //Debug.LogWarning(saveDataBoardidList[0]);
                print(boardtitle + "," + b_id);


                //-> id���� ����� 
            }

            //TrelloCardJson jsonRead = JsonConvert.DeserializeObject<TrelloCardJson>(readData);
        }
        // �ڷ�ƾ�� ������ �Լ��� �����Ű�� ���ؼ��� �ڷ�ƾ ������ �κп� �ִ´�. *
        SaveDataBoardidList();

        for (int i = 0; i < idLists.Count; i++)
        {
            //idLists[i] = id[i].ToString();
            //print("1");
            string url3 = string.Format("https://api.trello.com/1/lists/{0}/cards?key={1}&token={2}", idLists[i], APIKey, APIToken);
            //ī�帮��Ʈ �߿� ����Ʈ�鸸 ���� ex. "To Do" �� ���� "name"��  "desc"��)

            //�ڷ�ƾ �ȿ� �ڷ�ƾ ������!
            yield return StartCoroutine(GetidListData(url3, idLists[i])); //�տ��ٰ�  yield return �Ƚ��ְ� �ڷ�ƾ �����Ѵٴ� �ǹ̴� ������ �Ϸ�Ǵ����� ��. 
            //�տ����� �ڵ尡 ������� ����Ǵµ� yield return���ָ� ���� ������ �����ϴ°� 
            print($"idList�� �ݺ�Ƚ�� : {idLists[i]}");

        }
    }

    //1. boardid���� ������ ��(To Do / Doing/ Done)���� name �� id�� ������.
    //2. id�� idLists�� �ٽ� �־��ش�. 
    //3. ���⿡�� ���� name�� �ؽ�Ʈ�� ȭ�鿡 ����ش�. 
    private void SaveDataBoardidList()
    {
        for (int i = 0; i < saveDataBoardidList.Count; i++)
        {
            //1. boardid���� ������ ��(To Do / Doing/ Done)���� name �� id�� ������.
            //Debug.LogWarning(saveDataBoardidList[i] + "�����K" + i);
            string[] b = saveDataBoardidList[i].Split(",");

            boardtitle = b[0];

            GameObject nameText_b = Instantiate(boardid_text, boardList_content); //, ��� �ڿ� �����ָ� �θ� ������ ������
            //print(boardtitle);
            nameText_b.GetComponentInChildren<Text>().text = boardtitle;

            b_id = b[1];
            //ī���� �θ� �� �������� transform�� ����
            contentCardList[b_id] = nameText_b.transform.Find("Scroll View/Viewport/Content1"); //�ڽ��� �ڽ��� �ڽ��� ã���Լ�
            print($"�а���� �� : {contentCardList[b_id]} id = {b_id}");
            //2. id�� idLists�� �ٽ� �־��ش�. -> �ڷ�ƾ GetidListData�� ������ �� �ֵ���
            idLists.Add(b_id);
            print($"b_id�� id�� : {b_id}");
            //boardSeperation[id] = new List<string>();
            //print(id);
        }
    }



    //idList�� ���� ������ To Do / Doing/ Done(boardId������ ���� id�� ������ֱ�!) -> url2������ ���� idList�־� �� ���� �̸�, desc, id? ���
    IEnumerator GetidListData(string url3, string Bid)
    {
        //url�� ���� ���´�.
        UnityWebRequest req = UnityWebRequest.Get(url3);
        req.SetRequestHeader("Accept", "application / json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"����: {req.error}");
        }

        //�� �޾ƿ��°��� �����ϸ�
        if (req.result == UnityWebRequest.Result.Success)
        {
            //�����Ͱ��� �� �����´�.
            string readData = req.downloadHandler.text;
            print($"���� ��: {readData}");

            Desc description = cardid_text.GetComponent<Desc>();

            //���̽��迭 �ȿ� ���̽��� ���� ��  ���ϴ� ���� ������ �� �ֵ��� ������ش�. 
            JArray jRead = JArray.Parse(readData);
            foreach (JObject jtoken in jRead)
            {
                //�̸� ��������
                cardtitle = jtoken.GetValue("name").ToString();
                print($"Json cardtitle �Ľ��� ��� 1: {cardtitle}");
                //nameText.text = name;

                //desc�� �����ͼ� JToken ������ �־��ش�.
                JToken exist = jtoken.GetValue("desc");
                //���� ���� �ִٸ� desc���� �־��ش�. 
                if (exist != null)
                {
                    desc = exist.ToString();
                    // print($"Json �Ľ��� ��� 2: {desc}");
                    description.desc = desc;

                }

                //
                jtoken.GetValue("id");
                c_id = jtoken.GetValue("id").ToString();
                print($"Json c_id �Ľ��� ��� 3: {c_id}");

                //cardtitle�� desc�� �� ���� //����Ʈ�� �� �־����� ���°�.
                saveData.Add(cardtitle + "," + desc + "," + c_id + "," + Bid); //clear 
                print(cardtitle + "," + desc + "," + c_id + Bid);

                //


            }


        }
        SaveData();
    }

    //��ư�� Ŭ������ �� ��ư���� �ִ�

    private void SaveData()
    {
        // idLists = idLists.Distinct().ToList();
        print("���嵥������ ��: " + saveData.Count);
        for (int i = 0; i < saveData.Count; i++)
        {
            print("��ųʸ��� ����: " + contentCardList.Values.Count);

            ////���������� �ϳ� �ּ� 
            ////get�� �� Ű ������ �ҷ����µ�, �� �������� get �� �� �и��ؼ� �����°��� 
            string[] c = saveData[i].Split(","); //�׷� ���Ⱑ ���� 
            //print($"saveData�� ���尪 : {saveData[i]}");
            cardtitle = c[0];
            //print(cardtitle);
            Debug.Log($"������ ���� ���� : {saveData.Count}");
            //���忡 ���� �����ϵ��� �ٲ������. 
            print(contentCardList[c[3]]); //�̰� �ι������ΰ� 3�� ������ �ǹ���... content1�� �� ���̵𰪿� 
            GameObject nameText_b = Instantiate(cardid_text, contentCardList[c[3]]); //, ��� �ڿ� �����ָ� �θ� ������ ������
            nameText_b.GetComponentInChildren<Text>().text = cardtitle;

            description = nameText_b.GetComponent<Desc>();
            ////c = saveData.Distinct().ToArray();
            desc = c[1];
            description.Set(cardtitle, desc, descDisplay);
            c_id = c[2];

            ///*if (idLists.Contains(idLists[i])) continue;
            //idLists.Add(c[i].ToString());*/

            //// print(desc);
            /*string[] splitData = saveData[i].Split(',');
            GameObject go = Instantiate(cardid_text, contentCardList[splitData[3]]);
            go.transform.GetChild(0).GetComponent<Text>().text = splitData[0];*/
        }

        saveData.Clear(); //���ھƾƾƾƾ�

        
    }

    //1.send��ư�� ������ �� ��ư�� ������ �˸� 
    //2.�������� �� �ʱ�ȭ���ش�. (�������� -> json �޴� �� & list�� ���ִ°���)
    //3.ȭ�鿡 ������ ������ �͵� �����ֱ� -> cardid �����ո� �������� ��.
    //4. �ٽ� json���� ������ �� �ֵ��� �����. -> ���� boardid���� �и� �ߴٸ� cardid���� �����ü��ֵ���
    //5. �Ұ��� �ٽ� false�� ������ֱ�

    //�� �Լ��� ���ο� ȭ�鱸��(add a card ��ư ������, �� �ۼ� �� , send ��ư ������ ����)�� ���ִ� �Լ�
    public void OnNewCard()
    {
        /*GameObject trelloM = GameObject.Find("TrelloManager");
        TrelloUI trelloUi = trelloM.GetComponent<TrelloUI>();
        */
        //if (trelloUi.newValue == true)
        //{
        //2.�������� �� �ʱ�ȭ���ش�. (�������� -> json �޴� �� & list�� ���ִ°���)
        cardtitle = "";
        desc = "";
        c_id = "";

        //�±� �޾��༭ ����
        GameObject[] cardidPrefab = GameObject.FindGameObjectsWithTag("Cardid");
        //������ ����
        for (int i = 0; i < cardidPrefab.Length; i++)
        {
            Destroy(cardidPrefab[i]);
        }

        //����Ʈ �ʱ�ȭ (�ణ �ɸ��°���  bid�� �ʱ�ȭ�� �ǹ����� ������... �̰� ã������������ �𸣁���/ �켱 �غ���)
        saveData.Clear();

        for (int i = 0; i < idLists.Count; i++)
        {
            //idLists[i] = id[i].ToString();
            //print("1");
            string url3 = string.Format("https://api.trello.com/1/lists/{0}/cards?key={1}&token={2}", idLists[i], APIKey, APIToken);
            //ī�帮��Ʈ �߿� ����Ʈ�鸸 ���� ex. "To Do" �� ���� "name"��  "desc"��)

            //�ڷ�ƾ �ȿ� �ڷ�ƾ ������!
            //yield return
            StartCoroutine(GetidListData(url3, idLists[i]));
            //�տ����� �ڵ尡 ������� ����Ǵµ� yield return���ָ� ���� ������ �����ϴ°� 

        }
        //}
    }


    //cardid �� ��ư�� Ŭ���ϸ� DescDisplay�̹����� �����Ѵ�.
    //CardNameText���� cardTitle���� ���ߵȴ�. (���������ؼ� �־��ְ�)
    //DescText���� desc ���� ���ߵȴ�. 
    //���� �߿��Ѱ��� �� cardTitle���� �´� desc�� �־���ߵȴ�. (�и��� ������Ѵٰ� ����)
    /*public void OnDescDisplay()
    {
        descDisplay.SetActive(true);
        //ī���ϳ��ϳ� ���������� ���� 
    }
*/
    public void DescDisplayXBtn()
    {
        descDisplay.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

    }



}

