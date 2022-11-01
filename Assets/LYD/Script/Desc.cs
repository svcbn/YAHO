using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desc : MonoBehaviour
{
     //desc ȭ�� �ִ� ����
    GameObject descDisplay;

    

    public string cardTitle;
    public string desc;
    // string c_id;

    

    Text cardNameText;
    Text descText;

    //getTest���� string cardTitle, desc������ �Լ�
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

    //cardid �� ��ư�� Ŭ���ϸ� DescDisplay�̹����� �����Ѵ�.
    //CardNameText���� cardTitle���� ���ߵȴ�. (���������ؼ� �־��ְ�)
    //DescText���� desc ���� ���ߵȴ�. 
    //���� �߿��Ѱ��� �� cardTitle���� �´� desc�� �־���ߵȴ�. (�и��� ������Ѵٰ� ����)
    public void OnDescDisplay()
    {
        descDisplay.SetActive(true);
        //ī���ϳ��ϳ� ���������� ���� 

        cardNameText.text = cardTitle;
        descText.text = desc;

    }

   
    // Start is called before the first frame update
    void Start()
    {
        //�ٲٰ� ���� �ؽ�Ʈ�� �������ش�.
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
