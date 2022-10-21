using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//배치된 거 삭제하기
public class Build : MonoBehaviour
{
    //previewObject 에서 사용하기 위해 싱글톤으로 생성하기
    public static Build instance;

    GameObject place;
    public enum State
    {
        Add,
        Remove,
        Idle
    }
    public State state;

    public GameObject furniture;
    public GameObject previewObject;

    PreviewObject prevObject;

    //빈오브젝트 부모 변수 선언
    public GameObject furnitureParent;

    //가구 종류 리스트를 하이어라키 창에서 넣을 수 있도록 만들어준다.
    public List<GameObject> previewkindFur;

    //public GameObject removeImage;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            //중복되는 게 생기면 생성자체 못하도록 파괴
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //place.GetComponent<PreviewObject>().placeOb = true;
        prevObject = GetComponent<PreviewObject>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {



        if (state == State.Add)
        {

            //마우스 왼쪽 버튼을 클릭하면
            if (Input.GetMouseButtonDown(0))
            {

                if (!EventSystem.current.currentSelectedGameObject) //
                {
                    //
                    GameObject objectClone = Instantiate(furniture, previewObject.transform.position, previewObject.transform.rotation);
                    //furnitureParent 나의 부모는 누구다.라고 선언/ 이를 위해 부모의 transform을 우선 찾아준다. 
                    objectClone.transform.parent = furnitureParent.transform;
                    previewkindFur.Add(objectClone);
                    objectClone.tag = "Furniture";
                    prevObject.placeOb = false;
                    state = State.Idle;
                    //*wall.transform.position = transform.position;
                }

            }
        }
        /*else if(state == State.Idle)
        {
            previewObject.SetActive(false);
        }*/

        else if (state == State.Remove)
        {
            // 마우스 왼쪽 버튼을 클릭했을때
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if(hitInfo.transform.tag.Contains("Furniture"))
                    {
                        //제거
                        previewkindFur.Remove(hitInfo.transform.gameObject);
                        //ray 에 맞은 오브젝트가 사라진다.
                        Destroy(hitInfo.transform.gameObject);
                        state = State.Idle;
                    }
                    
                }

            }

        }

     


    }

    //remove 버튼(UI)을 누른다.

    public void OnObjectRemove()
    {
        state = State.Remove;

    }


    public void OnObjectRemoveAll()
    {
        //1. list다 삭제
        previewkindFur.Clear();
        //2. FurnitureParent의 자식들을 삭제
        Transform[] furChildList = furnitureParent.GetComponentsInChildren<Transform>();
        if(furChildList != null)
        {
            for(int i = 1; i < furChildList.Length; i++)
            {
                if (furChildList[i] != transform)
                    Destroy(furChildList[i].gameObject);
            }
        }

            //널(아무것도 없을 때는 작동하지 않도록) -> 상태를 idle로 그냥 바꿔준다. ?







        
    }
}
