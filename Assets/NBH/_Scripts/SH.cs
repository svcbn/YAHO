//using Cysharp.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.Networking;

//[Serializable]
//public class ResultGet<T>
//{
//    public bool result;
//    public int count;
//    public List<T> data;
//}
//[Serializable]
//public class ResultGetId<T>
//{
//    public bool result;
//    public T data;
//}
//[Serializable]
//public class ResultPost<T>
//{
//    public bool result;
//    public T data;
//}

//[Serializable]
//public class ResultPut
//{
//    public bool result;
//}
//public class DataModule
//{
//    private const string DOMAIN = "http://14.47.254.38:4000"; //"https://dev.team-buildup.shop";
//    //public static string REPLACE_BEARER_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNjY4MDkwMDkzLCJleHAiOjMzMjI1NjkwMDkzfQ.9WI6YXZlRLiB8dIb-Fea4AzMZocUQKkHjVZ0NeAyS2I";
//    public static string REPLACE_BEARER_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiaWF0IjoxNjY4NjY3NjYyLCJleHAiOjMzMjI2MjY3NjYyfQ.xfMelHxCCAcqzXtsR6DPESWBOJf-ty20UP0y1f0sTis";

//    /// <summary>
//    /// Network Type ����
//    /// </summary>
//    public enum NetworkType
//    {
//        GET,
//        POST,
//        PUT,
//        DELETE
//    }
//    /// <summary>
//    /// ���� Data type ����
//    /// </summary>
//    public enum DataType
//    {
//        BUFFER,
//        TEXTURE,
//        ASSETBUNDLE,
//        FILE
//    }

//    protected static double timeout = 5;

//    /// <summary>
//    /// Web�� Data�� Request�� �� �ִ�.
//    /// </summary>
//    /// <typeparam name="T">Json ����</typeparam>
//    /// <param name="_url">Json �ø� url</param>
//    /// <param name="networkType">��� Request �� ���ΰ�</param>
//    /// <param name="data">���� ������</param>
//    /// <returns></returns>
//    public static async UniTask<T> WebRequestBuffer<T>(string _url, NetworkType networkType, DataType dataType, string data = null, List<IMultipartFormSection> form = null, string filePath = null)
//    {
//        //��Ʈ��ũ üŷ
//        await CheckNetwork();
//        //API URL ����
//        string requestURL = DOMAIN + _url;
//        //Timeout ����
//        var cts = new CancellationTokenSource();
//        cts.CancelAfterSlim(TimeSpan.FromSeconds(timeout));

//        UnityWebRequest request;

//        //�� ��û ����(Get,Post,Delete,Update)
//        //Texture�ΰ� Buffer�ΰ�?
//        request = new UnityWebRequest(requestURL, networkType.ToString());

//        //Body ���� �Է�
//        request.downloadHandler = DownHandlerFactory(dataType, filePath, requestURL);


//        if (data != null)
//        {
//            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
//            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
//        }

//        if (form != null)
//        {
//            request = UnityWebRequest.Post(requestURL, form);
//            SetHeaders(request, "Authorization", "Bearer " + REPLACE_BEARER_TOKEN);
//            //SetHeaders(request, "Content-Type", "multipart/form-data");
//            //SetHeaders(request, "FileName", "multipart/form-data");
//        }
//        else
//        {
//            SetHeaders(request, "Authorization", "Bearer " + REPLACE_BEARER_TOKEN);
//            SetHeaders(request, "Content-Type", "application/json");

//        }

//        //Header ���� �Է�
//        if (REPLACE_BEARER_TOKEN == "" && PlayerPrefs.GetString("Bearer") != "")
//        {
//            REPLACE_BEARER_TOKEN = PlayerPrefs.GetString("Bearer");
//        }


//        try
//        {

//            var res = await request.SendWebRequest().WithCancellation(cts.Token);
//            T result = JsonUtility.FromJson<T>(res.downloadHandler.text);

