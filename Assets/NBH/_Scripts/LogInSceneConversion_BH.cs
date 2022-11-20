using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInSceneConversion_BH : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelLogIn;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        Setmat();
        StartCoroutine(Conversion());
        panelLogIn.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Setmat()
    {
        mat = new Material(panelMain.GetComponent<Image>().material);
        panelMain.GetComponent<Image>().material = mat;
        mat.SetFloat("_BlurDistance", 0.0025f);
        mat.SetFloat("_CenterX", 0.5f);
        mat.SetFloat("_CenterY", 0.5f);
        mat.SetFloat("_DecayRadius", 2f);
    }

    IEnumerator Conversion()
    {
        yield return new WaitForSeconds(1.5f);
        float radius = 2f;
        while (radius > 0.0002f)
        {
            radius = Mathf.Lerp(radius, 0.0001f, 10 * Time.deltaTime);
            mat.SetFloat("_DecayRadius", radius);
            yield return null;
        }
        StartCoroutine(LogInPopUp());
    }

    IEnumerator LogInPopUp()
    {
        float scale = 0f;
        while(scale < 1f)
        {
            scale = Mathf.Lerp(scale, 1.05f, 10 * Time.deltaTime);
            panelLogIn.transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }
}
