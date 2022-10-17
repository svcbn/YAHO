using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ġ�� �� �����ϱ�
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
            //���콺 ���� ��ư�� Ŭ���ϸ�
            if (Input.GetMouseButtonDown(0))
            {
                GameObject objectClone = Instantiate(furniture, previewObject.transform.position, previewObject.transform.rotation);
                //*wall.transform.position = transform.position;

            }
       }
       
       
        if(state == State.Remove)
        {
            //ray�� ���.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //���콺 ���� ��ư�� Ŭ��������
                if(Input.GetMouseButtonDown(0))
                {
                    //ray �� ���� ������Ʈ�� �������.
                    Destroy(furniture);

                }


            }
        }
       

    }

    //remove ��ư(UI)�� ������.

    public void OnObjectRemove()
    {
        state = State.Remove;
        //���¸� ���Ż��·� �ٲ۴�.
        
    }
    


}
