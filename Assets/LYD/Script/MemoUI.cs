using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//cardui 프리팹에 스크립트를 넣는다.
//버튼을 클릭했을 때 
public class MemoUI : MonoBehaviour
{
    GameObject descPlay;

   public InputField cardNameText;
   public InputField memoText;

    public string cardTitle;
    public string memo;

    public void Set(string s1, string s2)
    {
        cardTitle = s1;
        memo = s2;
    }

    public void Set1(GameObject go)
    {
        descPlay = go;

    }
    // Start is called before the first frame update
    void Start()
    {
       /* cardNameText = descPlay.transform.GetChild(0).GetComponent<InputField>();
        memoText = descPlay.transform.GetChild(1).GetComponent<InputField>();*/

    }

    public void OnDescDisplay()
    {
        descPlay.SetActive(true);
        //바꾸고 싶은 텍스트를 가져와준다.
        
        cardNameText.text = cardTitle;
        memoText.text = memo;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
