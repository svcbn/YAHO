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
}
