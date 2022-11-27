using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //custo��ư�� ������ customBackground �̹����� ������ �ϰ� �ʹ�. 
    //custombackground�� �ִ� x ��ư�� ������ customBackground�̹����� ������. 

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
    //�޴� ��ư�� �������� Ŀ���� ��׶���� task��ư�̶� ���� ��ư�� �����Ѵ�.

    public GameObject menu;

    int count;
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

    //�޴���ư�� ������ custom������, �����ư, task�����̰� �����Ѵ�.
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

    //ȸ�Ƿ�
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

    //������ư ������ �� ȸ�Ƿ��̹��� ���ش�. 
    public void meetingImageBtnX()
    {
        meeting.SetActive(false);
    }
    
    public void MeXBtn()
    {
        me.SetActive(false);
    }

    //BTN EXIT�� ������ DAYReport �̹����� ������. 
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
