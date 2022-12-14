using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreviewObject : MonoBehaviour
{
    //[HideInInspector]
    public GameObject lamp;
    public GameObject frame;
    public GameObject postIt;
    public GameObject plant;
    public GameObject calculator;

    
    public GameObject PreviewFrame;
    public GameObject previewLamp;
    public GameObject previewPostIt;
    public GameObject previewPlant;
    public GameObject previewCarculator;

    

    Build build;

    GameObject preview;

    public bool placeOb = true;

    
    //가구종류 리스트에는 램프, 벽 이렇게 두가지만 넣어보기 
    //public int kindFurSize = 5;

    //번호(index) 변수 만들어주기
    int index;

    // Start is called before the first frame update
    void Start()
    {
        // placeOb = true;
        build = GetComponent<Build>();

        //가구 종류에 가구를 넣어준다.
        //for(int i = 0; i < kindFurSize; i++)
        //{
        //    //kindFur에 가구 종류들을 넣어준다.
        //    //버튼 넣어주기
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //만약 placeOb가 Add 일때 true이고, remove일때 false로 만들어준다. 오브젝트가 보이면 안된다. 
        //오브젝트가 마우스에 따라 움직이는 것은 여기 스크립트에서 움직인다. 

        build.previewObject = preview;
        //마우스의 위치에 따라 레이를 쏜다.
        Vector3 mouse = Input.mousePosition;
        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(casepoint, out hit, Mathf.Infinity) && preview)
        {
           preview.SetActive(placeOb);

            //오브젝트가 마우스에 따라 움직임
            if (placeOb)
            {
                preview.transform.position = new Vector3(hit.point.x, hit.point.y + preview.transform.localScale.y / 2, hit.point.z);
            }

            //만약 태그가 가구이면
            if (hit.transform.tag.Contains("Furniture"))
            {

                
                preview.GetComponentInChildren<Renderer>().material.color = new Color(1,0,0,0.5f); //빨강
                build.canBuild = false;


                /*//박스 콜라이더가 있으면 
                if(hit.transform.GetComponent<BoxCollider>())
                {
                    //1. preview에 달려있는 아이들을 찾는다.
                    Transform[] previewChildList = preview.GetComponentsInChildren<Transform>();
                    if (previewChildList != null)
                    {
                        for (int i = 0; i < previewChildList.Length; i++)
                        {
                            previewChildList[i].GetComponent<Material>().color = Color.red;
                            build.canBuild = false;




                        }
                    }
                }
                else
                {
                    //1. preview에 달려있는 아이들을 찾는다.
                    Transform[] previewChildList = preview.GetComponentsInChildren<Transform>();
                    if (previewChildList != null)
                    {
                        for (int i = 0; i < previewChildList.Length; i++)
                        {
                            previewChildList[i].GetComponent<Material>().color = Color.green;
                            build.canBuild = true;




                        }
                    }
                }*/


            }
            else
            {
                preview.GetComponentInChildren<Renderer>().material.color = new Color(0, 1,0.5f, 0.5f); //초록
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
        preview = PreviewFrame;
        preview.SetActive(true);
        previewLamp.SetActive(false);
        previewPostIt.SetActive(false);
        previewPlant.SetActive(false);
        previewCarculator.SetActive(false);
        build.furniture = frame;
        build.canBuild = true;

        // placeOb = false;
    }

    public void Lamp()
    {
        build.state = Build.State.Add;
        placeOb = true;
        preview = previewLamp;
        preview.SetActive(true);
        PreviewFrame.SetActive(false);
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
        PreviewFrame.SetActive(false);
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
        PreviewFrame.SetActive(false);
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
        PreviewFrame.SetActive(false);
        previewLamp.SetActive(false);
        previewPostIt.SetActive(false);
        previewPlant.SetActive(false);
        build.furniture = calculator;
        build.canBuild = true;
    }
}

