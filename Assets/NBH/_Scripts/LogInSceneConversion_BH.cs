using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInSceneConversion_BH : MonoBehaviour
{
    public Image image1;
    public Image image2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && image2.enabled)
        {
            StartCoroutine(Conversion());
        }
    }

    IEnumerator Conversion()
    {
        float a = 0;
        while(true)
        {
            a = Mathf.Lerp(a, 1, 5 * Time.deltaTime);
            image1.color = new Color(1, 1, 1, a);
            image2.color = new Color(1, 1, 1, 1 - a);
            yield return null;

            if(a > 0.99f)
            {
                break;
            }
        }
        image2.enabled = false;

    }

}
