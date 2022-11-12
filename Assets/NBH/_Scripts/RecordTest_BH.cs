using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;


public class RecordTest_BH : MonoBehaviour
{
    AudioClip recordClip;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("녹음시작");
            StartRecordMicrophone();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("녹음끝");
            StopRecordMicrophone();
            SavWav.Save("test", recordClip);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            byte[] data = File.ReadAllBytes("C:\\Users\\HP\\Desktop\\4차프로젝트\\WAV\\test.wav");
           
            File.WriteAllBytes(Application.dataPath + "/b.wav", data);
        }
    }
}
