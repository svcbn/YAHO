using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProject_BH : MonoBehaviour
{
    public Dropdown dDcoWorker;
    public Transform panelCoWorker;
    GameObject coWorkerPrefab;
    public Button btnSubmit;

    // Start is called before the first frame update
    void Start()
    {
        dDcoWorker.onValueChanged.AddListener(onDDValueChanged);
        coWorkerPrefab = (GameObject)Resources.Load("BtnCoworker");
        btnSubmit.onClick.AddListener(onBtnSubmitClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onDDValueChanged(int i)
    {
        Debug.Log(dDcoWorker.options[i].text);
        if (dDcoWorker.value != 0)
        {
            AddCoWorker(dDcoWorker.options[i].text);
            dDcoWorker.value = 0;
        }
    }

    void AddCoWorker(string name)
    {
        GameObject coWorker = Instantiate(coWorkerPrefab, panelCoWorker);
        coWorker.name = name;
        coWorker.GetComponentInChildren<Text>().text = name;
    }

    void onBtnSubmitClicked()
    {
        this.gameObject.SetActive(false);
    }
}
