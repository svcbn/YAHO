using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidiingUI_BH : MonoBehaviour
{
    public GameObject btnOpen;
    public Button btnOpenUI;
    public Button btnCloseUI;
    public GameObject panelUI;
    public RectTransform openPos;
    public RectTransform closePos;

    float speed = 7f;
    float dist = 10f;

    void Start()
    {
        btnOpenUI.onClick.AddListener(OnBtnOpenUIClicked);
        btnCloseUI.onClick.AddListener(OnBtnCloseUIClicked);
    }

    void OnBtnOpenUIClicked()
    {
        StartCoroutine(SlideOpen());
    }

    void OnBtnCloseUIClicked()
    {
        StartCoroutine(SlideClose());
    }

    public IEnumerator SlideOpen()
    {
        while (Vector2.Distance(panelUI.transform.position, openPos.transform.position) > dist)
        {
            panelUI.transform.position = Vector3.Lerp(panelUI.transform.position, openPos.transform.position, Time.deltaTime * speed);
            yield return null;
        }
        panelUI.transform.position = openPos.transform.position;
        btnOpen.SetActive(false);
    }

    public IEnumerator SlideClose()
    {
        while (Vector2.Distance(panelUI.transform.position, closePos.transform.position) > dist)
        {
            panelUI.transform.position = Vector3.Lerp(panelUI.transform.position, closePos.transform.position, Time.deltaTime * speed);
            yield return null;
        }
        panelUI.transform.position = closePos.transform.position;
        btnOpen.SetActive(true);
    }
    
}
