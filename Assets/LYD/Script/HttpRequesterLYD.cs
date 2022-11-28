using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
//���� ������ 
[System.Serializable]
public class TodoListData
{
    //����
    public int memberNo;
    //������Ʈ
    public int projectNo;
    //��¥
    public string dueDate;
    //����
    public string title;
    //����
    public string content;
    //�±�
    public int tagNo;

    public int todolistNo;
}

//��������� �����ִ´�� �ٲ����.
public class CheckListData
{
    //������Ʈ ��ȣ
    public int projectNo;
    //�����ȣ
    public int memberNo;
    //����
    public string title;
    //üũ��ȣ
    public int checklistNo;

}

public enum RequestTypeLYD
{
    POST,
    GET,
    PUT
}

[Serializable]
public class TodolistDataArray
{
    public List<TodoListData> data;
}

[Serializable]

public class CheckListDatArray
{
    public List<CheckListData> data;
}
public class HttpRequesterLYD
{
    //url
    public string url;
    //��ûŸ�� : getp, post
    public RequestTypeLYD requestTypeLYD;
    public string todoList;

    //������ ���� �� ȣ������ �Լ� (Action)
    //Action �Լ��� ���� �� �ִ� �ڷ��� 
    public Action<DownloadHandler> onComplete;

}