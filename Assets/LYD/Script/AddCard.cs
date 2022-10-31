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

    //add a card 버튼을 누르면 trelloCanvas가 켜진다.
    public void AddCardBtn()
    {
        /*//boardid에서 자식을 찾는다. ->add a card
        GameObject addCard = Boardid.transform.GetChild(2).gameObject;
        //버튼 컴포넌트를 가져온다.
        addC = addCard.GetComponent<Button>();
        Debug.Log(addCard.name);*/

        GameObject trellManager = GameObject.Find("TrelloManager");
        Debug.Log(trellManager.transform.GetChild(0).childCount);
        GameObject trellManager2 = trellManager.transform.GetChild(0).GetChild(0).gameObject;
        trellManager2.SetActive(true);

    }
}
