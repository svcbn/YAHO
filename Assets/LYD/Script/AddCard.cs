using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCard : MonoBehaviour
{
    //public GameObject Boardid;
    //public GameObject trelloManager;

    //Button addC;

    void Start()
    {
        
    }

    //add a card ��ư�� ������ trelloCanvas�� ������.
    public void AddCardBtn()
    {
        /*//boardid���� �ڽ��� ã�´�. ->add a card
        GameObject addCard = Boardid.transform.GetChild(2).gameObject;
        //��ư ������Ʈ�� �����´�.
        addC = addCard.GetComponent<Button>();
        Debug.Log(addCard.name);*/

        GameObject trellManager = GameObject.Find("TrelloManager");
        Debug.Log(trellManager.transform.GetChild(0).childCount);
        GameObject trellManager2 = trellManager.transform.GetChild(0).GetChild(0).gameObject;
        trellManager2.SetActive(true);

    }
}
