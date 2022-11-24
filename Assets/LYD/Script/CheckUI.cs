using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckUI : MonoBehaviour
{
    public Sprite frame32;
    public GameObject checkObject;

    public Image f;
    public int check;
    public Button b;
    public Transform t;
    //이 버튼을 누르면 체크가 됨
    public void OnCheck()
    {
        f.sprite = frame32;
        checkObject.GetComponent<Button>().interactable = false;
        b.interactable = false;
        //버튼 interctable 꺼주기 
        GameObject gi = GameObject.Find("HttpUIManager");
        HttpUIManagerLYD hui = gi.GetComponent<HttpUIManagerLYD>();
        hui.PutCheck(check);//풋함수
        transform.SetSiblingIndex(t.childCount);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set(Image i1, Button b1, int i, Transform cc)
    {
        f = i1;
        b = b1;
        check = i;
        t = cc;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
