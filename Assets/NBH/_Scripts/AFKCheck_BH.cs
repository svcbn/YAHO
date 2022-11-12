using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFKCheck_BH : MonoBehaviour
{
    public GameObject detection;
    public int awayTime = 0;
    AFKState state;
    public enum AFKState
    {
        STAY,
        AWAY,
        STAYHOLD,
        AWAYHOLD
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AwayCheck());
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    IEnumerator AwayCheck()
    {
        int awayCount = 0;

        while(true)
        {
            if(detection.activeSelf)
            {
                awayCount = 0;
                Debug.Log("일하는중");
            }
            else
            {
                awayCount++;
                Debug.Log("자리비움");
            }
            yield return new WaitForSeconds(5);

            if(awayCount > awayTime)
            {
                state = AFKState.AWAY;
            }
        }
    }


    void SwitchState()
    {
        switch(state)
        {
            case AFKState.STAY:

                break;
            case AFKState.AWAY:

                break;
        }
    }
}
