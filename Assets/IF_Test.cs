using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IF_Test : MonoBehaviour
{
    ContentSizeFitter sizeFit_content;
    ContentSizeFitter sizeFit;
    InputField iField;

    // Start is called before the first frame update
    void Start()
    {
        sizeFit = GetComponent<ContentSizeFitter>();
        sizeFit_content = transform.parent.GetComponent<ContentSizeFitter>();
        iField = GetComponent<InputField>();
    }

    public void OnWrite()
    {
        string[] checker = iField.text.Split('\n');
        if(checker.Length < 3)
        {

        }

        if(checker.Length > 3)
        {
            sizeFit_content.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFit.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        sizeFit.SetLayoutVertical();
    }
}
