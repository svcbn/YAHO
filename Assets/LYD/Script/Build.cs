using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//배치된 거 삭제하기
public class Build : MonoBehaviour
{

   public enum State
    {
        Add,
        Remove
    }
    public State state;

    static public GameObject furniture;
    static public GameObject previewObject;

    public GameObject removeImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(state == State.Add)
       {
            //마우스 왼쪽 버튼을 클릭하면
            if (Input.GetMouseButtonDown(0))
            {
                GameObject objectClone = Instantiate(furniture, previewObject.transform.position, previewObject.transform.rotation);
                //*wall.transform.position = transform.position;

            }
       }
       
       
        if(state == State.Remove)
        {
            //ray를 쏜다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //마우스 왼쪽 버튼을 클릭했을때
                if(Input.GetMouseButtonDown(0))
                {
                    //ray 에 맞은 오브젝트가 사라진다.
                    Destroy(furniture);

                }


            }
        }
       

    }

    //remove 버튼(UI)을 누른다.

    public void OnObjectRemove()
    {
        state = State.Remove;
        //상태를 제거상태로 바꾼다.
        
    }
    


}
