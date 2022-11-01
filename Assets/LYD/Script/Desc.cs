using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desc : MonoBehaviour
{
     //desc 화면 넣는 변수
    GameObject descDisplay;

    

    public string cardTitle;
    public string desc;
    // string c_id;

    

    Text cardNameText;
    Text descText;

    //getTest에서 string cardTitle, desc가져올 함수
    public void Set(string s1, string s2, GameObject go)
    {
        cardTitle = s1;
        desc = s2;
        descDisplay = go;
    }

   /* public void Getdisplay(GameObject go)
    {
        descDisplay = go;

    }*/

    //cardid 중 버튼을 클릭하면 DescDisplay이미지가 떠야한다.
    //CardNameText에는 cardTitle값이 들어가야된다. (변수선언해서 넣어주고)
    //DescText에는 desc 값이 들어가야된다. 
    //제일 중요한것은 그 cardTitle값에 맞는 desc를 넣어줘야된다. (분리를 해줘야한다고 생각)
    public void OnDescDisplay()
    {
        descDisplay.SetActive(true);
        //카드하나하나 지역변수로 만들어서 

        cardNameText.text = cardTitle;
        descText.text = desc;

    }

   
    // Start is called before the first frame update
    void Start()
    {
        //바꾸고 싶은 텍스트를 가져와준다.
        cardNameText = descDisplay.transform.GetChild(0).GetComponent<Text>();
        descText = descDisplay.transform.GetChild(2).GetComponent<Text>();

    }

   /* public void OffDescDisplay()
    {
        descDisplay.SetActive(false);

    }*/
    // Update is called once per frame
    void Update()
    {
        
    }
}
