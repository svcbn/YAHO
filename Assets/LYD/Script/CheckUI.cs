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
    //�� ��ư�� ������ üũ�� ��
    public void OnCheck()
    {
        f.sprite = frame32;
        checkObject.GetComponent<Button>().interactable = false;
        b.interactable = false;
        //��ư interctable ���ֱ� 
        GameObject gi = GameObject.Find("HttpUIManager");
        HttpUIManagerLYD hui = gi.GetComponent<HttpUIManagerLYD>();
        hui.PutCheck(check);//ǲ�Լ�
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
