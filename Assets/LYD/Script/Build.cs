using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ġ�� �� �����ϱ�
public class Build : MonoBehaviour
{

   /* public enum State
    {
        Add,
        Remove
    }*/
    //public State state;

    static public GameObject furniture;
    static public GameObject previewObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(state == State.Add)
        //{
            //���콺 ���� ��ư�� Ŭ���ϸ�
            if (Input.GetMouseButtonDown(0))
            {
                GameObject objectClone = Instantiate(furniture, previewObject.transform.position, previewObject.transform.rotation);
                //*wall.transform.position = transform.position;

            }
        //}

       /* if(state == State.Remove)
        {
            //remove ��ư(UI)�� ������.
            //ray�� ���.
            //ray �� ���� ������Ʈ�� �������.
        }*/
       

    } 
    


}
