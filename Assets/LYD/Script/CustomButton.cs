using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //custo버튼을 누르면 customBackground 이미지가 켜지게 하고 싶다. 
    //custombackground에 있는 x 버튼을 누르면 customBackground이미지가 꺼진다. 

    public GameObject customBackground;


    public GameObject taskBtn;
    public GameObject enterBtn;
    

    public GameObject exitBtn;

    public GameObject me;
    public GameObject meXBtn;

    //public GameObject meetingImage;

    public GameObject dayReport;

    public GameObject btnEnd;
    public GameObject BlackImage;

    public GameObject meetingHistoryBtn;
    public GameObject meeting;

    public GameObject image;
    public GameObject menuImage1;
    //메뉴 버튼을 눌러을때 커스텀 백그라운드랑 task버튼이랑 입장 버튼이 떠야한다.

    public GameObject menu;

    int count;
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
        menu.SetActive(false);
        taskBtn.SetActive(false);
        enterBtn.SetActive(false);
        meetingHistoryBtn.SetActive(false);
        exitBtn.SetActive(false);

        image.SetActive(false);
        menuImage1.SetActive(false);
    }

    public void CustomXBtn()
    {
        customBackground.SetActive(false);
        menu.SetActive(true);
        

    }

    //메뉴버튼을 누를때 custom유아이, 입장버튼, task유아이가 떠야한다.
    public void OnMenu()
    {
        // count++;

        //if (count % 2 != 0)
        //{4
        image.SetActive(true);
        menuImage1.SetActive(true);
            taskBtn.SetActive(true);
            enterBtn.SetActive(true);
            meetingHistoryBtn.SetActive(true);
            exitBtn.SetActive(true);

        menu.SetActive(false);

       // }
        /*else
        {
            customBtn.SetActive(false);
            taskBtn.SetActive(false);
            enterBtn.SetActive(false);
            meetingHistoryBtn.SetActive(false);
            exitBtn.SetActive(false);

        }*/
    }
    public void OnBackMenu()
    {
        image.SetActive(false);
        menuImage1.SetActive(false);
        taskBtn.SetActive(false);
        enterBtn.SetActive(false);
        meetingHistoryBtn.SetActive(false);
        exitBtn.SetActive(false);

        menu.SetActive(true);
    }

    //1..todo
    public void OnTaskBtn()
    {
        me.SetActive(true);

        image.SetActive(false);
        menuImage1.SetActive(false);
        taskBtn.SetActive(false);
        enterBtn.SetActive(false);
        meetingHistoryBtn.SetActive(false);
        exitBtn.SetActive(false);

        menu.SetActive(true);

    }

    //회의록
   public void OnMeetingHistroy()
    {
        meeting.SetActive(true);

        image.SetActive(false);
        menuImage1.SetActive(false);
        taskBtn.SetActive(false);
        enterBtn.SetActive(false);
        meetingHistoryBtn.SetActive(false);
        exitBtn.SetActive(false);

        menu.SetActive(true);
    }

    //엑스버튼 눌렀을 때 회의록이미지 꺼준다. 
    public void meetingImageBtnX()
    {
        meeting.SetActive(false);
    }
    
    public void MeXBtn()
    {
        me.SetActive(false);
    }

    //BTN EXIT을 누르면 DAYReport 이미지가 켜진다. 
    public void OnDayReport()
    {
        dayReport.SetActive(true);

        image.SetActive(false);
        menuImage1.SetActive(false);
        taskBtn.SetActive(false);
        enterBtn.SetActive(false);
        meetingHistoryBtn.SetActive(false);
        exitBtn.SetActive(false);

        menu.SetActive(true);
    }

    public void OnBtnEnd()
    {
        BlackImage.SetActive(true);
    }
    
}
