using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//����, �ҷ�����(�̰Ŵ� ���� ��Ʈ��ũ�� ����������, ����Ƽ ���ٰ� ����� �Ƶ� ����� ��� �� ����� �����س��� ���׸�� �ҷ���������)
//������Ʈ ����
[System.Serializable]
public class ObjectInfo
{
    //������, ��������, / (����ȭ�� ����������???)
    //public int type;
    public enum Type //eunm�� ����ϸ� string, int �� ��밡����.
    {
        Lamp,
        Wall
    }
    public Type type;
    //��ġ
    public Vector3 position;
    //������
    public Vector3 scale;
    //����
    public Vector3 angle;
}

[System.Serializable]
public class ArrayObjectInfo
{
    public List<ObjectInfo> data;
}
public class UIManager : MonoBehaviour
{
    //�ҷ����⿡�� ����� Ÿ�� ������Ʈ �迭
    public GameObject[] loadObjs;

    GameObject obj;
    //�ӽ� ������Ʈ ���� ���� ����
    //ObjectInfo objInfo = new ObjectInfo();

    //������Ʈ �������� ���� �� �ִ� ����Ʈ
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
        //���ӿ�����Ʈ Ÿ�Դ�� ���� 
        //1. ���ӿ�����Ʈ Ÿ���� ������´�. (lamp����, wall���� (0, 1)
        int a = (int)info.type;
        GameObject loadObj = Instantiate(loadObjs[a]);
        //2. �װſ� �´� ������Ʈ�� �����Ѵ�. (�迭���ٰ� 0��, 1���� �־��ش�.)
            /*if(loadObj.name.Contains("Lamp"))
        {
            GameObject 
        }*/
        loadObj.transform.position = info.position;
        loadObj.transform.localScale = info.scale;
        loadObj.transform.eulerAngles = info.angle;

    }
   
    //���� ��ư
    public void OnClickSave()
    {

        //1. FurnitureParent�� ã�´�.
        obj = GameObject.Find("FurnitureParent");
        
        //2. �ݺ����� ������ �� ����Ʈcount�� length�� �ִ� �ڽĵ��� position, rotation. scale�� ������ objectInfo���� �� �־��ش�. 
        for(int i = 0; i < obj.transform.childCount; i++) //�ڽİ����� �� transform�̴�.
        {
            ObjectInfo objectInfo = new ObjectInfo();
            // �� ������ � �������� �����ϰ��Ѵ�..
            GameObject child = obj.transform.GetChild(i).gameObject;
            //������Ʈ �̸��� �����̸� Ÿ���� lamp������ wall�� �̷�������
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
        //3. ������ ����Ʈ�� �߰� (�ٵ� �̷��� �Ȼ��� �״�� �ִ°��� �ƴѰ�?)
        //��ġ, ũ��, ȸ��, ������Ʈ ����
        ArrayObjectInfo arrayData = new ArrayObjectInfo();
        arrayData.data = objInfoList;
        //objInfo�� Json���� ��ȯ
        string jsonData = JsonUtility.ToJson(arrayData, true);
        print(jsonData);

        //������ ��������
        string path = UnityEngine.Application.dataPath + "/Data";

        //�ش��ο� Data������ �ִ°�?
        {
            //�ش��θ� �����
            Directory.CreateDirectory(path);
        }

        //Text ���Ϸ� ����
        File.WriteAllText(path + "/data.txt", jsonData);

    }
    
    //�ҷ����� ��ư
    public void OnClickLoad()
    {
        //���ϰ��
        string path = UnityEngine.Application.dataPath + "/Data/data.txt";
        //������ �ҷ�����
        string jsonData = File.ReadAllText(path);
        print(jsonData);

        //jsonData -> Objectinfo
        ArrayObjectInfo arrayData = JsonUtility.FromJson<ArrayObjectInfo>(jsonData);
        //������Ʈ ����
        for(int i = 0; i < arrayData.data.Count; i++)
        {
            ObjectInfo info = arrayData.data[i];
            objInfoList.Add(info);
            CreateObject(info);
        }
    }
}
