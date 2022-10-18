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
        //만약 placeOb가 Add 일때 true이고, remove일때 false로 만들어준다. 오브젝트가 보이면 안된다. 
        //오브젝트가 마우스에 따라 움직이는 것은 여기 스크립트에서 움직인다. 
        //if(placeOb)
        //{
            Build.previewObject = preview;
            //마우스의 위치에 따라 레이를 쏜다.
            Vector3 mouse = Input.mousePosition;
            Ray casepoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(casepoint, out hit, Mathf.Infinity) && preview)
            {
                //오브젝트가 마우스에 따라 움직임
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
