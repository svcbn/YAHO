using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//cardui �����տ� ��ũ��Ʈ�� �ִ´�.
//��ư�� Ŭ������ �� 
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
        //�ٲٰ� ���� �ؽ�Ʈ�� �������ش�.
        
        cardNameText.text = cardTitle;
        memoText.text = memo;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
