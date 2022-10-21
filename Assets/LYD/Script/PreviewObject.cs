using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreviewObject : MonoBehaviour
{
    public GameObject wall;
    public GameObject lamp;

    public GameObject previewWall;
    public GameObject previewLamp;

    Build build;

    GameObject preview;

    public bool placeOb = true;

    
    //�������� ����Ʈ���� ����, �� �̷��� �ΰ����� �־�� 
    //public int kindFurSize = 5;

    //��ȣ(index) ���� ������ֱ�
    int index;

    // Start is called before the first frame update
    void Start()
    {
        // placeOb = true;
        build = GetComponent<Build>();

        //���� ������ ������ �־��ش�.
        //for(int i = 0; i < kindFurSize; i++)
        //{
        //    //kindFur�� ���� �������� �־��ش�.
        //    //��ư �־��ֱ�
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //���� placeOb�� Add �϶� true�̰�, remove�϶� false�� ������ش�. ������Ʈ�� ���̸� �ȵȴ�. 
        //������Ʈ�� ���콺�� ���� �����̴� ���� ���� ��ũ��Ʈ���� �����δ�. 

        build.previewObject = preview;
        //���콺�� ��ġ�� ���� ���̸� ���.
        Vector3 mouse = Input.mousePosition;
        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(casepoint, out hit, Mathf.Infinity) && preview)
        {
           preview.SetActive(placeOb);

            //������Ʈ�� ���콺�� ���� ������
            if (placeOb)
            {
                preview.transform.position = new Vector3(hit.point.x, hit.point.y + preview.transform.localScale.y / 2, hit.point.z);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            preview.transform.Rotate(0, 90, 0);
        }
    }



    public void Wall()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewWall;
        preview.SetActive(true);
        previewLamp.SetActive(false);
        build.furniture = wall;

        // placeOb = false;
    }

    public void Lamp()
    {
        build.state = Build.State.Add;

        placeOb = true;
        preview = previewLamp;
        preview.SetActive(true);
        previewWall.SetActive(false);
        build.furniture = lamp;
        // placeOb = false;


    }
}

