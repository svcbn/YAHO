using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInformation_BH : MonoBehaviour
{
    public static UserInformation_BH instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }
    [SerializeField]
    private string _memberId;

    [SerializeField]
    private string _name;

    [SerializeField]
    private string _accessToken;

    [SerializeField]
    private int _memberNo;

    [SerializeField]
    private int _commutingManagementNo;

    public string MemberId
    {
        get
        {
            return _memberId;
        }
        set
        {
            _memberId = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public string AccessToken
    {
        get
        {
            return _accessToken;
        }
        set
        {
            _accessToken = value;
        }
    }

    public int MemberNo
    {
        get
        {
            return _memberNo;
        }
        set
        {
            _memberNo = value;
        }
    }

    public int CommutingManagementNo
    {
        get
        {
            return _commutingManagementNo;
        }
        set
        {
            _commutingManagementNo = value;
        }
    }

    private void Start()
    {
        
    }
}
