using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    //custo��ư�� ������ customBackground �̹����� ������ �ϰ� �ʹ�. 
    //custombackground�� �ִ� x ��ư�� ������ customBackground�̹����� ������. 

    public GameObject customBackground;


    public GameObject customBtn;
    public GameObject taskBtn;
    public GameObject enterBtn;
    public GameObject meetingHistoryBtn;
    public GameObject exitBtn;

    public GameObject me;
    public GameObject meXBtn;

    public GameObject meetingImage;

    public GameObject dayReport;

    public GameObject btnEnd;
    public GameObject BlackImage;

    


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
    }

    public void CustomXBtn()
    {
        customBackground.SetActive(false);
        menu.SetActive(true);
        taskBtn.SetActive(true);
        enterBtn.SetActive(true);
        meetingHistoryBtn.SetActive(true);
        exitBtn.SetActive(true);

    }

    //�޴���ư�� ������ custom������, �����ư, task�����̰� �����Ѵ�.
    public void OnMenu()
    {
        count++;

        if (count % 2 != 0)
        {
            customBtn.SetActive(true);
            taskBtn.SetActive(true);
            enterBtn.SetActive(true);
            meetingHistoryBtn.SetActive(true);
            exitBtn.SetActive(true);

        }
        else
        {
            customBtn.SetActive(false);
            taskBtn.SetActive(false);
            enterBtn.SetActive(false);
            meetingHistoryBtn.SetActive(false);
            exitBtn.SetActive(false);

        }
    }

    public void OnTaskBtn()
    {
        me.SetActive(true);
    }

   public void OnMeetingHistroy()
    {
        meetingImage.SetActive(true);
    }

    //������ư ������ �� ȸ�Ƿ��̹��� ���ش�. 
    public void meetingImageBtnX()
    {
        meetingImage.SetActive(false);
    }
    
    public void MeXBtn()
    {
        me.SetActive(false);
    }

    //BTN EXIT�� ������ DAYReport �̹����� ������. 
    public void OnDayReport()
    {
        dayReport.SetActive(true);
    }

    public void OnBtnEnd()
    {
        BlackImage.SetActive(true);
    }
    
}
