using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreviewObject : MonoBehaviour
{
    //[HideInInspector]
    public GameObject lamp;
    public GameObject wall;
    public GameObject postIt;
    public GameObject plant;
    public GameObject calculator;

    
    public GameObject previewWall;
    public GameObject previewLamp;
    public GameObject previewPostIt;
    public GameObject previewPlant;
    public GameObject previewCarculator;

    

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

            //���� �±װ� �����̸�
            if(hit.transform.tag.Contains("Furniture"))
            {

                preview.GetComponentInChildren<Renderer>().material.color = new Color(1,0,0,0.5f);
                build.canBuild = false;
                

                  
            }
            else { preview.GetComponentInChildren<Renderer>().material.color = new Color(0, 1, 0, 0.5f);

                build.canBuild = true;
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
        previewPostIt.SetActive(false);
        previewPlant.SetActive(false);
        previewCarculator.SetActive(false);
        build.furniture = wall;
        build.canBuild = true;

        // placeOb = false;
    }

    public void Lamp()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewLamp;
        preview.SetActive(true);
        previewWall.SetActive(false);
        previewPostIt.SetActive(false);
        previewPlant.SetActive(false);
        previewCarculator.SetActive(false);
        build.furniture = lamp;
        build.canBuild = true;

        // placeOb = false;


    }

    public void PostIt()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewPostIt;
        preview.SetActive(true);
        previewWall.SetActive(false);
        previewLamp.SetActive(false);
        previewPlant.SetActive(false);
        previewCarculator.SetActive(false);
        build.furniture = postIt;
        build.canBuild = true;
    }

    public void Plant()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewPlant;
        preview.SetActive(true);
        previewWall.SetActive(false);
        previewLamp.SetActive(false);
        previewPostIt.SetActive(false);
        previewCarculator.SetActive(false);
        build.furniture = plant;
        build.canBuild = true;
    }

    public void Carculator()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewCarculator;
        preview.SetActive(true);
        previewWall.SetActive(false);
        previewLamp.SetActive(false);
        previewPostIt.SetActive(false);
        previewPlant.SetActive(false);
        build.furniture = calculator;
        build.canBuild = true;
    }
}

