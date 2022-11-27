using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuImage : MonoBehaviour
{
    //public GameObject image;
    public GameObject endImage;
    //float currentTime = 0;
    //float createTime = 0.00001f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*currentTime += Time.deltaTime;
        if(currentTime > createTime)
        {
            currentTime = 0;
            Vector3 dir = endpos - transform.position;
            dir.Normalize();
            transform.position = transform.position + dir;

        }*/
       transform.position = Vector3.Lerp(transform.position, endImage.transform.position, 1);
    }
}
