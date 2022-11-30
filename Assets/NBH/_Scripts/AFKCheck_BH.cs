using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AFKCheck_BH : MonoBehaviour
{
    public WebcamHandler_BH webcam;
    public GameObject imageAFK;
    public RectTransform afkPos;
    public RectTransform stayPos;

    float speed = 7f;
    float dist = 10f;

    public static AFKCheck_BH instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AFKforSec(5));
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public bool isfinished = false;

    IEnumerator AFKforSec(int seconds)
    {
        while (true)
        {
            isfinished = false;

            yield return new WaitForSeconds(seconds);

            webcam.StartWebCam();

            yield return new WaitForSeconds(1);

            webcam.StopWebCam();
            CaptureImg();

            StartCoroutine(WebRequester_BH.instance.UploadPNG(sendTexture, UserInformation_BH.instance.MemberNo, 1));

            yield return new WaitUntil(() => isfinished);
        }
    }
    
    Texture originMesh;
    byte[] sendTexture;

    void CaptureImg()
    {
        originMesh = GameObject.Find("RawImage").GetComponent<RawImage>().mainTexture;
        sendTexture = Texture2Byte(originMesh);
    }

    byte[] Texture2Byte(Texture texture)
    {
        Texture2D texture2D = new Texture2D((int)(texture.width), (int)(texture.height));

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture2D.width, texture2D.height, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        Color[] pixels = texture2D.GetPixels();
        RenderTexture.active = currentRT;

        byte[] bArray = texture2D.EncodeToJPG(); //texture2D.GetRawTextureData();
        return bArray;
    }

    public void OnCompleteAFKCheck(DownloadHandler handler)
    {
        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);

        if(jsonData.data == "false")
        {
            // 자리비움
            StartCoroutine(SlideOpen());
        }
        else
        {
            StartCoroutine(SlideClose());
        }

        isfinished = true;
    }

    public IEnumerator SlideOpen()
    {
        while (Vector2.Distance(imageAFK.transform.position, afkPos.transform.position) > dist)
        {
            imageAFK.transform.position = Vector3.Lerp(imageAFK.transform.position, afkPos.transform.position, Time.deltaTime * speed);
            yield return null;
        }
        imageAFK.transform.position = afkPos.transform.position;
    }

    public IEnumerator SlideClose()
    {
        while (Vector2.Distance(imageAFK.transform.position, stayPos.transform.position) > dist)
        {
            imageAFK.transform.position = Vector3.Lerp(imageAFK.transform.position, stayPos.transform.position, Time.deltaTime * speed);
            yield return null;
        }
        imageAFK.transform.position = stayPos.transform.position;
    }
}
