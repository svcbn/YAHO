using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//저장, 불러오기(이거는 이제 네트워크랑 연동했을때, 유니티 껐다가 사원증 아디를 찍었을 경우 그 사원이 저장해놓은 인테리어가 불러와져야함)
//오브젝트 정보
[System.Serializable]
public class ObjectInfo
{
    //벽인지, 램프인지, / (세분화가 나눠지려나???)
    //public int type;
    public enum Type //eunm을 사용하면 string, int 다 사용가능함.
    {
        Lamp,
        Wall
    }
    public Type type;
    //위치
    public Vector3 position;
    //스케일
    public Vector3 scale;
    //각도
    public Vector3 angle;
}

[System.Serializable]
public class ArrayObjectInfo
{
    public List<ObjectInfo> data;
}
public class UIManager : MonoBehaviour
{
    //불러오기에서 사용할 타입 오브젝트 배열
    public GameObject[] loadObjs;

    GameObject obj;
    //임시 오브젝트 정보 담을 변수
    //ObjectInfo objInfo = new ObjectInfo();

    //오브젝트 정보들을 담을 수 있는 리스트
    public List<ObjectInfo> objInfoList = new List<ObjectInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateObject(ObjectInfo info)
    {
        //게임오브젝트 타입대로 생성 
        //1. 게임오브젝트 타입을 가지고온다. (lamp인지, wall인지 (0, 1)
        int a = (int)info.type;
        GameObject loadObj = Instantiate(loadObjs[a]);
        //2. 그거에 맞는 오브젝트를 생성한다. (배열에다가 0값, 1값을 넣어준다.)
            /*if(loadObj.name.Contains("Lamp"))
        {
            GameObject 
        }*/
        loadObj.transform.position = info.position;
        loadObj.transform.localScale = info.scale;
        loadObj.transform.eulerAngles = info.angle;

    }
   
    //저장 버튼
    public void OnClickSave()
    {

        //1. FurnitureParent를 찾는다.
        obj = GameObject.Find("FurnitureParent");
        
        //2. 반복문을 돌려서 그 리스트count나 length에 있는 자식들의 position, rotation. scale의 갓ㅂ을 objectInfo값에 다 넣어준다. 
        for(int i = 0; i < obj.transform.childCount; i++) //자식관련은 다 transform이다.
        {
            ObjectInfo objectInfo = new ObjectInfo();
            // 이 가구가 어떤 가구인지 구별하게한다..
            GameObject child = obj.transform.GetChild(i).gameObject;
            //오브젝트 이름이 램프이면 타입이 lamp여야함 wall도 이런식으로
            if (child.name.Contains("Lamp"))
            {
                objectInfo.type = ObjectInfo.Type.Lamp;
            }
            else if(child.name.Contains("Wall"))
            {
                objectInfo.type = ObjectInfo.Type.Wall;
            }
            objectInfo.position = child.transform.position;
            objectInfo.scale = child.transform.localScale;
            objectInfo.angle = child.transform.eulerAngles;
            
            objInfoList.Add(objectInfo);

        }
        //3. 정보를 리스트에 추가 (근데 이러고 안빼면 그대로 있는것이 아닌가?)
        //위치, 크기, 회전, 오브젝트 종류
        ArrayObjectInfo arrayData = new ArrayObjectInfo();
        arrayData.data = objInfoList;
        //objInfo를 Json으로 변환
        string jsonData = JsonUtility.ToJson(arrayData, true);
        print(jsonData);

        //저장경로 가져오기
        string path = UnityEngine.Application.dataPath + "/Data";

        //해당경로에 Data폴더가 있는가?
        {
            //해당경로를 만들기
            Directory.CreateDirectory(path);
        }

        //Text 파일로 저장
        File.WriteAllText(path + "/data.txt", jsonData);

    }
    
    //불러오기 버튼
    public void OnClickLoad()
    {
        //파일경로
        string path = UnityEngine.Application.dataPath + "/Data/data.txt";
        //데이터 불러오기
        string jsonData = File.ReadAllText(path);
        print(jsonData);

        //jsonData -> Objectinfo
        ArrayObjectInfo arrayData = JsonUtility.FromJson<ArrayObjectInfo>(jsonData);
        //오브젝트 생성
        for(int i = 0; i < arrayData.data.Count; i++)
        {
            ObjectInfo info = arrayData.data[i];
            objInfoList.Add(info);
            CreateObject(info);
        }
    }
}
