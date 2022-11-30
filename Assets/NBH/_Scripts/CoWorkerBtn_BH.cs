using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoWorkerBtn_BH : MonoBehaviour
{
    GameObject btn;
    Transform content;
    GameObject btnFac;

    void Start()
    {
        btn = this.gameObject;
        btn.GetComponent<Button>().onClick.AddListener(OnBtnClicked);
        btnFac = (GameObject)Resources.Load("BtnCoWorkers");
        content = GameObject.Find("PanelCoWorkers").transform;
    }

    void OnBtnClicked()
    {
        GameObject btnCoWorkers = Instantiate(btnFac);
        btnCoWorkers.GetComponentInChildren<Text>().text = btn.GetComponentInChildren<Text>().text.Substring(0, 3);
        btnCoWorkers.transform.SetParent(content);
    }
}
