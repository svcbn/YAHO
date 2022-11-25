using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;
using System;

public class MeetingHistoryData
{
    public string date;
    public List<string> keyword;
    public List<string> summary;
    public string transcripts;
    public List<string> teamMember; //아이디, 멤버이름이랑 시퀀스번호
    //?팀원
    //이미지는 어떻게 받을지
    public string graph1;
}

public enum MeetingRequestType
{
    POST,
    GET
}
public class MeetingGetTest : MonoBehaviour
{

    public GameObject mi;
    public Button x;

    public GameObject teamMembertext;
    public GameObject summarytext;
    public GameObject keywordText;

    public Transform teamMemberContent;
    public Transform summaryContent;
    public Transform keywordContent;

    public Text date;
    public Text transcripts;

    public Image image;
    public byte[] byteTexture;

    public GameObject meeting;
    public GameObject meetingObject;
    public Transform meetingContent;

    public int index;
    //미팅버튼을 누르면 미팅이 뜬다. 
    //프로젝트별 리스트가 뜰 수 있도록 조회한다. 
    public void OnMeeting()
    {
        mi = GameObject.Find("MonitorCanvas").transform.GetChild(8).gameObject;
        mi.SetActive(true);

        //meeting.SetActive(true);
        GetMeeting();
        GetMeetingMember();
    }
    public string a; 
    
    
    void GetMeetingMember()
    {
        GameObject s = GameObject.Find("HttpUIManager");
        HttpUIManagerLYD h = s.GetComponent<HttpUIManagerLYD>();
        List<int> a1 = h.mmmNum;
        string meme  = "";
        for(int i = 0; i < a1.Count; i++)
        {
            meme += a1[i].ToString();

            if(i < a1.Count -1)
            {
                meme += ",";
            }
        }

        string url = string.Format("http://43.201.58.81:8088/members/name/"+meme);
        StartCoroutine(GetMeetingMemberData(url));
    }
    IEnumerator GetMeetingMemberData(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();


        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"에러:{req.error}");
        }

        if (req.result == UnityWebRequest.Result.Success)
        {
            string readData = req.downloadHandler.text;
            print($"wwwwwwwwwwwwwwwwwwwwwww읽은 값: {readData}");

            JObject jObject = JObject.Parse(readData);

            string meme = "";

            print(jObject.ToString());
            JToken jk = jObject["data"];
            foreach(JToken j1 in jk)
            {
                //              meme += j1.ToString();
                meme += j1.ToString() + ",";
               print("11111111111111 : " + meme);
           }

            mi.transform.GetChild(9).GetComponent<Text>().text = meme.Substring(0, meme.Length - 1);
            //meme.length -1 로 substring잘라주기
            //print("111111111" + );
           
            if(meme.Length > 18)
            {
                string s = meme.Substring(0, 18);
                meme = s + "...";
            }
                

                
            

        }
    }

    //미팅에 미팅목록 리스트가 나오도록 만든다. 
    void GetMeeting()
    {
        string url = string.Format("http://43.201.58.81:8088/meetings/"+a);
        StartCoroutine(GetMeetingData(url));
    }

    IEnumerator GetMeetingData(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            print($"에러:{req.error}");
        }

        if (req.result == UnityWebRequest.Result.Success)
        {
            string readData = req.downloadHandler.text;
            print($"읽은 값: {readData}");



            JObject jObject = JObject.Parse(readData);
            print(jObject.ToString());

            JToken jk = jObject["data"];
            string ww = jk["imageURL"].ToString();
            StartCoroutine(GetImage(ww));
            
            mi.transform.GetChild(1).GetComponent<Text>().text = jk["meetingStartTime"].ToString().Substring(0 ,18);
            mi.transform.GetChild(2).GetComponent<Text>().text = jk["meetingEndTime"].ToString().Substring(11, 8);
           // string s = mi.transform.GetChild(2).GetComponent<Text>().text.Substring(11, 8);
           // print(s);
            // print(jk["meetingEndTime"].ToString());
            mi.transform.Find("Scroll View_transcript/Viewport/Content/Text (Legacy)").GetComponent<Text>().text = jk["record"].ToString();

            JToken js = jk["keyword"];
            foreach(JToken j1 in js)
            {
                 print(j1.ToString());
                GameObject go = GameObject.Instantiate(keywordText, mi.transform.GetChild(7).GetChild(0).GetChild(0));
                go.GetComponent<Text>().text = j1.ToString();


            }

            JToken su = jk["summary"];
            foreach(JToken j1 in su)
            {
                print(j1.ToString());
                GameObject go = GameObject.Instantiate(summarytext, mi.transform.GetChild(6).GetChild(0).GetChild(0));
                go.GetComponent<Text>().text = j1.ToString();
            }
            // var jsonP = jObject[""];
            //print("1111111" + jsonP);
            x = mi.transform.GetChild(5).GetComponent<Button>();
            //print(x);
            x.onClick.AddListener(OnBtnX);
        }

    }

    IEnumerator GetImage(string url)
    {
        UnityWebRequest ww = UnityWebRequestTexture.GetTexture(url);
        yield return ww.SendWebRequest();


        if(ww.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(ww.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)ww.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            mi.transform.GetChild(4).GetComponent<Image>().sprite = sprite;
        }
    }

    //엑스 버튼을 누르면 mi가 꺼진다. 
   public void OnBtnX()
   {
        mi.SetActive(false);
   }
    // Start is called before the first frame update
    void Start()
    {

    }


        //아얘하나의 함수안에 다 넣어서 스크립트 하나 더 생성해서 보이게 하는 방법도 있음. 


        // Update is called once per frame
        void Update()
        {

        }

        #region AI연동
        //이 버튼을 클릭했을 때 화면이 보이면서 같은 시간대에 조회가 요청된다.
        /*public void OnMeetingImage()
        {
            //조회요청
            string url = string.Format("http://15.165.47.243:9090/send");
            StartCoroutine(MeetingGetData(url));
        }


        IEnumerator MeetingGetData(string url)
        {
            //url값을 얻어온다.
            UnityWebRequest req = UnityWebRequest.Get(url);

            yield return req.SendWebRequest();

            if(req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                print($"에러:{req.error}");
            }


            if (req.result == UnityWebRequest.Result.Success)
            {
                string readData = req.downloadHandler.text;
                print($"읽은 값: {readData}");

                //MeetingHistoryData meetingHistoryData = JsonUtility.FromJson<MeetingHistoryData>(readData);

                JObject jObject = JObject.Parse(readData);
                print(jObject.ToString());

                date.text = jObject["date"].ToString();

                transcripts.text = jObject["text"].ToString();
                print(date.text);
                print(transcripts.text);

                //리스트형식
                JToken member1 = jObject["members"];
                foreach (JToken x in member1)
                {
                    // 1. 프리팹 생성
                    // teamMembertext
                    GameObject go = GameObject.Instantiate(teamMembertext, teamMemberContent);
                    // 2. teamMembertext.text = x.ToString
                    //  
                    //x.ToString();
                    go.GetComponent<Text>().text = x.ToString();
                    print(x.ToString());
                }

                JToken keyword1 = jObject["keyword"];
                foreach (JToken x in keyword1)
                {
                    GameObject go = GameObject.Instantiate(keywordText, keywordContent);
                    go.GetComponent<Text>().text = x.ToString();
                }




                //print("graph test : "+jObject["graph"].ToString());

          JToken summary = jObject["summary"];
                foreach (JToken x in summary)
                {
                    GameObject go = GameObject.Instantiate(summarytext, summaryContent);
                    go.GetComponent<Text>().text = x.ToString();
                   // print(x.ToString());
                }
                // keyword[i].text = jObject["Keyword"].ToString();

                //byte[] graphBytes = JsonConvert.f
                //현지가 한 부분 따라함

                JToken Jgraph = jObject["graph"];
                foreach(JToken x in Jgraph)
                {
                    print("Test graph : "+x.ToString());
                }
                byte[] imgByte =  Convert.FromBase64String(jObject["graph"].ToString());
                int width, height;
                //GetImageSize(image, out width, out height);
                //Texture2D texture2D = new Texture2D(64, 64, TextureFormat.PVRTC_RGBA4, false);
                Texture2D texture2D = new Texture2D(0, 0);
                texture2D.LoadImage(imgByte);
                //texture2D.LoadRawTextureData(imgByte);
                //texture2D.Apply();
                //Base64ToTexture2D(imgByte);
                Rect rect = new Rect(0, 0, texture2D.width, texture2D.height);
                image.sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
    *//*

                Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
                tex.LoadImage(image);
                tex.Apply();*//*

                // byte[] bArray = Image.getr

            }

        }


        private Texture2D Base64ToTexture2D(byte[] imageData)
        {

            int width, height;
            GetImageSize(imageData, out width, out height);

            // 매프레임 new를 해줄경우 메모리 문제 발생 -> 멤버 변수로 변경
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);

            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(imageData);
            texture.Apply();

            return texture;
        }
        private void GetImageSize(byte[] imageData, out int width, out int height)
        {
            width = ReadInt(imageData, 3 + 15);
            height = ReadInt(imageData, 3 + 15 + 2 + 2);
        }

        private int ReadInt(byte[] imageData, int offset)
        {
            return (imageData[offset] << 8 | imageData[offset + 1]);
        }
    */
        #endregion


        /*public Image GetImage(string base64String)
         {
             //convert Base64 string to byte[]
             byte[] imageByte = Convert.FromBase64String(base64String);
             MemoryStream mm = new MemoryStream(imageByte, 0, imageByte.Length);

             //convert byte[] to image
             mm.Write(imageByte, 0, imageByte.Length);
             image = Image.FromStream(mm, true);
             return image;*/



    }





