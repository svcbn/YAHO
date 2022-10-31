using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //custo버튼을 누르면 customBackground 이미지가 켜지게 하고 싶다. 
    //custombackground에 있는 x 버튼을 누르면 customBackground이미지가 꺼진다. 

    public GameObject customBackground;
    /*public GameObject Boardid;
    public GameObject trelloManager;

    Button addC;
    
    void Start()
    {
        //boardid에서 자식을 찾는다. ->add a card
        GameObject addCard = Boardid.transform.GetChild(2).gameObject;
        //버튼 컴포넌트를 가져온다.
        addC = addCard.GetComponent<Button>();
        Debug.Log(addCard.name);
        addC.onClick.AddListener(AddCard);
    }

    //add a card 버튼을 누르면 trelloCanvas가 켜진다.
    public void AddCard()
    {
        GameObject trellopanel = trelloManager.transform.GetChild(0).GetChild(0).gameObject;
        trellopanel.SetActive(true);
        
    }*/

    //custo버튼을 누르면 customBackground 이미지가 켜지게 하고 싶다. 
    public void OnCustomBtn()
    {
        customBackground.SetActive(true);
    }

    public void CustomXBtn()
    {
        customBackground.SetActive(false);

    }

   

    
    
}