//            return result;
//        }
//        catch (OperationCanceledException ex)
//        {
//            if (ex.CancellationToken == cts.Token)
//            {
//                Debug.Log("Timeout");
//                //TODO: ��Ʈ��ũ ��õ� �˾� ȣ��.

//                //��õ�
//                return await WebRequestBuffer<T>(_url, networkType, dataType, data);
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.Log(e.Message);
//            return default;
//        }
//        return default;
//    }

//    /// <summary>
//    /// Texture Webrequest.
//    /// Post�϶��� �޴� ���� default(return). 
//    /// </summary>
//    /// <param name="url"></param>
//    /// <returns></returns>
//    public static async UniTask<Texture2D> WebrequestTextureGet(string url, NetworkType networkType)
//    {

//        //��Ʈ��ũ üŷ
//        await CheckNetwork();
//        //API URL ����
//        string requestURL = DOMAIN + url;
//        //Timeout ����
//        var cts = new CancellationTokenSource();
//        cts.CancelAfterSlim(TimeSpan.FromSeconds(timeout));

//        UnityWebRequest request;
//        //Texture�ΰ� Buffer�ΰ�?
//        request = UnityWebRequestTexture.GetTexture(requestURL);
//        //Body ���� �Է�
//        DownloadHandlerTexture handlerTexture = request.downloadHandler as DownloadHandlerTexture;

//        //Header ���� �Է�
//        if (REPLACE_BEARER_TOKEN == "" && PlayerPrefs.GetString("Bearer") != "")
//        {
//            REPLACE_BEARER_TOKEN = PlayerPrefs.GetString("Bearer");
//        }

//        SetHeaders(request, "Authorization", "Bearer " + REPLACE_BEARER_TOKEN);
//        SetHeaders(request, "Content-Type", "application/json");

//        try
//        {
//            var res = await request.SendWebRequest().WithCancellation(cts.Token);
//            Texture2D result = handlerTexture.texture;
//            return result;
//        }
//        catch (OperationCanceledException ex)
//        {
//            if (ex.CancellationToken == cts.Token)
//            {
//                Debug.Log("Timeout");
//                //TODO: ��Ʈ��ũ ��õ� �˾� ȣ��.

//                //��õ�
//                return await WebrequestTextureGet(url, networkType);
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.Log(e.Message);
//            return default;
//        }
//        return default;
//    }

//    /// <summary>
//    /// AssetBundle �޴� �ڵ�
//    /// </summary>
//    /// <param name="_url"></param>
//    /// <param name="networkType"></param>
//    /// <param name="dataType"></param>
//    /// <param name="data"></param>
//    /// <param name="filePath"></param>
//    /// <returns></returns>
//    public static async UniTask<AssetBundle> WebRequestAssetBundle(string _url, NetworkType networkType, DataType dataType, string bundleName, string filePath = null)
//    {
//        //��Ʈ��ũ üŷ
//        await CheckNetwork();
//        //API URL ����
//        string requestURL = DOMAIN + _url;
//        //Timeout ����
//        var cts = new CancellationTokenSource();
//        cts.CancelAfterSlim(TimeSpan.FromSeconds(timeout));

//        //�� ��û ����(Get,Post,Delete,Update)
//        //UnityWebRequest request = new UnityWebRequest(requestURL, networkType.ToString());
//        UnityWebRequest request = UnityWebRequest.Get(requestURL);


//        //Body ���� �Է�
//        //request.downloadHandler = DownHandlerFactory(dataType, filePath, requestURL);

//        //Header ���� �Է�
//        if (REPLACE_BEARER_TOKEN == "" && PlayerPrefs.GetString("Bearer") != "")
//        {
//            REPLACE_BEARER_TOKEN = PlayerPrefs.GetString("Bearer");
//        }

//        SetHeaders(request, "Authorization", "Bearer " + REPLACE_BEARER_TOKEN);
//        SetHeaders(request, "Content-Type", "application/json");

