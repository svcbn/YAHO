using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //custo��ư�� ������ customBackground �̹����� ������ �ϰ� �ʹ�. 
    //custombackground�� �ִ� x ��ư�� ������ customBackground�̹����� ������. 

    public GameObject customBackground;
    /*public GameObject Boardid;
    public GameObject trelloManager;

    Button addC;
    
    void Start()
    {
        //boardid���� �ڽ��� ã�´�. ->add a card
        GameObject addCard = Boardid.transform.GetChild(2).gameObject;
        //��ư ������Ʈ�� �����´�.
        addC = addCard.GetComponent<Button>();
        Debug.Log(addCard.name);
        addC.onClick.AddListener(AddCard);
    }

    //add a card ��ư�� ������ trelloCanvas�� ������.
    public void AddCard()
    {
        GameObject trellopanel = trelloManager.transform.GetChild(0).GetChild(0).gameObject;
        trellopanel.SetActive(true);
        
    }*/

    //custo��ư�� ������ customBackground �̹����� ������ �ϰ� �ʹ�. 
    public void OnCustomBtn()
    {
        customBackground.SetActive(true);
    }

    public void CustomXBtn()
    {
        customBackground.SetActive(false);

    }

   

    
    
}
