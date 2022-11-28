using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class RecordTest_BH : MonoBehaviour
{
    AudioClip recordClip;
    STTTest_BH sTT;
    public GameObject imageRecord;

    public GameObject panelMakeProject;
    public GameObject panelSelectProject;
    public GameObject panelProjectInfo;
    public GameObject panelTeamTask;

    void StartRecordMicrophone()
    {

        recordClip = Microphone.Start(Microphone.devices[0], true, 100, 44100);
    }

    void StopRecordMicrophone()
    {
        int lastTime = Microphone.GetPosition(Microphone.devices[0]);

        if (lastTime == 0) return;
        else
        {
            Microphone.End(Microphone.devices[0]);

            float[] samples = new float[recordClip.samples];

            recordClip.GetData(samples, 0);

            float[] cutSamples = new float[lastTime];

            Array.Copy(samples, cutSamples, cutSamples.Length - 1);

            recordClip = AudioClip.Create("Notice", cutSamples.Length, 1, 44100, false);

            recordClip.SetData(cutSamples, 0);
        
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <Microphone.devices.Length; i++)
        {
            print(Microphone.devices[i]);
        }

        sTT = GetComponent<STTTest_BH>();
    }

    // Update is called once per frame
    void Update()
    {
        if(panelMakeProject.activeSelf || panelProjectInfo.activeSelf || panelSelectProject.activeSelf || panelTeamTask.activeSelf)
        {
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(RecordStartDelay());
                print("≥Ï¿ΩΩ√¿€");
                StartRecordMicrophone();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                print("≥Ï¿Ω≥°");
                StartCoroutine(RecordEndDelay());
            }
        }

    }

    IEnumerator RecordStartDelay()
    {
        yield return new WaitForSeconds(1);
        imageRecord.SetActive(true);
    }

    IEnumerator RecordEndDelay()
    {
        imageRecord.SetActive(false);
        yield return new WaitForSeconds(1);
        StopRecordMicrophone();
        SavWav.Save("test", recordClip);
        sTT.SendWav();
    }
}