//        try
//        {
//            var res = await request.SendWebRequest().WithCancellation(cts.Token);
//            //AssetBundle result = DownloadHandlerAssetBundle.GetContent(request);

//#if UNITY_STANDALONE
//            //������ �ڵ������� ���ÿ� �����Ѵ�.
//            string assetBundleDirectory = Application.dataPath + "/MarketBundle";
//#elif UNITY_IOS
//            string assetBundleDirectory = Application.persistentDataPath + "/MarketBundle";
//#endif
//            if (!Directory.Exists(assetBundleDirectory + "/" + bundleName.Split("/")[0]))
//            {
//                Directory.CreateDirectory(assetBundleDirectory + "/" + bundleName.Split("/")[0]);
//            }

//            FileStream fs = new FileStream(assetBundleDirectory + "/" + bundleName, FileMode.Create);
//            fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
//            fs.Close();
//            request.Dispose();

//            //������ ���� �ʴ´�. 
//            return default;
//        }
//        catch (OperationCanceledException ex)
//        {
//            if (ex.CancellationToken == cts.Token)
//            {
//                Debug.Log("Timeout");
//                //TODO: ��Ʈ��ũ ��õ� �˾� ȣ��.

//                //��õ�
//                return await WebRequestAssetBundle(_url, networkType, dataType, bundleName);
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.Log(e.Message);
//            request.Dispose();
//            return default;
//        }
//        request.Dispose();
//        return default;
//    }
//    /// <summary>
//    /// Check Network
//    /// </summary>
//    /// <returns></returns>
//    private static async UniTask CheckNetwork()
//    {
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//        {
//            Debug.LogError("The netwok is not connected");
//            await UniTask.WaitUntil(() => Application.internetReachability != NetworkReachability.NotReachable);
//            Debug.Log("The network is connected");
//        }
//    }
//    /// <summary>
//    /// Setting Header
//    /// </summary>
//    /// <param name="request"></param>
//    /// <param name="value1"></param>
//    /// <param name="value2"></param>
//    private static void SetHeaders(UnityWebRequest request, string value1, string value2)
//    {
//        request.SetRequestHeader(value1, value2);
//    }
//    /// <summary>
//    /// DownloadHandlerFactory
//    /// </summary>
//    /// <param name="dataType"></param>
//    /// <param name="filePath"></param>
//    /// <param name="url"></param>
//    /// <param name="crc"></param>
//    /// <returns></returns>
//    private static DownloadHandler DownHandlerFactory(DataType dataType, string filePath = null, string url = null, uint crc = 0)
//    {
//        switch (dataType)
//        {
//            case DataType.BUFFER:
//                return new DownloadHandlerBuffer();
//            case DataType.TEXTURE:
//                return new DownloadHandlerTexture();
//            case DataType.ASSETBUNDLE:
//                return new DownloadHandlerAssetBundle(url, crc);
//            case DataType.FILE:
//                return new DownloadHandlerFile(filePath);
//            default:
//                return null;
//        }
//    }
//}

//public class SH : MonoBehaviour
//{
//    string saveUrl;
//    int fileId;
//    public async void SaveTreeImg()
//    {
//        // Upload Tree File 
//        saveUrl = "/api/v1/files/temp/upload";
//        List<IMultipartFormSection> treeCaptures = new List<IMultipartFormSection>();

//        string path = $"{Application.dataPath}/ScreenShot/Image/TreeCapture_{GameManager.Instance.treeController.treeId}.png";




//        if (!resultPost.result)
//        {
//            Debug.LogError("Tree Screenshot Image ���ε� ����");
//            return;
//        }
//        else
//        {
//            Debug.Log($"Tree Screenshot Image ���ε� ����");
//            Debug.Log($"id = {resultPost.data[0].id}");
//            fileId = resultPost.data[0].id;
//            UploadImg();
//        }
//    }
//}
