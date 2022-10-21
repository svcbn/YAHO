using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//��ġ�� �� �����ϱ�
public class Build : MonoBehaviour
{
    //previewObject ���� ����ϱ� ���� �̱������� �����ϱ�
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

    //�������Ʈ �θ� ���� ����
    public GameObject furnitureParent;

    //���� ���� ����Ʈ�� ���̾��Ű â���� ���� �� �ֵ��� ������ش�.
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
            //�ߺ��Ǵ� �� ����� ������ü ���ϵ��� �ı�
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

            //���콺 ���� ��ư�� Ŭ���ϸ�
            if (Input.GetMouseButtonDown(0))
            {

                if (!EventSystem.current.currentSelectedGameObject) //
                {
                    //
                    GameObject objectClone = Instantiate(furniture, previewObject.transform.position, previewObject.transform.rotation);
                    //furnitureParent ���� �θ�� ������.��� ����/ �̸� ���� �θ��� transform�� �켱 ã���ش�. 
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
            // ���콺 ���� ��ư�� Ŭ��������
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if(hitInfo.transform.tag.Contains("Furniture"))
                    {
                        //����
                        previewkindFur.Remove(hitInfo.transform.gameObject);
                        //ray �� ���� ������Ʈ�� �������.
                        Destroy(hitInfo.transform.gameObject);
                        state = State.Idle;
                    }
                    
                }

            }

        }

     


    }

    //remove ��ư(UI)�� ������.

    public void OnObjectRemove()
    {
        state = State.Remove;

    }


    public void OnObjectRemoveAll()
    {
        //1. list�� ����
        previewkindFur.Clear();
        //2. FurnitureParent�� �ڽĵ��� ����
        Transform[] furChildList = furnitureParent.GetComponentsInChildren<Transform>();
        if(furChildList != null)
        {
            for(int i = 1; i < furChildList.Length; i++)
            {
                if (furChildList[i] != transform)
                    Destroy(furChildList[i].gameObject);
            }
        }

            //��(�ƹ��͵� ���� ���� �۵����� �ʵ���) -> ���¸� idle�� �׳� �ٲ��ش�. ?







        
    }
}
