using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
//정보 데이터 
[System.Serializable]
public class TodoListData
{
    //팀원
    public int memberNo;
    //프로젝트
    public int projectNo;
    //날짜
    public string dueDate;
    //제목
    public string title;
    //내용
    public string content;
    //태그
    public int tagNo;

    public int todolistNo;

    //public int 
}

//재원오빠가 보내주는대로 바꿔야함.
public class CheckListData
{
    //프로젝트 번호
    public int projectNo;
    //멤버번호
    public int memberNo;
    //제목
    public string title;
    //체크번호
    public int checklistNo;

}

public class CommutingManagementData
{
    public int memberNo;
    public string commutingManagementNo;
}

public class DailyGraphData
{
    public int memberNo;
    public int projectNo;
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

public class CommutingManagementDataArray
{
    public List<CommutingManagementData> data;
}

[Serializable]

public class DailyGraphDataArray
{
    public List<DailyGraphData> data;
}
public class HttpRequesterLYD
{
    //url
    public string url;
    //요청타입 : getp, post
    public RequestTypeLYD requestTypeLYD;
    public string todoList;

    //응답이 왔을 때 호출해줄 함수 (Action)
    //Action 함수를 담을 수 있는 자료형 
    public Action<DownloadHandler> onComplete;

}
