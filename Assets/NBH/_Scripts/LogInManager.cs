using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviourPunCallbacks
{
    #region ����ȭ�� component

    // ���̵� InputField
    public InputField inputID;

    // ��й�ȣ IF
    public InputField inputPW;

    // �α��� ��ư
    public Button btnLogIn;

    // ��й�ȣ ã�� ��ư
    public Button btnFindPW;

    // ���̵� ã�� ��ư
    public Button btnFindID;

    // ȸ������ ��ư
    public Button btnSignUp;

    // ȸ������ �г�
    public GameObject panelSignUp;
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
