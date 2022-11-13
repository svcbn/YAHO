using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoWorkerBtn_BH : MonoBehaviour
{
    private void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnBtnClicked);
    }

    void OnBtnClicked()
    {
        Destroy(gameObject);
    }
}
