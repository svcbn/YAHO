using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreviewObject : MonoBehaviour
{
    public GameObject wall;
    public GameObject lamp;

    public GameObject previewWall;
    public GameObject previewLamp;

    GameObject preview;

    
   // public bool placeOb = true;
    // Start is called before the first frame update
    void Start()
    {
       // placeOb = true;
    }

    // Update is called once per frame
    void Update()
    {
        //���� placeOb�� Add �϶� true�̰�, remove�϶� false�� ������ش�. ������Ʈ�� ���̸� �ȵȴ�. 
        //������Ʈ�� ���콺�� ���� �����̴� ���� ���� ��ũ��Ʈ���� �����δ�. 
        //if(placeOb)
        //{
            Build.previewObject = preview;
            //���콺�� ��ġ�� ���� ���̸� ���.
            Vector3 mouse = Input.mousePosition;
            Ray casepoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(casepoint, out hit, Mathf.Infinity) && preview)
            {
                //������Ʈ�� ���콺�� ���� ������
                preview.transform.position = new Vector3(hit.point.x, hit.point.y + preview.transform.localScale.y / 2, hit.point.z);
            }


            if (Input.GetMouseButtonDown(1))
            {
                preview.transform.Rotate(0, 90, 0);
            }
        //}
        
    }
    
    public void Wall()
    {
        //placeOb = true;
        preview = previewWall;
        preview.SetActive(true);
        previewLamp.SetActive(false);
        Build.furniture = wall;
    }

    public void Lamp()
    {
        //placeOb = true;
        preview = previewLamp;
        preview.SetActive(true);
        previewWall.SetActive(false);
        Build.furniture = lamp;
       
    }
}
