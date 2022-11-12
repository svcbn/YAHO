using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace NaverAPI
{
    class APIExamSTT 
    {
        public static string Main(string path, STTTest_BH stt)
        {
            string FilePath = path;
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[fs.Length];
            fs.Read(fileData, 0, fileData.Length);
            fs.Close();

            string lang = "Kor";    // 언어 코드 ( Kor, Jpn, Eng, Chn )
            string url = $"https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang={lang}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-NCP-APIGW-API-KEY-ID", "q508pwz7zu");
            request.Headers.Add("X-NCP-APIGW-API-KEY", "OvvUwV0qE6JqIWqt8XA6BnxPYuhCOjdud8NInUcW");
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.ContentLength = fileData.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileData, 0, fileData.Length);
                requestStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            stt.End();
            return text;
        }


    }
}
