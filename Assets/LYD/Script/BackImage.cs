using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackImage : MonoBehaviour
{
    public GameObject endI;
    public GameObject back;
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.transform.position = Vector3.Lerp(endI.transform.position, back.transform.position, Time.deltaTime * 2);
    }
}
