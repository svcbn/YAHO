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

    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    //이 버튼을 클릭했을 때 화면이 보이면서 같은 시간대에 조회가 요청된다.
    public void OnMeetingImage()
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
/*

            Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
            tex.LoadImage(image);
            tex.Apply();*/

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




