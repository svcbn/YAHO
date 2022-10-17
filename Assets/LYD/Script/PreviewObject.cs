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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Build.previewObject = preview;
        //마우스의 위치에 따라 레이를 쏜다.
        Vector3 mouse = Input.mousePosition;
        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if(Physics.Raycast(casepoint, out hit, Mathf.Infinity) && preview)
        {
            preview.transform.position = new Vector3(hit.point.x, hit.point.y + preview.transform.localScale.y / 2, hit.point.z);
        }
        

        if(Input.GetMouseButtonDown(1))
        {
            preview.transform.Rotate(0, 90, 0);
        }
    }
    
    public void Wall()
    {
        preview = previewWall;
        preview.SetActive(true);
        previewLamp.SetActive(false);
        Build.furniture = wall;
    }

    public void Lamp()
    {
        preview = previewLamp;
        preview.SetActive(true);
        previewWall.SetActive(false);
        Build.furniture = lamp;
       
    }
}
