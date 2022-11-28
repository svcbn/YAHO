using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using System;
using UnityEngine.UI;

public class FormFile
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
    public Stream Stream { get; set; }
}

[System.Serializable]
public class MultipartImage
{
    public string faceType;
    public byte[] image;
}


public class MultipartTest_BH : MonoBehaviour
{
    

    public static string PostMultipart(string url, Dictionary<string, object> parameters)
    {
        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "multipart/form-data; boundary=" + boundary;
        request.Method = "POST";
        request.KeepAlive = true;
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;

        if (parameters != null && parameters.Count > 0)
        {

            using (Stream requestStream = request.GetRequestStream())
            {

                foreach (KeyValuePair<string, object> pair in parameters)
                {

                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    if (pair.Value is FormFile)
                    {
                        FormFile file = pair.Value as FormFile;
                        string header = "Content-Disposition: form-data; name=\"" + pair.Key + "\"; filename=\"" + file.Name + "\"\r\nContent-Type: " + file.ContentType + "\r\n\r\n";
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(header);
                        requestStream.Write(bytes, 0, bytes.Length);
                        byte[] buffer = new byte[32768];
                        int bytesRead;
                        if (file.Stream == null)
                        {
                            // upload from file
                            using (FileStream fileStream = File.OpenRead(file.FilePath))
                            {
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                    requestStream.Write(buffer, 0, bytesRead);
                                fileStream.Close();
                            }
                        }
                        else
                        {
                            // upload from given stream
                            while ((bytesRead = file.Stream.Read(buffer, 0, buffer.Length)) != 0)
                                requestStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    else
                    {
                        string data = "Content-Disposition: form-data; name=\"" + pair.Key + "\"\r\n\r\n" + pair.Value;
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();
            }
        }

        using (WebResponse response = request.GetResponse())
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
                return reader.ReadToEnd();
        }


    }

    public void UploadMultipart(byte[] file, string filename, string contentType, string url)
    {
        var webClient = new WebClient();
        string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
        webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
        var fileData = webClient.Encoding.GetString(file);
        var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filename, contentType, fileData);

        var nfile = webClient.Encoding.GetBytes(package);

        
        
    }

    public void OnCompleteTest(DownloadHandler handler)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator UploadTest()
    {
        List<IMultipartFormSection> data = new List<IMultipartFormSection>();

        data.Add(new MultipartFormDataSection("faceType", "front"));
        data.Add(new MultipartFormFileSection("image", sendTexture, "front", "image/jpg"));

        UnityWebRequest www = new UnityWebRequest();
        www.SetRequestHeader("Content-Type", "multipart/form-data");
        www.url = "http://43.201.58.81:8088/detectFace/checkFace";
        

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! " + www.downloadHandler.text);

        }
        www.Dispose();
    }

    IEnumerator Upload(byte[] byteArray)
    {
        //List<IMultipartFormSection> faceType = new List<IMultipartFormSection>();

        //pictureData.Add(new MultipartFormFileSection("file", File.ReadAllBytes(ImageCapture.imageSavePath), "imageName", "image/jpg"));
        //faceType.Add(new MultipartFormFileSection("faceType", byteArray, "front", "image/png"));

        //faceType.Add(new MultipartFormDataSection(byteArray));

        //pictureData.Add(new MultipartFormFileSection("userId", "17ac4c482dcdd"));

        MultipartImage multipartImage = new MultipartImage()
        {
            faceType = "front" //JsonUtility.ToJson(byteArray)
        };

        UnityWebRequest webRequest = null;

        webRequest = UnityWebRequest.Post("http://43.201.58.81:8088/members/checkFace", JsonUtility.ToJson(multipartImage));
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(multipartImage));
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        webRequest.SetRequestHeader("Content-Type", "image/png");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            print(webRequest.downloadHandler.text);
        }
        //그렇지않다면
        else
        {
            //서버통신 실패
            print("통신 실패" + webRequest.result + "\n" + webRequest.error);

        }

        yield return null;
        webRequest.Dispose();

        //yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete! " + www.downloadHandler.text);
        //}
    }

    IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        //int width = Screen.width;
        //int height = Screen.height;
        //var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //tex.Apply();

        // Encode texture into PNG
        //byte[] bytes = tex.EncodeToPNG();
        //Destroy(tex);

        // Create a Web Form
        WWWForm form = new WWWForm();
        form.AddField("faceType", "front");
        form.AddBinaryData("image", sendTexture, "front.png", "image/png");
       

        // Upload to a cgi script
        WWW w = new WWW("http://192.168.0.2:8001/detectFace/checkFace", form);
        yield return w;
        if (!string.IsNullOrEmpty(w.error))
        {
            print(w.error);
        }
        else
        {
            print("전송완료");
        }
    }

    [SerializeField]
    Dictionary<string, object> pairs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ScreenShare();
            //UploadMultipart(sendTexture, "faceType", "image/png", "http://43.201.58.81:8088/checkFace");
            StartCoroutine(UploadTest());
        }
    }

    Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D((int)(texture.width) , (int)(texture.height));
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture2D.width, texture2D.height, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        Color[] pixels = texture2D.GetPixels();
        RenderTexture.active = currentRT;

        return texture2D;

    }

    byte[] Texture2DToByte(Texture2D texture2D)
    {
        byte[] bArray = texture2D.GetRawTextureData();
        return bArray;
    }

    
    Texture originMesh;
    Texture2D sendTexture2D;
    byte[] sendTexture;

    void ScreenShare()
    {
        originMesh = GameObject.Find("RawImage").GetComponent<RawImage>().mainTexture;
        sendTexture2D = TextureToTexture2D(originMesh);
        sendTexture = Texture2DToByte(sendTexture2D);
    }
    
}
